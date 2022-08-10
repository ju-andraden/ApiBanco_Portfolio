using Dominio.Dto;
using Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IContaService
    {
        public Task<Conta> Criar(CriarContaDto criarContaDto);
        public Conta Ler(string numero);
        public Task<List<Conta>> Ler();
        public List<Conta> Ler(Guid id);
        public Conta Atualizar(string numeroConta, Conta novosDados);
        public string Deletar(string numero);
    }
}
