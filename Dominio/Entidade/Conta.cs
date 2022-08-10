using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dominio.Entidade
{
    public class Conta
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public string Numero { get; set; }
        public string Agencia { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Transacao>? Transacoes { get; set; }

        public Conta()
        {
            ClienteId = Guid.Empty;
            Numero = string.Empty;
            Agencia = string.Empty;
        }
    }
}