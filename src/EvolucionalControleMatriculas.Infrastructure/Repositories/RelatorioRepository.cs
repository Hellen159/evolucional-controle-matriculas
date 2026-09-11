using Dapper;
using EvolucionalControleMatriculas.Application.DTOs;
using EvolucionalControleMatriculas.Application.Interfaces.Repositories;
using EvolucionalControleMatriculas.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Infrastructure.Repositories
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public RelatorioRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IReadOnlyCollection<AlunosPorTurmaDto>> ListarAlunosPorTurmaAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();

                var sql = @"
                        SELECT
                            t.Nome AS NomeTurma,
                            COUNT(m.Id) AS QuantidadeAlunos,
                            t.VagasDisponiveis AS VagasRestantes
                        FROM Turma t
                        LEFT JOIN Matricula m ON m.TurmaId = t.Id
                        GROUP BY
                            t.Id,
                            t.Nome,
                            t.VagasDisponiveis
                        ORDER BY
                            t.Id";

                var resultado = await connection.QueryAsync<AlunosPorTurmaDto>(sql);

                return resultado.AsList();
            }
        }
    }
}
