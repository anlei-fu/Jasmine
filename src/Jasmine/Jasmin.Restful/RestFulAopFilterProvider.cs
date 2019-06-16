using Jasmine.Common;
using Jasmine.Common.Exceptions;
using Jasmine.Ioc;
using Jasmine.Restful.Exceptions;
using System;

namespace Jasmine.Restful
{
    public class RestfulAopFilterProvider : AbstractAopFilterProvider<HttpFilterContext>
    {
        private RestfulAopFilterProvider()
        {

        }

        public static IAopFilterProvider<HttpFilterContext> Instance = new RestfulAopFilterProvider();

        private IServiceProvider _serviceProvider => IocServiceProvider.Instance;

        public override IFilter<HttpFilterContext> GetFilter(string name)
        {
            if (!base._nameMap.ContainsKey(name))
            {
                var type = Type.GetType(name);

                _map.TryGetValue(type, out var instance);
                 
                if (instance == null)
                {
                    throw new AopFilterCanNotBeCreatedException($"{name} can not be created! may ioc service config incorrect or not config");
                }

              
            }

            return _map[_nameMap[name]];
        }

        public override void AddFilter(string name, Type type)
        {
            if(!_nameMap.TryAdd(name,type)&&_nameMap[name]==type)
            {
                throw new FilterAlreadyExistException($" giving filter ({type}) 's name alreadey exists in {_nameMap[name]} ");
            }

            if(!_map.ContainsKey(type))
            {
                var instance = IocServiceProvider.Instance.GetService(type);

                if (instance == null)
                    throw new AopFilterCanNotBeCreatedException($"the filter ({type} can not be created ,maybe ioc service config incorect or not config yet!)");
            }
        }

        public override void AddFilter<T>()
         
        {
            if (_map.ContainsKey(typeof(T)))
                return;

            var instance = IocServiceProvider.Instance.GetService<T>();

            if (instance == null)
                throw new AopFilterCanNotBeCreatedException($"the filter ({type} can not be created ,maybe ioc service config incorect or not config yet!)");

            AddFilter(Instance);
        }

        public override IFilter<HttpFilterContext> GetFilter<T>()
        {
           
        }

        public override IFilter<HttpFilterContext> GetFilter(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
