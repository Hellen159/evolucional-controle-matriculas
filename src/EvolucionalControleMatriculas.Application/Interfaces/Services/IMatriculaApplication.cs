using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application.Interfaces.Services
{
    public interface IMatriculaApplication
    {
        Task<bool> MatricularAsync(int alunoId, int turmaId);
    }
}
