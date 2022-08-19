using _4_Recursos;
using Aplicacao.Interfaces;
using Apresentacao.Controllers;
using Dominio.Dto;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Testes.Controllers
{
    public class ContaControllerTest
    {
        private readonly ContaController _contaController;
        public ContaControllerTest()
        {
            var iContaService = Substitute.For<IContaService>();

            _contaController = new ContaController(iContaService);
        }

        [Fact]
        public async Task CriadoReturnUnauthorized()
        {
            //configurar
            var conta = new CriarContaDto();

            //executar
            var response = (UnauthorizedObjectResult)await _contaController.Criado(conta);

            //validar
            Assert.NotNull(response);
            Assert.Equal(401, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ClienteNaoEncontrado, response.Value);
        }
    }
}
