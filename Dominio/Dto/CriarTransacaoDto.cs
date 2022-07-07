using Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dto
{
    public class CriarTransacaoDto
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
    }
}
