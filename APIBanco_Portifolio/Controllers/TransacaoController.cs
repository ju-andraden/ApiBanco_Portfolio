using _4_Recursos;
using Aplicacao.Interfaces;
using Dominio.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Apresentacao.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpPost("CriarTransacao")]
        public async Task<IActionResult> TransacaoCriada([FromBody] CriarTransacaoDto criarTransacaoDto)
        {
            try
            {
                var transacaoCriada = await _transacaoService.CriarTransacao(criarTransacaoDto);

                if (transacaoCriada is null)
                {
                    return Unauthorized(Mensagens.ContaNaoEncontrada);
                }

                return Created($"/{transacaoCriada.Id}", transacaoCriada);
            }

            catch (Exception excecao)
            {
                return BadRequest(excecao.Message);
            }
        }

        [HttpGet("BuscarTodasAsTransacoes")]
        public async Task<IActionResult> LeiaTransacoes(Guid id, DateTime dataInicio, DateTime dataFim)
        {
            var leiaTransacoes = await _transacaoService.LerTransacoes(id, dataInicio, dataFim);
            return Ok(leiaTransacoes);
        }

        [HttpGet("BuscarTransacaoPeloId")]
        public async Task<IActionResult> LeiaTransacao(Guid id)
        {
            var leiaTransacao = await _transacaoService.LerTransacao(id);

            if (leiaTransacao is null)
            {
                return NotFound(Mensagens.TransacaoNaoEncontrada);
            }
            return Ok(leiaTransacao);
        }
    }
}

