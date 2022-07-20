using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dominio.Dto
{
    public class Conta
    {
        [Key]
        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string? Agencia { get; set; }
        public Guid? ClienteId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Transacao>? Transacoes { get; set; }
    }
}
