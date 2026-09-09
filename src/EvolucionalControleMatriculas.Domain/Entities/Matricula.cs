using EvolucionalControleMatriculas.Exceptions;
using System;

namespace EvolucionalControleMatriculas.Entities
{
    public class Matricula
    {
        public int Id { get; private set; }
        public int AlunoId { get; private set; }
        public int TurmaId { get; private set; }
        public DateTime DataMatricula { get; private set; }

        public Matricula(Aluno aluno, Turma turma)
        {
            if (!aluno.Ativo)
                throw new DomainException($"O aluno {aluno.Nome} não está ativo, não foi possível matricula-lo!");

            turma.InscreverAluno();

            AlunoId = aluno.Id;
            TurmaId = turma.Id;
            DataMatricula = DateTime.Now;
        }

    }
}
