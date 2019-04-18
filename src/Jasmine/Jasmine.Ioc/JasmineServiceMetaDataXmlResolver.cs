using Jasmine.Extensions;
using Jasmine.Ioc.Exceptions;
using Jasmine.Reflection;
using Jasmine.Reflection.Interfaces;
using Jasmine.Reflection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Jasmine.Ioc
{
    public class JasmineServiceMetaDataXmlResolver : IServiceMetaDataXmlResolver
    {

        private JasmineServiceMetaDataXmlResolver()
        {

        }

        private const string SERVICES = "";
        private const string SERVICE = "";
        private const string CONSTRUCTOR = "";
        private const string NULLABLE = "";
        private const string DEFAULT_VALUE = "";
        private const string PARAM = "";
        private const string INDEX = "";
        private const string REF = "";
        private const string TYPE = "";
        private const string IMPL = "";
        private const string VALUE = "";
        private const string SCOPE = "";
        private const string PROPERTY = "";
        private const string INITIA_METHOD = "";
        private const string DESTROY_METHOD = "";
        private const string NOT_NULL = "";
        private const string Name = "";

        private readonly ITypeCache _typeCache = DefaultReflectionCache.Instance;
        private readonly IServiceMetaDataReflectResolver _resolver;
        private readonly IServiceMetaDataManager _manager;

        public static readonly IServiceMetaDataXmlResolver Instance = new JasmineServiceMetaDataXmlResolver();


        public void Resolve(string path)
        {
            var dom = new XmlDocument();

            dom.Load(path);

            var dic = new Dictionary<string, Type>();//use to cache name type mapping

            foreach (var service in dom.GetDirectChildrenByTagName(SERVICES))//iterate in services element
            {
                var typeStr = service.GetAttributeValue(TYPE);

                if (typeStr == null)//has type attr
                {
                    throw new RequirdAttributeNotFoundException("type tag is requird of service element!");
                }
                else
                {
                    var type = Type.GetType(typeStr);//try get type

                    if (type == null)
                    {
                        throw new TypeNotFoundException($"{typeStr} is not found");
                    }

                    var name = service.GetAttributeValue(REF);

                    if (name != null)
                        dic.Add(name, type);

                    if (type.IsInterfaceOrAbstraClass())//abstrct class impl should be instructed
                    {
                        var impl = service.GetAttributeValue(IMPL);

                        if (impl == null)
                            throw new RequirdAttributeNotFoundException($"abstract or interface ,the impl attribute should be instructed!");

                        var typeimpl = Type.GetType(impl);

                        if (typeimpl == null)
                            throw new TypeNotFoundException($"{impl} is not found!");

                        if (typeimpl.IsInterfaceOrAbstraClass() || typeimpl.IsDerivedFrom(type))//
                            throw new NotImplementedException($"{typeimpl} is abstract or not implement {type}");

                        _manager.SetImplement(type, typeimpl);


                    }
                    else
                    {
                        //base type info

                        if (!_manager.ContainsKey(type))
                            _resolver.Resolve(type);

                        _manager.TryGetValue(type, out var metaData);

                        var scope = service.GetAttributeValue(SCOPE);

                        if (scope != null)
                        {
                            if (Enum.TryParse<ServiceScope>(scope, out var scopeE))
                            {
                                metaData.Scope = scopeE;
                            }
                            else
                            {
                                throw new AttributeValueIncorrectException($"property scope,the value is incorrect!");
                            }

                        }


                        //find matched  constructor
                        //every parameter's type attribute is required if ref not exist

                        foreach (var constructor in service.GetDirectChildrenByTagName(CONSTRUCTOR))
                        {
                            var candidate = new MethodCadidate();

                            foreach (var parameter in constructor.GetDirectChildrenByTagName(PARAM))
                            {
                                var parameterCadidate = new ParameterCadidate();

                                var refer = parameter.GetAttributeValue(REF);

                                if (dic.TryGetValue(refer, out var value))
                                {
                                    parameterCadidate.Type = value;
                                }

                                var paraTypeStr = parameter.GetAttributeValue(TYPE);

                                if (Utls.TryGetType(paraTypeStr, out var paraType))
                                {
                                    parameterCadidate.Type = paraType;
                                }
                                else
                                {
                                    throw new TypeNotFoundException($"{paraTypeStr} of parameter is not found!");
                                }


                                var paraTypeImplStr = parameter.GetAttributeValue(IMPL);


                                if (Utls.TryGetType(paraTypeImplStr, out var paraTypImpl))
                                {
                                    if (paraTypImpl.IsInterfaceOrAbstraClass())
                                        throw new NotImplementedException($"{paraTypImpl} is abstrct or interface");

                                    parameterCadidate.Impl = paraTypImpl;
                                }
                                else
                                {
                                    throw new TypeNotFoundException($"{paraTypeImplStr} is not found!");
                                }

                                if (paraType == null || paraTypImpl == null)
                                    throw new RequirdAttributeNotFoundException("parameter type or implement type must be instruct");

                                if (paraType != null && IMPL != null && !paraTypImpl.IsDerivedFrom(paraType))
                                    throw new NotImplementedException($"{paraTypImpl}  not implement {paraType}");


                                var notNull = parameter.GetAttributeValue(NOT_NULL);

                                if (JasmineStringConvertor.TryGetValue<bool>(notNull, out var notNullValue))
                                {
                                    parameterCadidate.NullNable = notNullValue;
                                }
                                else
                                {
                                    throw new AttributeValueIncorrectException($"{notNull} is incorrect!");
                                }

                                var value1 = parameter.GetAttributeValue(VALUE);

                                if (value1 != null)
                                    parameterCadidate.DefaultValue = value1;

                                foreach (var defaultValue in parameter.GetDirectChildrenByTagName(VALUE))
                                {
                                    parameterCadidate.DefaultValue = defaultValue.InnerText;
                                }


                                candidate.Parameters.Add(parameterCadidate);

                            }


                            var constructors = _typeCache.GetItem(type).Constructors;

                            Constructor matcherConstructor = null;

                            foreach (var ctr in constructors)
                            {
                                var parameters = ctr.Parameters.ToArray();

                                Array.Sort(parameters, (x, y) => x.Index.CompareTo(y.Index));


                                if (candidate.Parameters.Count == parameters.Length)
                                {
                                    var i = 0;
                                    bool match = true;

                                    foreach (var para in candidate.Parameters)
                                    {
                                        if (para.HasImplemnt && para.Impl.IsDerivedFrom(parameters[i].ParameterType))
                                            match = true;
                                        else if (para.Type.IsDerivedFrom(parameters[i].ParameterType))
                                            match = true;
                                        else
                                        {
                                            match = false;
                                            break;
                                        }

                                    }

                                    if (match)
                                    {
                                        matcherConstructor = ctr;
                                        break;
                                    }
                                }

                                if (metaData.ConstrctorMetaData.Constructor.Equals(ctr))
                                {
                                   
                                    foreach (var item in metaData.ConstrctorMetaData.Parameters)
                                    {

                                    }
                                }
                                else
                                {
                                    var constructorMetaData = new ConstructorMetaData(ctr);
                                }


                            }





                        }


                        /***
                         * 
                         *
                         */



                        foreach (var property in service.GetDirectChildrenByTagName(PROPERTY))
                        {


                        }

                        foreach (var initiaMethod in service.GetDirectChildrenByTagName(INITIA_METHOD))
                        {

                        }

                        foreach (var destroyMethod in service.GetDirectChildrenByTagName(DESTROY_METHOD))
                        {

                        }

                    }
                }

            }


        }

        private class MethodCadidate
        {
            public IList<ParameterCadidate> Parameters { get; set; } = new List<ParameterCadidate>();
        }

        private class ParameterCadidate
        {
            public string Name { get; set; }
            public Type Type { get; set; }
            public Type Impl { get; set; }
            public object DefaultValue { get; set; }
            public int Index { get; set; }
            public bool NullNable { get; set; }

            public bool HasImplemnt => Impl != null;
        }
    }
}
