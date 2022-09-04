using Aplicacao.Interfaces;
using Aplicacao.Services;
using Dominio.Entidade;
using Infraestrutura.DataBase;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Testes.Services
{
    public class ClienteServiceTest
    {
        private readonly ApiDbContext _apiDbContext;
        private readonly IContaService _contaService;
        private readonly ITransacaoService _transacaoService;

        private Task<List<Cliente>> clientes;
        private DbContextOptions<ApiDbContext> options;

        public ClienteServiceTest()
        {
            //Substitute simula o que a interface faz
            _contaService = Substitute.For<IContaService>();
            _transacaoService = Substitute.For<ITransacaoService>();

            //DbContextOptionsBuilder db em memória, simula as informações do db
            //cria um contexto de um db "fake"
            options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: "ApiBancoDB")
            .Options;

            //_clienteService = new ClienteService(_apiDbContext, _contaService, _transacaoService);
        }

        [Fact]
        public async Task LerClientes()
        {
            //configurar            
            MockLerClientes();

            //executar

            using (var context = new ApiDbContext(options))
            {
                ClienteService _clienteService = new ClienteService(context, _contaService, _transacaoService);
                clientes = _clienteService.LerClientes();                
            }

            //validar
            Assert.NotNull(clientes);
            Assert.Equal(1, clientes.Result.Count);
            Assert.Equal(Guid.Parse("08da8b5c-f701-40b5-8e23-c5b4eed40c74"), clientes.Result[0].Id);
            Assert.Equal(Guid.Parse("08da8b5c-f70a-44a6-8546-c71a40f239c5"), clientes.Result[0].EnderecoId);
            Assert.Equal("JULIANA", clientes.Result[0].Nome);
            Assert.NotNull(clientes.Result[0].DataNascimento);
            Assert.Equal("505.757.518-00", clientes.Result[0].Cpf);
            Assert.Equal("(11)98504-5363", clientes.Result[0].Telefone);
        }

        private void MockLerClientes()
        {
            //Insere os dados dentro do db usando uma instância de contexto e salva o cliente na tabela
            using (var context = new ApiDbContext(options))
            {
                context.Clientes.Add(new Cliente
                {
                    Id = Guid.Parse("08da8b5c-f701-40b5-8e23-c5b4eed40c74"),
                    EnderecoId = Guid.Parse("08da8b5c-f70a-44a6-8546-c71a40f239c5"),
                    Nome = "JULIANA",
                    DataNascimento = DateTime.Parse("1999-10-07"),
                    Cpf = "505.757.518-00",
                    Telefone = "(11)98504-5363"
                });
                context.SaveChanges();
            }
        }
    }
}
