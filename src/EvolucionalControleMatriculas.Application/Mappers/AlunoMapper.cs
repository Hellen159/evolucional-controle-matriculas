using EvolucionalControleMatriculas.Application.DTOs;
using EvolucionalControleMatriculas.Entities;

namespace EvolucionalControleMatriculas.Application.Mappers
{
    public static class AlunoMapper
    {
        public static AlunoDto Mapear(Aluno aluno)
        {
            return new AlunoDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Email = aluno.Email,
                DataNascimento = aluno.DataNascimento,
                Ativo = aluno.Ativo
            };
        }
    }
}
