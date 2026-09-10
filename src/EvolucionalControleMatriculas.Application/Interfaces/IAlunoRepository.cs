using EvolucionalControleMatriculas.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application.Interfaces
{
    public interface IAlunoRepository
    {

        Task<(IReadOnlyCollection<Aluno> Itens, int Total)> ListarAsync(string nome, int pagina, int tamanhoPagina);
        Task<Aluno> ObterPorIdAsync(int id);
        Task<int> InserirAsync(Aluno aluno);
        Task AtualizarAsync(Aluno aluno);
        Task DesativarAsync(int id);
    }
}
