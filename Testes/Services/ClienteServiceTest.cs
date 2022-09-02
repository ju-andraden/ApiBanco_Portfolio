using Aplicacao.Interfaces;
using Aplicacao.Services;
using Infraestrutura.DataBase;
using NSubstitute;

namespace Testes.Services
{
    public class ClienteServiceTest
    {
        private readonly ApiDbContext _apiDbContext;
        private readonly IContaService _contaService;
        private readonly ITransacaoService _transacaoService;
        private readonly IClienteService _clienteService;

        public ClienteServiceTest()
        {
            _apiDbContext = Substitute.For<ApiDbContext>();
            _contaService = Substitute.For<IContaService>();
            _transacaoService = Substitute.For<ITransacaoService>();

            _clienteService = new ClienteService(_apiDbContext, _contaService, _transacaoService);
        }

    }
}
