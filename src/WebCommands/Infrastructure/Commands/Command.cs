namespace WebCommands.Infrastructure.Commands
{
    public abstract class Command
    {
        public virtual ValidationResult Validate()
        {
            return ValidationResult.Empty;
        }
    }
}