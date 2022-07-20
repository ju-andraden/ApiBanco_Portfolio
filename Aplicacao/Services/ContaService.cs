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

        public Conta Criar(string numero)
        {
            Conta conta = new Conta();
            conta.Numero = numero;
            
            _context.Contas.Add(conta);
            _context.SaveChanges();
            return conta;

            /*var x = BuscarContaPeloNumero(numero);
            //se x for nulo -> criar a conta
            //se x for uma conta -> informar que já existe a conta
            if (x is null)
            {
                var conta = new Conta();
                conta.Id = Guid.NewGuid();
                conta.Agencia = "AG";
                conta.IdCliente = Guid.NewGuid();
                conta.Transacoes = new List<Transacao>();
                conta.Numero = numero;
                contas.Add(conta);
                return conta;
            }
            return null;*/
        }

        public List<Conta> Ler()
        {
            return _context.Contas.ToList();
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
    }
}
