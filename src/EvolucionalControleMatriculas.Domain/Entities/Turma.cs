using EvolucionalControleMatriculas.Enums;
using EvolucionalControleMatriculas.Exceptions;

namespace EvolucionalControleMatriculas.Entities
{
    public class Turma
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public PeriodoTurma Periodo { get; private set; }
        public int VagasTotais { get; private set; }
        public int VagasDisponiveis { get; private set; }

        public Turma(string nome, PeriodoTurma periodoTurma, int vagasTotais, int vagasDisponiveis)
        {
            if (vagasTotais < 0)
                throw new DomainException($"O valor para vagas totais tem que ser maior que zero!");
            if (vagasDisponiveis < 0)
                throw new DomainException($"O valor para vagas disponiveis tem que ser maior que zero!");
            if (vagasDisponiveis > vagasTotais)
                throw new DomainException($"O valor para vagas disponiveis tem que ser menor que o valor de vagas totais!");

            Nome = nome;
            Periodo = periodoTurma;
            VagasTotais = vagasTotais;
            VagasDisponiveis = vagasDisponiveis;
        }

        public void InscreverAluno()
        {
            if (VagasDisponiveis < 1)
                throw new DomainException($"A turma {Nome} não tem vagas disponiveis!");

            VagasDisponiveis--;
        }
    }
}
