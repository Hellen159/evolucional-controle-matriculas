using EvolucionalControleMatriculas.Entities;
using EvolucionalControleMatriculas.Enums;
using EvolucionalControleMatriculas.Exceptions;
using Xunit;

namespace EvolucionalControleMatriculas.Tests.Domain
{
    public class TurmaTests
    {
        [Fact]
        public void Construtor_ComDadosValidos_DeveCriarTurma()
        {
            var turma = new Turma("3A - Ensino Medio",PeriodoTurma.Manha,30,10);

            Assert.Equal("3A - Ensino Medio", turma.Nome);
            Assert.Equal(PeriodoTurma.Manha, turma.Periodo);
            Assert.Equal(30, turma.VagasTotais);
            Assert.Equal(10, turma.VagasDisponiveis);
        }

        [Fact]
        public void Construtor_ComVagasTotaisNegativas_DeveLancarDomainException()
        {
            var exception = Assert.Throws<DomainException>(() =>
                new Turma("3A - Ensino Medio",PeriodoTurma.Manha,-1,10));

            Assert.Equal("O valor para vagas totais tem que ser maior que zero!",exception.Message);
        }

        [Fact]
        public void Construtor_ComVagasDisponiveisNegativas_DeveLancarDomainException()
        {
            var exception = Assert.Throws<DomainException>(() =>
                new Turma("3A - Ensino Medio",PeriodoTurma.Manha,30,-1));

            Assert.Equal("O valor para vagas disponiveis tem que ser maior que zero!",exception.Message);
        }

        [Fact]
        public void Construtor_ComVagasDisponiveisMaiorQueVagasTotais_DeveLancarDomainException()
        {
            var exception = Assert.Throws<DomainException>(() =>
                new Turma("3A - Ensino Medio",PeriodoTurma.Manha,10,11));

            Assert.Equal("O valor para vagas disponiveis tem que ser menor que o valor de vagas totais!",exception.Message);
        }

        [Fact]
        public void InscreverAluno_ComVagaDisponivel_DeveDecrementarVaga()
        {
            var turma = new Turma("3A - Ensino Medio",PeriodoTurma.Manha,30,10);

            turma.InscreverAluno();

            Assert.Equal(9, turma.VagasDisponiveis);
        }

        [Fact]
        public void InscreverAluno_SemVagasDisponiveis_DeveLancarDomainException()
        {
            var turma = new Turma("Turma Lotada",PeriodoTurma.Manha,2,0);

            var exception = Assert.Throws<DomainException>(() =>
                turma.InscreverAluno());

            Assert.Equal("A turma Turma Lotada não tem vagas disponiveis!",exception.Message);
        }
    }
}
