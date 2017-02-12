using System;
using WebCommands.Dominio.Repositorios;
using WebCommands.Infrastructure.Commands;

namespace WebCommands.Dominio.Comandos
{
    public class CancelarNotaFiscal : Command
    {
        public Guid Codigo { get; set; }

        public override ValidationResult Validate()
        {
            var result = new ValidationResult();

            if (this.Codigo == Guid.Empty)
                result.Add(new ValidationResult.Error() { Message = "Código da NF não informado." });

            return result;
        }
    }

    public class CancelarNotaFiscalHandler : IHandler<CancelarNotaFiscal>
    {
        private readonly IRepositorioDeNotasFiscais repositorioDeNotasFiscais;

        public CancelarNotaFiscalHandler(IRepositorioDeNotasFiscais repositorioDeNotasFiscais)
        {
            this.repositorioDeNotasFiscais = repositorioDeNotasFiscais;
        }

        public void Handle(CancelarNotaFiscal command)
        {
            var nf = this.repositorioDeNotasFiscais.BuscarPor(command.Codigo.ToString());

            nf.Cancelar();
        }
    }
}