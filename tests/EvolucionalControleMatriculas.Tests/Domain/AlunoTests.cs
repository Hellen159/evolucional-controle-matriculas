using EvolucionalControleMatriculas.Entities;
using System;
using Xunit;

namespace EvolucionalControleMatriculas.Tests.Domain
{
    public class AlunoTests
    {
        [Fact]
        public void Construtor_ComDadosValidos_DeveCriarAluno()
        {
            var nome = "Ana Souza";
            var email = "ana@email.com";
            var dataNascimento = new DateTime(2005, 10, 15);

            var aluno = new Aluno(nome, email, dataNascimento);

            Assert.Equal(nome, aluno.Nome);
            Assert.Equal(email, aluno.Email);
            Assert.Equal(dataNascimento, aluno.DataNascimento);
        }

        [Fact]
        public void Construtor_DeveCriarAlunoComoAtivo()
        {
            var aluno = new Aluno(
                "Ana Souza",
                "ana@email.com",
                new DateTime(2005, 10, 15));

            Assert.True(aluno.Ativo);
        }

        [Fact]
        public void Construtor_DeveDefinirDataCadastro()
        {
            var inicio = DateTime.Now;

            var aluno = new Aluno(
                "Ana Souza",
                "ana@email.com",
                new DateTime(2005, 10, 15));

            var fim = DateTime.Now;

            Assert.InRange(aluno.DataCadastro, inicio, fim);
        }
    }
}
