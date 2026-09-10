using Dapper;
using EvolucionalControleMatriculas.Application.Interfaces;
using EvolucionalControleMatriculas.Entities;
using EvolucionalControleMatriculas.Infrastructure.Data;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Infrastructure.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public MatriculaRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> ExisteAsync(int alunoId, int turmaId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT COUNT(1)
                    FROM Matricula
                    WHERE AlunoId = @AlunoId
                    AND TurmaId = @TurmaId";

                var quantidade = await connection.ExecuteScalarAsync<int>(sql, new
                {
                    AlunoId = alunoId,
                    TurmaId = turmaId
                });

                return quantidade > 0;
            }
        }

        public async Task<bool> MatricularAsync(Matricula matricula)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sqlMatricula = @"
                            INSERT INTO Matricula
                            (
                                AlunoId,
                                TurmaId,
                                DataMatricula
                            )
                            VALUES
                            (
                                @AlunoId,
                                @TurmaId,
                                @DataMatricula
                            )";

                        await connection.ExecuteAsync(
                            sqlMatricula,
                            new
                            {
                                matricula.AlunoId,
                                matricula.TurmaId,
                                matricula.DataMatricula
                            },
                            transaction);

                        var sqlTurma = @"
                            UPDATE Turma
                            SET VagasDisponiveis = VagasDisponiveis - 1
                            WHERE Id = @TurmaId
                            AND VagasDisponiveis > 0";

                        var linhasAfetadas = await connection.ExecuteAsync(
                            sqlTurma,
                            new
                            {
                                matricula.TurmaId
                            },
                            transaction);

                        if (linhasAfetadas == 0)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
