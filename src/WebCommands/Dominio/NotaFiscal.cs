using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCommands.Dominio
{
    public class NotaFiscal
    {
        private readonly IList<Item> itens = new List<Item>();

        public NotaFiscal(Cliente cliente)
        {
            this.Destinatario = new DadosDoDestinatario(cliente.Nome, cliente.Documento);
            this.Situacao = Situacoes.Emitida;
            this.Codigo = Guid.NewGuid().ToString();
        }

        public void Adicionar(Item item)
        {
            itens.Add(item);
            this.Total += item.Quantidade * item.Valor;
        }

        public void Cancelar() => this.Situacao = Situacoes.Cancelada;

        public string Codigo { get; private set; }

        public Situacoes Situacao { get; private set; }

        public DadosDoDestinatario Destinatario { get; private set; }

        public decimal Total { get; private set; }

        public IEnumerable<Item> Itens => itens.ToList();

        public class DadosDoDestinatario
        {
            public DadosDoDestinatario(string nome, string documento)
            {
                this.Nome = nome;
                this.Documento = documento;
            }

            public string Nome { get; private set; }

            public string Documento { get; private set; }
        }

        public class Item
        {
            public string Produto { get; set; }

            public int Quantidade { get; set; }

            public decimal Valor { get; set; }
        }

        public enum Situacoes
        {
            Emitida,
            Cancelada
        }
    }
}