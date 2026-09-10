using EvolucionalControleMatriculas.Entities;
using EvolucionalControleMatriculas.Enums;
using EvolucionalControleMatriculas.Exceptions;
using System;
using Xunit;

namespace EvolucionalControleMatriculas.Tests.Domain
{
    public class MatriculaTests
    {
        [Fact]
        public void Construtor_ComAlunoAtivoETurmaComVaga_DeveCriarMatricula()
        {
            var aluno = new Aluno(
                "Ana Souza",
                "ana@email.com",
                new DateTime(2005, 10, 15));

            var turma = new Turma(
                "3A - Ensino Medio",
                PeriodoTurma.Manha,
                30,
                10);

            var matricula = new Matricula(aluno, turma);

            Assert.Equal(aluno.Id, matricula.AlunoId);
            Assert.Equal(turma.Id, matricula.TurmaId);
        }

        [Fact]
        public void Construtor_ComAlunoAtivoETurmaComVaga_DeveDecrementarVagaDaTurma()
        {
            var aluno = new Aluno(
                "Ana Souza",
                "ana@email.com",
                new DateTime(2005, 10, 15));

            var turma = new Turma(
                "3A - Ensino Medio",
                PeriodoTurma.Manha,
                30,
                10);

            new Matricula(aluno, turma);

            Assert.Equal(9, turma.VagasDisponiveis);
        }

        //[Fact]
        //public void Construtor_ComAlunoInativo_DeveLancarDomainException()
        //{
        //    var aluno = new Aluno(
        //        "Ana Souza",
        //        "ana@email.com",
        //        new DateTime(2005, 10, 15));

        //    var turma = new Turma(
        //        "3A - Ensino Medio",
        //        PeriodoTurma.Manha,
        //        30,
        //        10);

        //}

        [Fact]
        public void Construtor_ComTurmaSemVaga_DeveLancarDomainException()
        {
            var aluno = new Aluno(
                "Ana Souza",
                "ana@email.com",
                new DateTime(2005, 10, 15));

            var turma = new Turma(
                "Turma Lotada",
                PeriodoTurma.Manha,
                2,
                0);

            var exception = Assert.Throws<DomainException>(() =>
                new Matricula(aluno, turma));

            Assert.Equal(
                "A turma Turma Lotada não tem vagas disponiveis!",
                exception.Message);
        }

        [Fact]
        public void Construtor_ComDadosValidos_DeveDefinirDataMatricula()
        {
            var aluno = new Aluno(
                "Ana Souza",
                "ana@email.com",
                new DateTime(2005, 10, 15));

            var turma = new Turma(
                "3A - Ensino Medio",
                PeriodoTurma.Manha,
                30,
                10);

            var inicio = DateTime.Now;

            var matricula = new Matricula(aluno, turma);

            var fim = DateTime.Now;

            Assert.InRange(matricula.DataMatricula, inicio, fim);
        }
    }
}
