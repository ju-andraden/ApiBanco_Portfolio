using _4_Recursos;
using Aplicacao.Interfaces;
using Aplicacao.Services;
using Dominio.Dto;
using Dominio.Entidade;
using Dominio.Enum;
using Microsoft.AspNetCore.Http;
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
        public IActionResult TransacaoCriada([FromBody] CriarTransacaoDto criarTransacaoDto)
        {
            try
            {
                var transacaoCriada = _transacaoService.CriarTransacao(criarTransacaoDto);

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
        public IActionResult LeiaTransacoes(Guid id,DateTime dataInicio, DateTime dataFim)
        {
            var leiaTransacoes = _transacaoService.LerTransacoes(id, dataInicio, dataFim);
            return Ok(leiaTransacoes);
        }

        [HttpGet("BuscarTransacaoPeloId")]
        public IActionResult LeiaTransacao(Guid id)
        {
            var leiaTransacao = _transacaoService.LerTransacao(id);
            if (leiaTransacao is null)
            {
                return NotFound(Mensagens.TransacaoNaoEncontrada);
            }
            return Ok(leiaTransacao);
        }
    }
}

