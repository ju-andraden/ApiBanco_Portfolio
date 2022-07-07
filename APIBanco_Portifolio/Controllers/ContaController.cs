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
        public IActionResult Criado(string numero)
        {
            var resultado = _contaService.Criar(numero);
            if (resultado is null)
            {
                return Unauthorized();
            }
            return Created($"/{numero}", resultado);
        }

        [HttpGet("BuscarTodasAsContas")]
        public IActionResult Leia()
        {
            var resultado = _contaService.Ler();
            return Ok(resultado);
        }

        [HttpGet("BuscarContaPeloNumero")]
        public IActionResult Leia(string numero)
        {
            var resultado = _contaService.Ler(numero);
            if (resultado is null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

        [HttpPut("AtualizarConta")]
        public IActionResult Atualizado(string numeroConta, [FromBody] Conta novosDados)
        {
            var resultado = _contaService.Atualizar(numeroConta, novosDados);
            return Ok(resultado);
        }

        [HttpDelete("DeletarConta")]
        public IActionResult Deletando(string numero)
        {
            var resultado = _contaService.Deletar(numero);
            if (resultado is null)
            {
                return NotFound(Mensagens.ContaNaoEncontrada);
            }
            return Ok(resultado);
        }
    }
}
