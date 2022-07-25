using _4_Recursos;
using Aplicacao.Interfaces;
using Dominio.Dto;
using Infraestrutura.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Conta Criar(CriarContaDto criarContaDto)
        {
            if (criarContaDto.ClienteId.Equals(Guid.Empty))
            {
                return null;
            }

            var clienteExiste = ValidarSeClienteExiste(criarContaDto.ClienteId);

            if (!clienteExiste)
            {
                return null;
            }

            Conta conta = new Conta();
            conta.ClienteId = criarContaDto.ClienteId;
            conta.Numero = criarContaDto.Numero;
            conta.Agencia = criarContaDto.Agencia;

            _context.Contas.Add(conta);
            _context.SaveChanges();

            return conta;
        }

        public List<Conta> Ler()
        {
            var contas = _context.Contas.ToList();

            return contas;
        }

        public Conta Ler(string numero)
        {
            var conta = BuscarContaPeloNumero(numero);

            if (conta is null)
            {
                return null;
            }
            return conta;
        }

        public Conta Atualizar(string numeroConta, Conta novosDados)
        {
            var conta = BuscarContaPeloNumero(numeroConta);

            if (conta is null)
            {
                return null;
            }
            conta.Agencia = novosDados.Agencia;
            conta.Numero = novosDados.Numero;

            _context.Entry(conta).State = EntityState.Modified;
            _context.SaveChanges();

            return conta;
        }

        public string Deletar(string numero)
        {
            var conta = BuscarContaPeloNumero(numero);

            if (conta is null)
            {
                return null;
            }

            _context.Contas.Remove(conta);
            _context.SaveChanges();

            return Mensagens.RemoverConta;
        }

        private Conta BuscarContaPeloNumero(string numero)
        {
            var conta = _context.Contas.FirstOrDefault(conta => conta.Numero.Equals(numero));

            if (conta != null)
            {
                conta.Transacoes = _transacaoService.LerTransacoes(conta.Id);
            }

            return conta;
        }
        private bool ValidarSeClienteExiste(Guid clienteId)
        {
            var cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id.Equals(clienteId));

            if (cliente is null)
            {
                return false;
            }

            return true;
        }
        public List<Conta> Ler(Guid id)
        {
            var listaContas = _context.Contas.Where(conta => conta.ClienteId == id).ToList();

            return listaContas;
        }
    }
}
