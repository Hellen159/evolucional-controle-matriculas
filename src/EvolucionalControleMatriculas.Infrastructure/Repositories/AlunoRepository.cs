using Dapper;
using EvolucionalControleMatriculas.Application.Interfaces;
using EvolucionalControleMatriculas.Entities;
using EvolucionalControleMatriculas.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public AlunoRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> AtualizarAsync(Aluno aluno)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();

                var sql = @"
                    UPDATE Aluno
                    SET Nome = @Nome,
                        Email = @Email,
                        DataNascimento = @DataNascimento
                    WHERE Id = @Id";

                var linhasAfetadas = await connection.ExecuteAsync(sql, new
                {
                    aluno.Id,
                    aluno.Nome,
                    aluno.Email,
                    aluno.DataNascimento
                });

                return linhasAfetadas > 0;
            }
        }

        public async Task<bool> DesativarAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();

                var sql = @"
                    UPDATE Aluno
                    SET Ativo = 0
                    WHERE Id = @Id";

                var linhasAfetadas = await connection.ExecuteAsync(sql, new { Id = id });

                return linhasAfetadas > 0;
            }
        }

        public async Task<int> InserirAsync(Aluno aluno)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();

                var sql = @"
                    INSERT INTO Aluno
                    (
                        Nome,
                        Email,
                        DataNascimento,
                        Ativo,
                        DataCadastro
                    )
                    OUTPUT INSERTED.Id
                    VALUES
                    (
                        @Nome,
                        @Email,
                        @DataNascimento,
                        @Ativo,
                        @DataCadastro
                    )";

                return await connection.ExecuteScalarAsync<int>(sql, new
                {
                    aluno.Nome,
                    aluno.Email,
                    aluno.DataNascimento,
                    aluno.Ativo,
                    aluno.DataCadastro
                });
            }
        }

        public async Task<(IReadOnlyCollection<Aluno> Itens, int Total)> ListarAsync(
            string nome,
            int pagina,
            int tamanhoPagina)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT Id, Nome, Email, DataNascimento, Ativo, DataCadastro
                    FROM Aluno
                    WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%')
                    ORDER BY Id
                    OFFSET @Offset ROWS
                    FETCH NEXT @TamanhoPagina ROWS ONLY;

                    SELECT COUNT(1)
                    FROM Aluno
                    WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%');";

                var offset = (pagina - 1) * tamanhoPagina;

                using (var resultado = await connection.QueryMultipleAsync(sql, new
                {
                    Nome = string.IsNullOrWhiteSpace(nome) ? null : nome,
                    Offset = offset,
                    TamanhoPagina = tamanhoPagina
                }))
                {
                    var itens = await resultado.ReadAsync<Aluno>();
                    var total = await resultado.ReadSingleAsync<int>();

                    return (itens.AsList(), total);
                }
            }
        }

        public async Task<Aluno> ObterPorIdAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT Id, Nome, Email, DataNascimento, Ativo, DataCadastro
                    FROM Aluno
                    WHERE Id = @Id";

                return await connection.QuerySingleOrDefaultAsync<Aluno>(
                    sql,
                    new { Id = id });
            }
        }
    }
}
