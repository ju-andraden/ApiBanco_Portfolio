using _4_Recursos;
using Aplicacao.Interfaces;
using Dominio.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Apresentacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _contaService;

        public ContaController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpPost("CriarConta")]
        public async Task<IActionResult> Criado([FromBody] CriarContaDto criarContaDto)
        {
            try
            {
                var contaCriada = await _contaService.Criar(criarContaDto);

                if (contaCriada is null)
                {
                    return Unauthorized(Mensagens.ClienteNaoEncontrado);
                }
                return Created($"/{criarContaDto.ClienteId}", contaCriada);
            }
            catch (Exception excecao)
            {
                return BadRequest(Mensagens.ContaExistente);
            }
        }

        [HttpGet("BuscarTodasAsContas")]
        public async Task<IActionResult> LeiaContas()
        {
            var resultado = await _contaService.Ler();

            return Ok(resultado);
        }

        [HttpGet("BuscarContaPeloNumero")]
        public async Task<IActionResult> Leia(string numeroConta)
        {
            var resultado = await _contaService.Ler(numeroConta);

            if (resultado is null)
            {
                return NotFound(Mensagens.ContaNaoEncontrada);
            }
            return Ok(resultado);
        }

        [HttpPut("AtualizarConta")]
        public async Task<IActionResult> Atualizado(string numeroConta,
            [FromBody] AtualizarContaDto atualizarContaDto)
        {
            var atualizandoConta = await _contaService.AtualizarConta(numeroConta,
                atualizarContaDto);

            if (atualizandoConta is null)
            {
                return BadRequest(Mensagens.ContaNaoEncontrada);
            }
            return Ok(atualizandoConta);
        }

        [HttpDelete("DeletarConta")]
        public async Task<IActionResult> Deletando(string numeroConta)
        {
            var resultado = await _contaService.Deletar(numeroConta);

            if (resultado is null)
            {
                return NotFound(Mensagens.ContaNaoEncontrada);
            }
            return Ok(resultado);
        }
    }
}