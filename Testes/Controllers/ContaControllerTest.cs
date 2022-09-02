using _4_Recursos;
using Aplicacao.Interfaces;
using Apresentacao.Controllers;
using Dominio.Dto;
using Dominio.Entidade;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Testes.Controllers
{
    public class ContaControllerTest
    {
        private readonly ContaController _contaController;
        private readonly IContaService _contaService;
        public ContaControllerTest()
        {
            _contaService = Substitute.For<IContaService>();

            _contaController = new ContaController(_contaService);
        }

        [Fact]
        public async Task CriadoReturnUnauthorized()
        {
            //configurar
            var criarContaDto = InstanciarUmaContaDto();

            //executar
            var response = (UnauthorizedObjectResult)await _contaController.Criado(criarContaDto);

            //validar
            Assert.NotNull(response);
            Assert.IsType<UnauthorizedObjectResult>(response);
            Assert.Equal(401, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ClienteNaoEncontrado, response.Value);
        }

        [Fact]
        public async Task CriadoReturnBadRequest()
        {
            //configurar
            var criarContaDto = InstanciarUmaContaDto();

            //para lançar a exceção via Substitute com método async
            _contaService.Criar(criarContaDto).Returns(Task.FromException<Conta>(new Exception("some error")));

            //executar
            var response = (BadRequestObjectResult)await _contaController.Criado(criarContaDto);

            //validar
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ContaExistente, response.Value);
        }

        [Fact]
        public async Task CriadoReturnOk()
        {
            //configurar
            var criarContaDto = InstanciarUmaContaDto();
            var contaEsperada = InstanciarUmaConta();

            _contaService.Criar(criarContaDto).Returns(Task.FromResult(contaEsperada));

            //executar
            var response = (CreatedResult)await _contaController.Criado(criarContaDto);

            //validar
            Assert.NotNull(response);
            Assert.IsType<CreatedResult>(response);
            Assert.Equal(201, response.StatusCode.GetValueOrDefault());
            Assert.Equal(contaEsperada, response.Value);
        }

        [Fact]
        public async Task LeiaTodasAsContasReturnOk()
        {
            //configurar
            var listarContas = ListarContas();

            _contaService.Ler().Returns(listarContas);

            //executar
            var response = (OkObjectResult)await _contaController.LeiaContas();

            //validar
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode.GetValueOrDefault());
            Assert.Equal(listarContas, response.Value);
        }

        [Fact]
        public async Task LeiaContaPeloNumeroReturnNotFound()
        {
            //configurar
            string conta = "";

            _contaService.Ler(conta).ReturnsNull();

            //executar
            var response = (NotFoundObjectResult)await _contaController.Leia(conta);

            //validar
            Assert.NotNull(response);
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ContaNaoEncontrada, response.Value);
        }

        [Fact]
        public async Task LeiaContaPeloNumeroReturnOk()
        {
            //configurar
            var contaEsperada = InstanciarUmaConta();
            string conta = "12345-6";

            _contaService.Ler(conta).Returns(contaEsperada);

            //executar
            var response = (OkObjectResult)await _contaController.Leia(conta);

            //validar
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode.GetValueOrDefault());
            Assert.Equal(contaEsperada, response.Value);
        }

        [Fact]
        public async Task AtualizadoReturnBadRequest()
        {
            //configurar
            string conta = "";

            _contaService.AtualizarConta(conta, null).ReturnsNull();

            //executar
            var response = (BadRequestObjectResult)await _contaController.Atualizado(conta, null);

            //validar
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ContaNaoEncontrada, response.Value);
        }

        [Fact]
        public async Task AtualizadoReturnOk()
        {
            //configurar
            var atualizarContaDto = AtualizarUmaContaDto();
            var contaAtualizada = InstanciarUmaConta();
            string conta = "12345-6";
            
            _contaService.AtualizarConta(conta, atualizarContaDto).Returns(contaAtualizada);

            //executar
            var response = (OkObjectResult)await _contaController.Atualizado(conta, atualizarContaDto);

            //validar
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode.GetValueOrDefault());
            Assert.Equal(contaAtualizada, response.Value);
        }

        [Fact]
        public async Task DeletandoReturnNotFound()
        {
            //configurar
            string conta = "";

            _contaService.Deletar(conta).ReturnsNull();

            //executar
            var response = (NotFoundObjectResult)await _contaController.Deletando(conta);

            //validar
            Assert.NotNull(response);
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ContaNaoEncontrada, response.Value);
        }

        [Fact]
        public async Task DeletandoReturnOk()
        {
            //configurar
            string conta = "12345-6";

            _contaService.Deletar(conta).Returns(Mensagens.RemoverConta);

            //executar
            var response = (OkObjectResult)await _contaController.Deletando(conta);

            //validar
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.RemoverConta, response.Value);
        }

        private CriarContaDto InstanciarUmaContaDto()
        {
            CriarContaDto criarContaDto = new CriarContaDto();
            criarContaDto.ClienteId = Guid.Parse("08da8b5c-f701-40b5-8e23-c5b4eed40c74");
            criarContaDto.NumeroConta = "12345-6";
            criarContaDto.Agencia = "1234";

            return criarContaDto;
        }

        private Conta InstanciarUmaConta()
        {
            Conta conta = new Conta();
            conta.ClienteId = Guid.Parse("08da8b5c-f701-40b5-8e23-c5b4eed40c74");
            conta.Numero = "12345-6";
            conta.Agencia = "1234";

            return conta;
        }

        private List<Conta> ListarContas()
        {
            return new List<Conta>()
            {
                new Conta()
                {
                    Id = Guid.NewGuid(),
                    Agencia = "1234",
                    Transacoes = new List<Transacao>()
                    {
                        new Transacao()
                        {
                            Id= Guid.NewGuid()
                        }
                    },
                    Numero = "789-0",
                    ClienteId = Guid.NewGuid()
                }
            };
        }

        private AtualizarContaDto AtualizarUmaContaDto()
        {
            AtualizarContaDto atualizarContaDto = new AtualizarContaDto();
            atualizarContaDto.NumeroConta = "12345-6";
            atualizarContaDto.Agencia = "1234";

            return atualizarContaDto;
        }
    }
}