using EvolucionalControleMatriculas.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application.Interfaces
{
    public interface ITurmaRepository
    {
        Task<IReadOnlyCollection<Turma>> ListarAsync();
    }
}
