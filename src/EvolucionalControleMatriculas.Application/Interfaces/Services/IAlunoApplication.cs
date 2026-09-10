using EvolucionalControleMatriculas.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application.Interfaces.Services
{
    public interface IAlunoApplication
    {
        Task<(IReadOnlyCollection<Aluno> Itens, int Total)> ListarAsync(string nome, int pagina, int tamanhoPagina);

        Task<Aluno> ObterPorIdAsync(int id);

        Task<int> CriarAsync(string nome, string email, DateTime dataNascimento);

        Task AtualizarAsync(int id, string nome, string email, DateTime dataNascimento);

        Task DesativarAsync(int id);
    }
}
