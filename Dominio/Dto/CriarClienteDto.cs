﻿using Dominio.Validacao;

namespace Dominio.Dto
{
    public class CriarClienteDto
    {
        [ValidarNome]
        public string? Nome { get; set; }

        [ValidarDataNascimento]
        public string? DataNascimento { get; set; }

        [ValidarCpf]
        public string? Cpf { get; set; }
        public string? Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}