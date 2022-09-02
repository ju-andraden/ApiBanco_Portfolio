using Aplicacao.Interfaces;
using Apresentacao.Controllers;
using NSubstitute;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using _4_Recursos;
using Dominio.Dto;
using Dominio.Entidade;
using NSubstitute.ReturnsExtensions;

namespace Testes.Controllers
{
    public class TransacaoControllerTest
    {
        private readonly TransacaoController _transacaoController;
        private readonly ITransacaoService _transacaoService;

        public TransacaoControllerTest()
        {
            _transacaoService = Substitute.For<ITransacaoService>();

            _transacaoController = new TransacaoController(_transacaoService);
        }

        [Fact]
        public async Task TransacaoCriadaReturnUnauthorized()
        {
            //configurar
            var criarTransacaoDto = InstanciarUmaTransacaoDto();

            //executar
            var response = (UnauthorizedObjectResult)await _transacaoController.TransacaoCriada(criarTransacaoDto);

            //validar
            Assert.NotNull(response);
            Assert.IsType<UnauthorizedObjectResult>(response);
            Assert.Equal(401, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ContaNaoEncontrada, response.Value);
        }

        [Fact]
        public async Task TransacaoCriadaReturnBadRequest()
        {
            //configurar
            var criarTransacaoDto = InstanciarUmaTransacaoDtoInvalida();

            _transacaoService.CriarTransacao(criarTransacaoDto).Returns(Task.FromException<Transacao>(new Exception
                (Mensagens.TransacaoInvalida)));

            //executar
            var response = (BadRequestObjectResult)await _transacaoController.TransacaoCriada(criarTransacaoDto);

            //validar
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.TransacaoInvalida, response.Value);
        }

        [Fact]
        public async Task TransacaoCriadaReturnOk()
        {
            //configurar
            var criarTransacaoDto = InstanciarUmaTransacaoDto();
            var transacaoEsperada = InstanciarUmaTransacao();

            _transacaoService.CriarTransacao(criarTransacaoDto).Returns(Task.FromResult(transacaoEsperada));

            //executar
            var response = (CreatedResult)await _transacaoController.TransacaoCriada(criarTransacaoDto);
            
            //validar
            Assert.NotNull(response);
            Assert.IsType<CreatedResult>(response);
            Assert.Equal(201, response.StatusCode.GetValueOrDefault());
            Assert.Equal(transacaoEsperada, response.Value);
        }

        [Fact]
        public async Task LeiaTodasAsTransacoesReturnOk()
        {
            //configurar
            var listarTransacoes = ListarTransacoes(); 
            Guid id = Guid.NewGuid();
            DateTime dataInicio = DateTime.Parse("2022-07-12T09:36:00");
            DateTime dataFim = DateTime.Now;

            _transacaoService.LerTransacoes(id, dataInicio, dataFim).Returns(listarTransacoes);

            //executar
            var response = (OkObjectResult)await _transacaoController.LeiaTransacoes(id, dataInicio, dataFim);

            //validar
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode.GetValueOrDefault());
            Assert.Equal(listarTransacoes, response.Value);
        }

        [Fact]
        public async Task LeiaTransacaoPeloIdReturnNotFound()
        {
            //configura
            Guid transacao = Guid.NewGuid();

            _transacaoService.LerTransacao(transacao).ReturnsNull();

            //executar
            var response = (NotFoundObjectResult)await _transacaoController.LeiaTransacao(transacao);


            //validar
            Assert.NotNull(response);
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.TransacaoNaoEncontrada, response.Value);
        }

        [Fact]
        public async Task LeiaTransacaoPeloIdReturnOk()
        {
            //configurar
            var transacaoEsperada = InstanciarUmaTransacao();
            Guid transacao = Guid.NewGuid();

            _transacaoService.LerTransacao(transacao).Returns(transacaoEsperada);

            //executar
            var response = (OkObjectResult)await _transacaoController.LeiaTransacao(transacao);

            //validar
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, response.StatusCode.GetValueOrDefault());
            Assert.Equal(transacaoEsperada, response.Value);
        }

        private CriarTransacaoDto InstanciarUmaTransacaoDto()
        {
            CriarTransacaoDto criarTransacaoDto = new CriarTransacaoDto();
            criarTransacaoDto.ContaId = Guid.Parse("08da83d4-edda-4f85-820c-c7a143036701");
            criarTransacaoDto.Valor = 10;
            criarTransacaoDto.TipoTransacao = Dominio.Enum.TipoTransacao.Doc;

            return criarTransacaoDto;
        }

        private Transacao InstanciarUmaTransacao()
        {
            Transacao transacao = new Transacao();
            transacao.ContaId = Guid.Parse("08da83d4-edda-4f85-820c-c7a143036701");
            transacao.Valor = 10;
            transacao.TipoTransacao = "4";

            return transacao;
        }

        private CriarTransacaoDto InstanciarUmaTransacaoDtoInvalida()
        {
            CriarTransacaoDto criarTransacaoDto = new CriarTransacaoDto();
            criarTransacaoDto.ContaId = Guid.Parse("08da83d4-edda-4f85-820c-c7a143036701");
            criarTransacaoDto.Valor = 10;
            criarTransacaoDto.TipoTransacao = (Dominio.Enum.TipoTransacao) 9;

            return criarTransacaoDto;
        }

        private List<Transacao> ListarTransacoes()
        {
            return new List<Transacao>()
            {
                new Transacao()
                {
                    Id = Guid.NewGuid(),
                    ContaId = Guid.NewGuid(),
                    Descricao = Mensagens.DocRealizado,
                    DataHora = DateTime.Now,
                    Valor = 500,
                    TipoTransacao = Dominio.Enum.TipoTransacao.Doc.ToString(),
                }
            };
        }
    }
}
