using System;
using System.Collections.Concurrent;

namespace Jasmine.Configuration
{
    public class JasmineConfigurationProvider : IConfigrationProvider
    {
        private JasmineConfigurationProvider()
        {

        }

        private ConcurrentDictionary<string, IConfigGroup> _groups = new ConcurrentDictionary<string, IConfigGroup>();

        public static readonly JasmineConfigurationProvider Instance = new JasmineConfigurationProvider();
        /// <summary>
        /// using json serialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <returns></returns>
        public T GetConfig<T>(string config)
        {
            return (T)GetConfig(typeof(T), config);
        }
        /// <summary>
        /// using json serialize
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public object GetConfig(Type type, string config)
        {
            return JasmineConfigStringConvertor.Convert(type, GetConfig(config));
        }
        /* 
         * e.g1
         * 
         * template:
         * <config-group name="config">
         * <prefix>
         * @{name}@ is  @{sth}@
         * </prefix>
         * </config-group>
         * 
         * input:
         * config.prefix[name:jamsmine,sth:admin]
         * 
         * output:
         * jasmin is admin
         * 
         * e.g 2
         * 
         * template:
         * 
         * <config-group name="config">
         * <prefix>
         * @{name}@ is @{sth}@
         * </prefix>
         * <decorate>
         *  a very good @{sth}@
         * </decorate>
         * </config-group>
         *
         * input:
         * config.prefix[name:jasmine,sth:${config.decorate[sth;admin]}$]
         * 
         * output:
         * jasmine is a very good admin
         * 
         * 
         * e.g3
         * 
         * template:
         * <config-group name="address">
         * <host>
         * 127.0.0.1
         * </host>
         * <port value="80"/>
         * <listen>
         * ${address.host}$:${address.port}$
         * </listen>
         * </config-group>
         * 
         * input :
         * config.listen
         * 
         * output:
         * 127.0.0.1:80
         * 
         * ${}$  represent a exists config template
         * @{}@  respresent a varible
         * 
         * [] represent a  real value to replace varible parameter ,parameters splite by ',', as form  [paraname;value,paraname:value]
         * value can get from another template  [paraname:template] as like 'address.localhost[host:${address.host}$,port:${adress.port}$]'
         * 
         * every template must use full name to visit
         * 
         * xml config file structure
         * 
         * group by tag '<config-group></config-group name=''>','name' attr is required,name as a namespace of config
         * template  name equels  to tag name  ,can use a value attr to represent like '<localhost value="127.0.0.1:80">' or inner-text
         * like '<localhost>127.0.0.1:80</localhost>',it  also can be empty
         *  can use a '<import path=""/>' to load a another config file,path attr is required and suggest  using full path, so import  is a key keyword,
         *  it can not use to represent a template name
         * 
         * warn: maybe throw a stack overflow exception, cause template dependency loop,
         * when it throw this exception , should   check is template dependency loop
         * 
         * 
         * 
         * 
         * 
         */

        public string GetConfig(string config)
        {
            return  PropertyNodeParser.Instance.Parse(config).GetValue(this);
        }

      

      
        public void LoadConfig(string path)
        {
            var groups =  ConfigurationXmlResolver.Instance.Resolve(path);

            foreach (var item in groups)
            {
                _groups.TryAdd(item.Name, item);
            }
        }

        public IConfigGroup GetGroup(string name)
        {
            return _groups.TryGetValue(name, out var result) ? result : null;
        }
    }
}
