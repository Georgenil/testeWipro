using System;

namespace testeWipro.Domain
{
    public class Locacao
    {
        public int Id { get; set; }
        public int FilmeId { get; set; }
        public Filme? Filme { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime DataFim { get; set; }
        public DateTime DataDevolucao { get; set; }

    }
}
