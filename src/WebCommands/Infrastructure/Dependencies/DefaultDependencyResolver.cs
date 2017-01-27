using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebCommands.Infrastructure.Dependencies
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        private readonly Dictionary<Type, Type> itens = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, object> singletons = new Dictionary<Type, object>();

        public void Add<TService, TImplementation>() where TImplementation : TService =>
            itens.Add(typeof(TService), typeof(TImplementation));

        public void Add<T>(T instance) where T : class =>
            singletons.Add(typeof(T), instance);

        public object Get(Type type)
        {
            if (singletons.ContainsKey(type))
                return singletons[type];
            else if (itens.ContainsKey(type))
                return CreateInstance(itens[type]);

            return CreateInstance(type);
        }

        private object CreateInstance(Type type)
        {
            var flags = BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance;
            var parameters = from p in type.GetConstructors(flags)[0].GetParameters()
                             select this.Get(p.ParameterType);

            return Activator.CreateInstance(type, parameters.ToArray());
        }
    }
}