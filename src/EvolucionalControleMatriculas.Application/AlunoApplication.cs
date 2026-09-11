using EvolucionalControleMatriculas.Application.DTOs;
using EvolucionalControleMatriculas.Application.Interfaces;
using EvolucionalControleMatriculas.Application.Interfaces.Services;
using EvolucionalControleMatriculas.Application.Mappers;
using EvolucionalControleMatriculas.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application
{
    public class AlunoApplication : IAlunoApplication
    {
        private readonly IAlunoRepository _repository;

        public AlunoApplication(IAlunoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AtualizarAsync(AlunoAtualizacaoDto dto)
        {
            var aluno = await _repository.ObterPorIdAsync(dto.Id);

            if (aluno == null)
                return false;

            aluno.Atualizar(
                dto.Nome,
                dto.Email,
                dto.DataNascimento);

            return await _repository.AtualizarAsync(aluno);
        }

        public async Task<int> CriarAsync(AlunoGravacaoDto dto)
        {
            var aluno = new Aluno(
                dto.Nome,
                dto.Email,
                dto.DataNascimento);

            return await _repository.InserirAsync(aluno);
        }

        public async Task<bool> DesativarAsync(int id)
        {
            return await _repository.DesativarAsync(id);
        }

        public async Task<(IReadOnlyCollection<AlunoDto> Itens, int Total)> ListarAsync(
            string nome,
            int pagina,
            int tamanhoPagina)
        {
            var resultado = await _repository.ListarAsync(
                nome,
                pagina,
                tamanhoPagina);

            var itens = resultado.Itens
                .Select(AlunoMapper.Mapear)
                .ToList();

            return (itens, resultado.Total);
        }

        public async Task<AlunoDto> ObterPorIdAsync(int id)
        {
            var aluno = await _repository.ObterPorIdAsync(id);

            if(aluno == null)
                return null;

            return AlunoMapper.Mapear(aluno);
        }
    }
}