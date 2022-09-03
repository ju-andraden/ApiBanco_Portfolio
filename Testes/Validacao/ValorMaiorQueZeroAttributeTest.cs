using Dominio.Validacao;
using Xunit;

namespace Testes.Validacao
{
    public class ValorMaiorQueZeroAttributeTest
    {
       private ValorMaiorQueZeroAttribute valorMaiorQueZeroAttribute = new ValorMaiorQueZeroAttribute();

        [Theory]
        [InlineData("-1", false)]
        [InlineData("1", true)]

        public void IsValid(string value, bool result)
        {
            var valorMaiorQueZeroIsValid = valorMaiorQueZeroAttribute.IsValid(value);

            Assert.Equal(result, valorMaiorQueZeroIsValid);
        }
    }
}
