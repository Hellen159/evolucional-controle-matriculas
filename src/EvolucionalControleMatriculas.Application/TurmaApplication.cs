using EvolucionalControleMatriculas.Application.DTOs;
using EvolucionalControleMatriculas.Application.Interfaces;
using EvolucionalControleMatriculas.Application.Interfaces.Services;
using EvolucionalControleMatriculas.Application.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application
{
    public class TurmaApplication : ITurmaApplication
    {
        private readonly ITurmaRepository _repository;

        public TurmaApplication(ITurmaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<TurmaDto>> ListarAsync()
        {
            var turmas = await _repository.ListarAsync();
            return turmas.Select(TurmaMapper.Mapear).ToList();
        }
    }
}
