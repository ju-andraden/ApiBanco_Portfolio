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

        public ContaService(ApiDbContext context)
        {
            _context = context;
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
            //atualizando um elemento -> encontrar o elemento na lista, modificar as propriedades e retornar o obj
            //encontrar
            //modificar
            //retornar o obj
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
            //obj conta ou nulo
            //se a conta existir -> remove
            //se a conta não existir -> mensagem de conta não encontrada
            var conta = BuscarContaPeloNumero(numero);
            if (conta is null)
            {
                return null;
            }
            _context.Contas.Remove(conta);
            _context.SaveChanges();
            return Mensagens.RemoverConta;
        }

        /// <summary>
        /// caso encontre a conta, retornará a conta
        /// caso não encontre a conta, retornará nulo
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        private Conta BuscarContaPeloNumero(string numero)
        {
            var conta = _context.Contas.FirstOrDefault(conta => conta.Numero.Equals(numero));
            return conta;
        }
    }
}
