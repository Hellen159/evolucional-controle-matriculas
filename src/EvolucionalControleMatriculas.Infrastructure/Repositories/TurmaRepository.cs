using Dapper;
using EvolucionalControleMatriculas.Application.Interfaces;
using EvolucionalControleMatriculas.Entities;
using EvolucionalControleMatriculas.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Infrastructure.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public TurmaRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IReadOnlyCollection<Turma>> ListarAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT
                        Id,
                        Nome,
                        Periodo,
                        VagasTotal,
                        VagasDisponiveis
                    FROM Turma
                    ORDER BY Id";

                var turmas = await connection.QueryAsync<Turma>(sql);

                return turmas.AsList();
            }
        }

        public async Task<Turma> ObterPorIdAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT
                        Id,
                        Nome,
                        Periodo,
                        VagasTotal,
                        VagasDisponiveis
                    FROM Turma
                    WHERE Id = @Id";

                return await connection.QuerySingleOrDefaultAsync<Turma>(
                    sql,
                    new { Id = id });
            }
        }
    }
}
