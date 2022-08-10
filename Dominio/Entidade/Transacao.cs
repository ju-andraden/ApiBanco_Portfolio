using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidade
{
    public class Transacao
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ContaId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public decimal Valor { get; set; }
        public string TipoTransacao { get; set; }

        public Transacao()
        {
            Descricao = string.Empty;
            TipoTransacao = string.Empty;
        }
    }
}