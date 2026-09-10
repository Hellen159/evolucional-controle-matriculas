using EvolucionalControleMatriculas.Entities;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application.Interfaces
{
    public interface IMatriculaRepository
    {
        Task<bool> ExisteAsync(int alunoId, int turmaId);
        Task<bool> MatricularAsync(Matricula matricula);
    }
}
