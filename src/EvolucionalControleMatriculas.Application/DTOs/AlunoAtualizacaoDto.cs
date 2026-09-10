using System;

namespace EvolucionalControleMatriculas.Application.DTOs
{
    public class AlunoAtualizacaoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
