using Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dto
{
    public class Cliente
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string? Telefone { get; set; }
        public Guid EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
    }
}
