using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using WebCommands.Infrastructure.Commands;
using WebCommands.Infrastructure.Dependencies;

namespace WebCommands.Infrastructure.Bus
{
    public class InMemoryBus : IBus
    {
        private readonly IDictionary<string, Type> handlers = new Dictionary<string, Type>();
        private readonly IDependencyResolver dependencyResolver;

        public InMemoryBus(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        public void RegisterHandler<T>() where T : Command
        {
            var commandType = typeof(T);

            this.handlers.Add(commandType.Name, commandType);
        }

        public async Task Send<T>(T command) where T : Command
        {
            await Task.Run(() =>
            {
                var commandType = command.GetType();

                if (this.handlers.ContainsKey(commandType.Name))
                {
                    var handlerType = Type.GetType(commandType.FullName + "Handler");
                    var handler = this.dependencyResolver.Get(handlerType);

                    handlerType.GetMethod("Handle", new[] { commandType }).Invoke(handler, new object[] { command });
                }
            });
        }

        public IDictionary<string, Type> Handlers => handlers;
    }
}