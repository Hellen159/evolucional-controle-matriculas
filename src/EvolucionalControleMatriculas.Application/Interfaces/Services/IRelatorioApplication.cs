using EvolucionalControleMatriculas.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application.Interfaces.Services
{
    public interface IRelatorioApplication
    {
        Task<IReadOnlyCollection<AlunosPorTurmaDto>> ListarAlunosPorTurmaAsync();
    }
}
