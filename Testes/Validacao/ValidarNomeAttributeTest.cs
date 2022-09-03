using Dominio.Validacao;
using Xunit;

namespace Testes.Validacao
{
    public class ValidarNomeAttributeTest
    {
        private ValidarNomeAttribute validarNomeAttribute = new ValidarNomeAttribute();

        [Theory]
        [InlineData("Julian4", false)]
        [InlineData("Julian@", false)]
        [InlineData("juliana", false)]
        [InlineData("Juliana", true)]
        [InlineData("", true)]
        [InlineData(null, true)]
        public void IsValid(string value, bool result)
        {
            var nomeIsValid = validarNomeAttribute.IsValid(value);

            Assert.Equal(result, nomeIsValid);
        }
    }
}
