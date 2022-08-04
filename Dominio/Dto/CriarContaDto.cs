using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dto
{
    public class CriarContaDto
    {
        public Guid ClienteId { get; set; }
        public string Numero { get; set; }
        public string Agencia { get; set; }
    }
}
