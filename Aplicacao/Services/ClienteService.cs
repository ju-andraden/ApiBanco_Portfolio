using _4_Recursos;
using Aplicacao.Interfaces;
using Dominio.Dto;
using Dominio.Entidade;
using Infraestrutura.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApiDbContext _context;

        private readonly IContaService _contaService;

        private readonly ITransacaoService _transacaoService;

        public ClienteService(ApiDbContext context, IContaService contaService, ITransacaoService transacaoService)
        {
            _context = context;
            _contaService = contaService;
            _transacaoService = transacaoService;
        }

        public async Task<Cliente> CriarCliente(CriarClienteDto criarClientDto)
        {

            Cliente cliente = new Cliente();
            cliente.Nome = criarClientDto.Nome;
            cliente.DataNascimento = criarClientDto.DataNascimento;
            cliente.Cpf = criarClientDto.Cpf;
            cliente.Telefone = criarClientDto.Telefone;
            cliente.EnderecoId = Guid.NewGuid();

            cliente.Endereco = new Endereco();
            cliente.Endereco.Logradouro = criarClientDto.Endereco.Logradouro;
            cliente.Endereco.Numero = criarClientDto.Endereco.Numero;
            cliente.Endereco.Cep = criarClientDto.Endereco.Cep;
            cliente.Endereco.Cidade = criarClientDto.Endereco.Cidade;
            cliente.Endereco.Estado = criarClientDto.Endereco.Estado;

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        public async Task<List<Cliente>> LerClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> LerCliente(string cpf)
        {
            var clienteEncontrado = await BuscarClientePeloCpf(cpf);

            if (clienteEncontrado is null)
            {
                return null;
            }

            clienteEncontrado.Endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == clienteEncontrado.EnderecoId);

            return clienteEncontrado;
        }

        public async Task<Cliente> AtualizarCliente(string cpf, AtualizarClienteDto atualizarClienteDto)
        {
            var cliente = await BuscarClientePeloCpf(cpf);

            if (cliente is null)
            {
                return null;
            }

            AtualizarClienteSemDadosNulos(cliente, atualizarClienteDto);

            //essa linha é sincrona
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return cliente;
        }

        public async Task<string> DeletarCliente(string cpf)
        {
            var cliente = await BuscarClientePeloCpf(cpf);

            if (cliente is null)
            {
                return null;
            }

            if (cliente.Contas.Any())
            {
                throw new Exception(Mensagens.RemoverClienteComConta);
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return Mensagens.RemoverCliente;
        }

        private async Task<Cliente> BuscarClientePeloCpf(string cpf)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(cliente => cliente.Cpf.Equals(cpf));

            if (cliente != null)
            {
                cliente.Contas = await _contaService.Ler(cliente.Id);

                foreach (Conta conta in cliente.Contas)
                {
                    conta.Transacoes = await _transacaoService.LerTransacoes(conta.Id, DateTime.MinValue, DateTime.MinValue);
                }
            }

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

            cliente.Endereco = new Endereco();
            cliente.Endereco.Id = cliente.EnderecoId;

            cliente.Endereco.Logradouro = atualizarClienteDto.Endereco.Logradouro;
            cliente.Endereco.Numero = atualizarClienteDto.Endereco.Numero;
            cliente.Endereco.Cep = atualizarClienteDto.Endereco.Cep;
            cliente.Endereco.Cidade = atualizarClienteDto.Endereco.Cidade;
            cliente.Endereco.Estado = atualizarClienteDto.Endereco.Estado;
        }
    }
}
