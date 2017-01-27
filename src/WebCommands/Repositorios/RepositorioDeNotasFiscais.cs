using System.Collections.Generic;
using System.Linq;
using WebCommands.Dominio;
using WebCommands.Dominio.Repositorios;

namespace WebCommands.Repositorios
{
    public class RepositorioDeNotasFiscais : IRepositorioDeNotasFiscais
    {
        private readonly IList<NotaFiscal> itens = new List<NotaFiscal>();

        public void Adicionar(NotaFiscal entidade) =>
            itens.Add(entidade);

        public NotaFiscal BuscarPor(string codigo) =>
            itens.SingleOrDefault(c => c.Codigo == codigo);
    }
}