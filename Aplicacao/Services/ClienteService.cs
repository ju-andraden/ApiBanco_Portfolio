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

        public ClienteService(ApiDbContext context, IContaService contaService,
            ITransacaoService transacaoService)
        {
            _context = context;
            _contaService = contaService;
            _transacaoService = transacaoService;
        }

        public async Task<Cliente> CriarCliente(CriarClienteDto criarClienteDto)
        {
            Cliente cliente = new Cliente();
            cliente.Nome = criarClienteDto.Nome.ToUpper();
            cliente.DataNascimento = DateTime.Parse(criarClienteDto.DataNascimento);
            cliente.Cpf = criarClienteDto.Cpf;
            cliente.Telefone = criarClienteDto.Telefone;
            cliente.EnderecoId = Guid.NewGuid();

            cliente.Endereco = new Endereco();
            cliente.Endereco.Logradouro = criarClienteDto.Endereco.Logradouro.ToUpper();
            cliente.Endereco.Numero = criarClienteDto.Endereco.Numero;
            cliente.Endereco.Cep = criarClienteDto.Endereco.Cep;
            cliente.Endereco.Cidade = criarClienteDto.Endereco.Cidade.ToUpper();
            cliente.Endereco.Estado = criarClienteDto.Endereco.Estado.ToUpper();

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

            clienteEncontrado.Endereco = await _context.Enderecos
                .FirstOrDefaultAsync(e => e.Id == clienteEncontrado.EnderecoId);

            return clienteEncontrado;
        }

        public async Task<Cliente> AtualizarCliente(string cpf, AtualizarClienteDto
            atualizarClienteDto)
        {
            var cliente = await BuscarClientePeloCpf(cpf);

            if (cliente is null)
            {
                return null;
            }

            AtualizarClienteSemDadosNulos(cliente, atualizarClienteDto);

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
            var cliente = await _context.Clientes.FirstOrDefaultAsync(cliente
                => cliente.Cpf.Equals(cpf));

            if (cliente != null)
            {
                cliente.Contas = await _contaService.Ler(cliente.Id);

                foreach (Conta conta in cliente.Contas)
                {
                    conta.Transacoes = await _transacaoService.LerTransacoes(conta.Id,
                        DateTime.MinValue, DateTime.MinValue);
                }
            }

            return cliente;
        }

        private void AtualizarClienteSemDadosNulos(Cliente cliente, AtualizarClienteDto
            atualizarClienteDto)
        {
            if (!string.IsNullOrEmpty(atualizarClienteDto.Nome))
            {
                cliente.Nome = atualizarClienteDto.Nome.ToUpper();
            }

            if (!string.IsNullOrEmpty(atualizarClienteDto.DataNascimento))
            {
                cliente.DataNascimento = DateTime.Parse(atualizarClienteDto.DataNascimento);
            }

            if (!string.IsNullOrEmpty(atualizarClienteDto.Telefone))
            {
                cliente.Telefone = atualizarClienteDto.Telefone;
            }

            //verificar se funciona sem as linhas 142 e 143, testar novamente o código
            cliente.Endereco = new Endereco();
            cliente.Endereco.Id = cliente.EnderecoId;

            cliente.Endereco = _context.Enderecos.FirstOrDefault(e => e.Id == cliente.EnderecoId);

            if (atualizarClienteDto.Endereco != null)
            {
                if (!string.IsNullOrEmpty(atualizarClienteDto.Endereco.Logradouro))
                {
                    cliente.Endereco.Logradouro = atualizarClienteDto.Endereco.Logradouro.ToUpper();
                }

                if (!string.IsNullOrEmpty(atualizarClienteDto.Endereco.Numero))
                {
                    cliente.Endereco.Numero = atualizarClienteDto.Endereco.Numero;
                }

                if (!string.IsNullOrEmpty(atualizarClienteDto.Endereco.Cep))
                {
                    cliente.Endereco.Cep = atualizarClienteDto.Endereco.Cep;
                }

                if (!string.IsNullOrEmpty(atualizarClienteDto.Endereco.Cidade))
                {
                    cliente.Endereco.Cidade = atualizarClienteDto.Endereco.Cidade.ToUpper();
                }

                if (!string.IsNullOrEmpty(atualizarClienteDto.Endereco.Estado))
                {
                    cliente.Endereco.Estado = atualizarClienteDto.Endereco.Estado.ToUpper();
                }
            }
        }
    }
}