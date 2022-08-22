using _4_Recursos;
using Aplicacao.Interfaces;
using Dominio.Dto;
using Dominio.Entidade;
using Infraestrutura.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Services
{
    public class ContaService : IContaService
    {
        private readonly ApiDbContext _context;
        private readonly ITransacaoService _transacaoService;

        public ContaService(ApiDbContext context, ITransacaoService transacaoService)
        {
            _context = context;
            _transacaoService = transacaoService;
        }

        public async Task<Conta> Criar(CriarContaDto criarContaDto)
        {
            if (criarContaDto.ClienteId.Equals(Guid.Empty))
            {
                return null;
            }

            var clienteExiste = await ValidarSeClienteExiste(criarContaDto.ClienteId);

            if (!clienteExiste)
            {
                return null;
            }

            Conta conta = new Conta();
            conta.ClienteId = criarContaDto.ClienteId;
            conta.Numero = criarContaDto.NumeroConta;
            conta.Agencia = criarContaDto.Agencia;

            await _context.Contas.AddAsync(conta);
            await _context.SaveChangesAsync();

            return conta;
        }

        public async Task<List<Conta>> Ler()
        {
            var contas = await _context.Contas.ToListAsync();

            return contas;
        }

        public async Task<Conta> Ler(string numero)
        {
            var conta = await BuscarContaPeloNumero(numero);

            if (conta is null)
            {
                return null;
            }
            return conta;
        }

        public async Task<List<Conta>> Ler(Guid id)
        {
            var listaContas = await _context.Contas.Where(conta 
                => conta.ClienteId == id).ToListAsync();

            return listaContas;
        }

        public async Task<Conta> AtualizarConta(string numeroConta, AtualizarContaDto atualizarContaDto)
        {
            var conta = await BuscarContaPeloNumero(numeroConta);

            if (conta is null)
            {
                return null;
            }

            AtualizarContaSemDadosNulos(conta, atualizarContaDto);

            _context.Entry(conta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return conta;
        }

        public async Task<string> Deletar(string numero)
        {
            var conta = await BuscarContaPeloNumero(numero);

            if (conta is null)
            {
                return null;
            }

            _context.Contas.Remove(conta);
            await _context.SaveChangesAsync();

            return Mensagens.RemoverConta;
        }

        private async Task<Conta> BuscarContaPeloNumero(string numero)
        {
            var conta = await _context.Contas.FirstOrDefaultAsync(conta 
                => conta.Numero.Equals(numero));

            if (conta != null)
            {
                conta.Transacoes = await _transacaoService.LerTransacoes(conta.Id);
            }
            return conta;
        }
        private async Task<bool> ValidarSeClienteExiste(Guid clienteId)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(cliente 
                => cliente.Id.Equals(clienteId));

            if (cliente is null)
            {
                return false;
            }
            return true;
        }

        private void AtualizarContaSemDadosNulos(Conta conta, AtualizarContaDto atualizarContaDto)
        {
            if (!string.IsNullOrEmpty(atualizarContaDto.NumeroConta))
            {
                conta.Numero = atualizarContaDto.NumeroConta;
            }

            if (!string.IsNullOrEmpty(atualizarContaDto.Agencia))
            {
                conta.Agencia = atualizarContaDto.Agencia;
            }
        }
    }
}