using EvolucionalControleMatriculas.Application.DTOs;
using EvolucionalControleMatriculas.Entities;

namespace EvolucionalControleMatriculas.Application.Mappers
{
    public static class TurmaMapper
    {
        public static TurmaDto Mapear(Turma turma)
        {
            return new TurmaDto
            {
                Id = turma.Id,
                Nome = turma.Nome,
                VagasDisponiveis = turma.VagasDisponiveis
            };
        }
    }
}
