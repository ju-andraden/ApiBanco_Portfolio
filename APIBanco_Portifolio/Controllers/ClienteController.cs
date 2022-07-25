using _4_Recursos;
using Aplicacao.Interfaces;
using Dominio.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Apresentacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost("CriarCliente")]
        public IActionResult ClienteCriado([FromBody] CriarClienteDto criarClienteDto)
        {
            try
            {
                var clienteCriado = _clienteService.CriarCliente(criarClienteDto);

                return Created($"/{clienteCriado.Id}", clienteCriado);
            }

            catch (Exception excecao)
            {
                var mensagem = excecao.InnerException.Message;

                if (mensagem.StartsWith("Duplicate"))
                {
                    return BadRequest(Mensagens.ClienteExiste);
                }

                return BadRequest(mensagem);
            }
        }

        [HttpGet("BuscarTodosOsClientes")]
        public IActionResult LeiaClientes()
        {
            var leiaClientes = _clienteService.LerClientes();

            return Ok(leiaClientes);
        }

        [HttpGet("BuscarClientePeloCpf")]
        public IActionResult LeiaCliente(string cpf)
        {
            var leiaCliente = _clienteService.LerCliente(cpf);

            if (leiaCliente is null)
            {
                return NotFound(Mensagens.ClienteNaoEncontrado);
            }
            return Ok(leiaCliente);
        }

        [HttpPut("AtualizarCliente")]
        public IActionResult AtualizandoCliente(string cpf, [FromBody] AtualizarClienteDto atualizarClienteDto)
        {
            var atualizandoCliente = _clienteService.AtualizarCliente(cpf, atualizarClienteDto);

            if (atualizandoCliente is null)
            {
                return NotFound(Mensagens.ClienteNaoEncontrado);
            }

            return Ok(atualizandoCliente);
        }

        [HttpDelete("DeletarCliente")]
        public IActionResult DeletandoCliente(string cpf)
        {
            try
            {
                var deletandoCliente = _clienteService.DeletarCliente(cpf);

                if (deletandoCliente is null)
                {
                    return NotFound(Mensagens.ClienteNaoEncontrado);
                }

                return Ok(deletandoCliente);
            }

            catch (Exception excecao)
            {
                return BadRequest(excecao.Message);
            }
            
        }
    }
}
