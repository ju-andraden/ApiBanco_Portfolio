using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Apresentacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpPost]
        public IActionResult Post(Weather wt)
        {
            return Ok();
        }
    }

    public class Weather
    {
        public int? Valor { get; set; }
        public string Nome { get; set; }
    }

    public class WeatherValidator : AbstractValidator<Weather>
    {
        public WeatherValidator()
        {
            RuleFor(x => x.Valor)
                .NotEmpty().WithMessage("Mensagem de aleluia")
                .GreaterThan(10).WithMessage("Maior que 10 mano brow");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Se esse cara é vazio ou espaço");
        }
    }
}

