using System.Collections.Generic;
using WebCommands.Dominio.Repositorios;
using WebCommands.Infrastructure.Commands;

namespace WebCommands.Dominio.Comandos
{
    public class EmitirNotaFiscal : Command
    {
        public string DocumentoDoCliente { get; set; }

        public IDictionary<string, int> Itens { get; set; }
    }

    public class EmitirNotaFiscalHandler : IHandler<EmitirNotaFiscal>
    {
        private readonly IRepositorioDeClientes repositorioDeClientes;
        private readonly IRepositorioDeProdutos repoositorioDeProdutos;
        private readonly IRepositorioDeNotasFiscais repositorioDeNotasFiscais;

        public EmitirNotaFiscalHandler(
            IRepositorioDeClientes repositorioDeClientes,
            IRepositorioDeProdutos repoositorioDeProdutos,
            IRepositorioDeNotasFiscais repositorioDeNotasFiscais)
        {
            this.repositorioDeClientes = repositorioDeClientes;
            this.repoositorioDeProdutos = repoositorioDeProdutos;
            this.repositorioDeNotasFiscais = repositorioDeNotasFiscais;
        }

        public void Handle(EmitirNotaFiscal command)
        {
            var cliente = this.repositorioDeClientes.BuscarPor(command.DocumentoDoCliente);
            var nf = new NotaFiscal(cliente);

            foreach (var item in command.Itens)
            {
                var produto = this.repoositorioDeProdutos.BuscarPor(item.Key);

                nf.Adicionar(new NotaFiscal.Item()
                {
                    Produto = produto.Descricao,
                    Valor = produto.Valor,
                    Quantidade = item.Value
                });
            }

            this.repositorioDeNotasFiscais.Adicionar(nf);
        }
    }
}