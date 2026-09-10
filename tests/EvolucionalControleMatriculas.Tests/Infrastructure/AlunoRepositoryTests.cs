using EvolucionalControleMatriculas.Entities;
using EvolucionalControleMatriculas.Infrastructure.Data;
using EvolucionalControleMatriculas.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EvolucionalControleMatriculas.Tests.Infrastructure
{
    public class AlunoRepositoryTests
    {
        private readonly AlunoRepository _repository;

        public AlunoRepositoryTests()
        {
            var connectionFactory = new DbConnectionFactory(
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TesteEscola;Integrated Security=True;");

            _repository = new AlunoRepository(connectionFactory);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarAluno()
        {
            var id = 1;

            var aluno = await _repository.ObterPorIdAsync(id);

            Assert.NotNull(aluno);
            Assert.Equal(id, aluno.Id);
            Assert.Equal("Ana Souza", aluno.Nome);
        }

        [Fact]
        public async Task ObterPorIdAsync_QuandoNaoEncontrar_DeveRetornarNulo()
        {
            var id = 9999;

            var aluno = await _repository.ObterPorIdAsync(id);

            Assert.Null(aluno);
        }

        [Fact]
        public async Task InserirAsync_DeveInserirAlunoERetornarId()
        {
            var aluno = new Aluno(
                "Aluno Teste",
                "teste@teste.com",
                new DateTime(2005, 10, 15));

            var id = await _repository.InserirAsync(aluno);

            Assert.True(id > 0);

            var alunoInserido = await _repository.ObterPorIdAsync(id);

            Assert.NotNull(alunoInserido);
            Assert.Equal("Aluno Teste", alunoInserido.Nome);
            Assert.Equal("teste@teste.com", alunoInserido.Email);
        }

        [Fact]
        public async Task AtualizarAsync_DeveAtualizarAluno()
        {
            var aluno = await _repository.ObterPorIdAsync(1);

            Assert.NotNull(aluno);

            // Act
            // Aqui precisaremos de uma forma de alterar o estado do Aluno.

            // Assert
        }

        [Fact]
        public async Task DesativarAsync_DeveDesativarAluno()
        {
            var aluno = await _repository.ObterPorIdAsync(5);

            Assert.NotNull(aluno);
            Assert.True(aluno.Ativo);

            await _repository.DesativarAsync(aluno.Id);

            var alunoDesativado = await _repository.ObterPorIdAsync(aluno.Id);

            Assert.NotNull(alunoDesativado);
            Assert.False(alunoDesativado.Ativo);
        }

        [Fact]
        public async Task ListarAsync_DeveRetornarAlunosEPaginacao()
        {
            var pagina = 1;
            var tamanhoPagina = 3;

            var resultado = await _repository.ListarAsync(
                null,
                pagina,
                tamanhoPagina);

            Assert.NotNull(resultado.Itens);
            Assert.Equal(tamanhoPagina, resultado.Itens.Count);
            Assert.True(resultado.Total >= resultado.Itens.Count);
        }

        [Fact]
        public async Task ListarAsync_ComFiltroNome_DeveRetornarAlunosCorrespondentes()
        {
            var nome = "Ana";

            var resultado = await _repository.ListarAsync(
                nome,
                1,
                10);

            Assert.NotNull(resultado.Itens);
            Assert.NotEmpty(resultado.Itens);
            Assert.All(resultado.Itens, aluno =>
            Assert.Contains(nome, aluno.Nome));
        }
    }
}
