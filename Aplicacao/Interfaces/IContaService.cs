using Dominio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IContaService
    {
        public Conta Criar(string numero);
        public Conta Ler(string numero);
        public List<Conta> Ler();
        public Conta Atualizar(string numeroConta, Conta novosDados);
        public string Deletar(string numero); 
    }
}
