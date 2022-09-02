using Dominio.Validacao;
using Xunit;

namespace Testes.Validacao
{
    public class ValidarCepAttributeTest
    {
        private ValidarCepAttribute validarCepAttribute = new ValidarCepAttribute();

        [Theory]
        [InlineData("abcd", false)]
        [InlineData("@#$*", false)]
        [InlineData("12345678", false)]
        [InlineData("12345-678", true)]
        [InlineData("", true)]
        [InlineData(null, true)]
        public void IsValid(string value, bool result)
        {
            var cepIsValid = validarCepAttribute.IsValid(value);

            Assert.Equal(result, cepIsValid);
        }
    }
}
