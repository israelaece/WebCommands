using System.Collections.Generic;
using System.Linq;
using WebCommands.Dominio;
using WebCommands.Dominio.Repositorios;

namespace WebCommands.Repositorios
{
    public class RepositorioDeClientes : IRepositorioDeClientes
    {
        private readonly IList<Cliente> itens = new List<Cliente>();

        public RepositorioDeClientes()
        {
            this.Adicionar(new Cliente() { Documento = "123", Nome = "Israel" });
        }

        public void Adicionar(Cliente entidade) =>
            itens.Add(entidade);

        public Cliente BuscarPor(string codigo) =>
            itens.SingleOrDefault(c => c.Documento == codigo);
    }
}