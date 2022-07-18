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

            Cliente cliente = new Cliente();
            cliente.Nome = criarClientDto.Nome;
            cliente.DataNascimento = criarClientDto.DataNascimento;
            cliente.Cpf = criarClientDto.Cpf;
            cliente.Telefone = criarClientDto.Telefone;
            cliente.EnderecoId = Guid.NewGuid();

            cliente.Endereco = new Dominio.Entidade.Endereco();
            cliente.Endereco.Logradouro = criarClientDto.Endereco.Logradouro;
            cliente.Endereco.Numero = criarClientDto.Endereco.Numero;
            cliente.Endereco.Cep = criarClientDto.Endereco.Cep;
            cliente.Endereco.Cidade = criarClientDto.Endereco.Cidade;
            cliente.Endereco.Estado = criarClientDto.Endereco.Estado;

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

            if (atualizarClienteDto.Endereco != null)
            {
                cliente.Endereco = new Dominio.Entidade.Endereco();
                cliente.Endereco.Id = cliente.EnderecoId;

                if (atualizarClienteDto.Endereco.Logradouro != null)
                {
                    cliente.Endereco.Logradouro = atualizarClienteDto.Endereco.Logradouro;
                }

                if (atualizarClienteDto.Endereco.Numero != null)
                {
                    cliente.Endereco.Numero = atualizarClienteDto.Endereco.Numero;
                }

                if (atualizarClienteDto.Endereco.Cep != null)
                {
                    cliente.Endereco.Cep = atualizarClienteDto.Endereco.Cep;
                }

                if (atualizarClienteDto.Endereco.Cidade != null)
                {
                    cliente.Endereco.Cidade = atualizarClienteDto.Endereco.Cidade;
                }

                if (atualizarClienteDto.Endereco.Estado != null)
                {
                    cliente.Endereco.Estado = atualizarClienteDto.Endereco.Estado;
                }
            }
        }
    }
}
