using EvolucionalControleMatriculas.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application.Interfaces.Services
{
    public interface ITurmaApplication
    {
        Task<IReadOnlyCollection<TurmaDto>> ListarAsync();
    }
}
