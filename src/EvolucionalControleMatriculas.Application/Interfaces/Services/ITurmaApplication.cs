using EvolucionalControleMatriculas.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application.Interfaces.Services
{
    public interface ITurmaApplication
    {
        Task<IReadOnlyCollection<Turma>> ListarAsync();

    }
}
