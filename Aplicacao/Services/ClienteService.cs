using _4_Recursos;
using Aplicacao.Interfaces;
using Dominio.Dto;
using Infraestrutura.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApiDbContext _context;

        public ClienteService(ApiDbContext context)
        {
            _context = context;
        }

        public Cliente CriarCliente(CriarClienteDto criarClientDto)
        {
            criarClientDto.Endereco.Id = Guid.NewGuid();

            Cliente cliente = new Cliente();
            cliente.Nome = criarClientDto.Nome;
            cliente.DataNascimento = criarClientDto.DataNascimento;
            cliente.Cpf = criarClientDto.Cpf;
            cliente.Telefone = criarClientDto.Telefone;
            cliente.EnderecoId = criarClientDto.Endereco.Id;
            cliente.Endereco = criarClientDto.Endereco;

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return cliente;
        }

        public List<Cliente> LerClientes()
        {
            return _context.Clientes.ToList();
        }

        public Cliente LerCliente(string cpf)
        {
            var clienteEncontrado = BuscarClientePeloCpf(cpf);
            if (clienteEncontrado is null)
            {
                return null;
            }

            clienteEncontrado.Endereco = _context.Enderecos.FirstOrDefault(e => e.Id == clienteEncontrado.EnderecoId);

            return clienteEncontrado;
        }

        public Cliente AtualizarCliente(string cpf, AtualizarClienteDto atualizarClienteDto)
        {
            var cliente = BuscarClientePeloCpf(cpf);
            if (cliente is null)
            {
                return null;
            }

            AtualizarClienteSemDadosNulos(cliente, atualizarClienteDto);

            _context.Entry(cliente).State = EntityState.Modified;
            _context.SaveChanges();
            return cliente;
        }

        public string DeletarCliente(string cpf)
        {
            var cliente = BuscarClientePeloCpf(cpf);
            if (cliente is null)
            {
                return null;
            }
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return Mensagens.RemoverCliente;
        }

        private Cliente BuscarClientePeloCpf(string cpf)
        {
            var cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Cpf.Equals(cpf));
            return cliente;
        }
        private void AtualizarClienteSemDadosNulos(Cliente cliente, AtualizarClienteDto atualizarClienteDto)
        {
            if (atualizarClienteDto.Nome != null)
            {
                cliente.Nome = atualizarClienteDto.Nome;
            }

            if (atualizarClienteDto.DataNascimento != null)
            {
                cliente.DataNascimento = atualizarClienteDto.DataNascimento;
            }

            if (atualizarClienteDto.Telefone != null)
            {
                cliente.Telefone = atualizarClienteDto.Telefone;
            }
        }
    }
}
