using Dominio.Validacao;
using Xunit;

namespace Testes.Validacao
{
    public class ValidarNumeroContaAttributeTest
    {
        private ValidarNumeroContaAttribute validarNumeroContaAttribute = new ValidarNumeroContaAttribute();

        [Theory]
        [InlineData("abcde-f", false)]
        [InlineData("@#$*!-@", false)]
        [InlineData("12345-6", true)]
        [InlineData("", true)]
        [InlineData(null, true)]
        public void IsValid(string value, bool result)
        {
            var numeroContaIsValid = validarNumeroContaAttribute.IsValid(value);

            Assert.Equal(result, numeroContaIsValid);
        }
    }
}
