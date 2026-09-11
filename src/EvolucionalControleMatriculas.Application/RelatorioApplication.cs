using EvolucionalControleMatriculas.Application.DTOs;
using EvolucionalControleMatriculas.Application.Interfaces.Repositories;
using EvolucionalControleMatriculas.Application.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application
{
    public class RelatorioApplication : IRelatorioApplication
    {
        private readonly IRelatorioRepository _repository;

        public RelatorioApplication(IRelatorioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<AlunosPorTurmaDto>> ListarAlunosPorTurmaAsync()
        {
            return await _repository.ListarAlunosPorTurmaAsync();
        }
    }
}
