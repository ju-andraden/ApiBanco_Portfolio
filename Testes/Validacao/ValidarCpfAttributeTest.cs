using Dominio.Validacao;
using Xunit;

namespace Testes.Validacao
{
    public class ValidarCpfAttributeTest
    {
        private ValidarCpfAttribute validarCpfAttribute = new ValidarCpfAttribute();

        [Theory]
        [InlineData("abc.def.ghi-jk", false)]
        [InlineData("123.@56.789-10", false)]
        [InlineData("123456.789-10", false)]
        [InlineData("", false)]
        [InlineData("123.456.789-10", true)]
        [InlineData(null, true)]
        public void IsValid(string value, bool result)
        {
            var cpfIsValid = validarCpfAttribute.IsValid(value);

            Assert.Equal(result, cpfIsValid);
        }
    }
}
