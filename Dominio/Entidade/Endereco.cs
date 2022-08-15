using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidade
{
    public class Endereco
    {
        [Key]
        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public Endereco()
        {
            Logradouro = string.Empty;
            Cep = string.Empty;
            Cidade = string.Empty;
            Estado = string.Empty;
        }
    }
}