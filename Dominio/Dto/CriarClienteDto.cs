﻿namespace Dominio.Dto
{
    public class CriarClienteDto
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string? Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}
