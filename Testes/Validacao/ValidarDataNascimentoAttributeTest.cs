using _4_Recursos;
using Dominio.Validacao;
using Xunit;

namespace Testes.Validacao
{
    public class ValidarDataNascimentoAttributeTest
    {
        private ValidarDataNascimentoAttribute validarDataNascimentoAttribute = 
            new ValidarDataNascimentoAttribute();

        [Theory]
        [InlineData("abcd-ef-gh", false)]
        [InlineData("@*%/-10-07", false)]
        [InlineData("1999-1007", false)]
        [InlineData("2050-01-01", false)]
        [InlineData("2012-01-01", false)]
        [InlineData("1999-10-07", true)]
        [InlineData("", true)]
        [InlineData(null, true)]

        public void IsValid(string value, bool result)
        {
            var dataNascimento = validarDataNascimentoAttribute.IsValid(value);

            Assert.Equal(result, dataNascimento);
        }
    }
}
