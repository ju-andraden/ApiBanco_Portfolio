using Dominio.Validacao;
using Xunit;

namespace Testes.Validacao
{
    public class ValidarAgenciaAttributeTest
    {
        private ValidarAgenciaAttribute validarAgenciaAttribute = new ValidarAgenciaAttribute();

        [Theory]
        [InlineData("abcd", false)]
        [InlineData("@#$*", false)]
        [InlineData("1234", true)]
        [InlineData("", true)]
        [InlineData(null, true)]
        public void IsValid(string value, bool result)
        {
            var agenciaIsValid = validarAgenciaAttribute.IsValid(value);

            Assert.Equal(result, agenciaIsValid);
        }
    }
}