using EvolucionalControleMatriculas.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application.Interfaces.Repositories
{
    public interface IRelatorioRepository
    {
        Task<IReadOnlyCollection<AlunosPorTurmaDto>> ListarAlunosPorTurmaAsync();
    }
}
