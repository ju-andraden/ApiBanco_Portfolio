using Dominio.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
