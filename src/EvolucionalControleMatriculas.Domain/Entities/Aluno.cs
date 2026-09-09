using System;

namespace EvolucionalControleMatriculas.Entities
{
    public class Aluno
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public bool Ativo { get; private set; }

        public DateTime DataCadastro { get;private set; }

        public Aluno(string nome, string email, DateTime dataNascimento)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Ativo = true;
            DataCadastro = DateTime.Now;
        }
    }
}
