using System;

namespace testeWipro.Domain
{
    public class Filme
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        // Quando o valor for igual a false o filme não foi locado se for igual a true foi locado
        public bool Status { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
