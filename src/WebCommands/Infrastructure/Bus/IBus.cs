using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCommands.Infrastructure.Commands;

namespace WebCommands.Infrastructure.Bus
{
    public interface IBus
    {
        void RegisterHandler<T>() where T : Command;

        Task Send<T>(T command) where T : Command;

        IDictionary<string, Type> Handlers { get; }
    }
}