using EvolucionalControleMatriculas.Application.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application
{
    public class MatriculaApplication : IMatriculaApplication
    {
        public Task<bool> MatricularAsync(int alunoId, int turmaId)
        {
            throw new NotImplementedException();
        }
    }
}
