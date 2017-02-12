using System.Collections.Generic;
using System.Linq;

namespace WebCommands.Infrastructure.Commands
{
    public class ValidationResult
    {
        public static ValidationResult Empty = new ValidationResult();

        private readonly IList<Error> errors = new List<Error>();

        public void Add(Error error) => this.errors.Add(error);

        public bool IsValid
        {
            get
            {
                return !this.errors.Any();
            }
        }

        public IEnumerable<Error> Errors
        {
            get
            {
                return this.errors;
            }
        }

        public class Error
        {
            public string Message { get; set; }
        }
    }
}