using Dominio.Validacao;
using Xunit;

namespace Testes.Validacao
{
    public class ValidarTelefoneAttributeTest
    {
        private ValidarTelefoneAttribute validarTelefoneAttribute = new ValidarTelefoneAttribute();

        [Theory]
        [InlineData("(ab)cdef-ghij", false)]
        [InlineData("(11)98504-5363", true)]
        [InlineData("(11)8504-5363", true)]
        [InlineData("", true)]
        [InlineData(null, true)]
        public void IsValid(string value, bool result)
        {
            var telefoneIsValid = validarTelefoneAttribute.IsValid(value);

            Assert.Equal(result, telefoneIsValid);
        }
    }
}
