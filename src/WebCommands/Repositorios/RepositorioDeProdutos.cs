using System.Collections.Generic;
using System.Linq;
using WebCommands.Dominio;
using WebCommands.Dominio.Repositorios;

namespace WebCommands.Repositorios
{
    public class RepositorioDeProdutos : IRepositorioDeProdutos
    {
        private readonly IList<Produto> itens = new List<Produto>();

        public RepositorioDeProdutos()
        {
            this.Adicionar(new Produto() { Descricao = "Mouse", Valor = 120 });
        }

        public void Adicionar(Produto entidade) =>
            itens.Add(entidade);

        public Produto BuscarPor(string codigo) =>
            itens.SingleOrDefault(c => c.Descricao == codigo);
    }
}