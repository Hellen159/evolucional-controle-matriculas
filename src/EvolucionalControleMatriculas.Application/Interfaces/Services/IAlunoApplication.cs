using EvolucionalControleMatriculas.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application.Interfaces.Services
{
    public interface IAlunoApplication
    {
        Task<(IReadOnlyCollection<AlunoDto> Itens, int Total)> ListarAsync(string nome, int pagina, int tamanhoPagina);

        Task<AlunoDto> ObterPorIdAsync(int id);

        Task<int> CriarAsync(AlunoGravacaoDto dto);

        Task<bool> AtualizarAsync(AlunoAtualizacaoDto dto);

        Task<bool> DesativarAsync(int id);
    }
}
