using Dominio.Validacao;
using Xunit;

namespace Testes.Validacao
{
    public class ValidarEstadoAttributeTest
    {
        private ValidarEstadoAttribute validarEstadoAttribute = new ValidarEstadoAttribute();

        [Theory]
        [InlineData("ab", false)]
        [InlineData("12", false)]
        [InlineData("#$", false)]
        [InlineData("sp", true)]
        [InlineData("SP", true)]
        [InlineData("", true)]
        [InlineData(null, true)]

        public void IsValid(string value, bool result)
        {
            var estadoIsValid = validarEstadoAttribute.IsValid(value);

            Assert.Equal(result, estadoIsValid);
        }
    }
}
