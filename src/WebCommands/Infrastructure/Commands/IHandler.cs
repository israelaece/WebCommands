namespace WebCommands.Infrastructure.Commands
{
    public interface IHandler<T> where T : Command
    {
        void Handle(T command);
    }
}