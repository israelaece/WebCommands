using System;

namespace WebCommands.Infrastructure.Dependencies
{
    public interface IDependencyResolver
    {
        void Add<TService, TImplementation>() where TImplementation : TService;

        void Add<T>(T instance) where T : class;

        object Get(Type type);
    }
}