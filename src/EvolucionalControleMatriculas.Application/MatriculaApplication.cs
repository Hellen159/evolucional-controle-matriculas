using EvolucionalControleMatriculas.Application.DTOs;
using EvolucionalControleMatriculas.Application.Enums;
using EvolucionalControleMatriculas.Application.Interfaces;
using EvolucionalControleMatriculas.Application.Interfaces.Services;
using EvolucionalControleMatriculas.Entities;
using System.Threading.Tasks;

namespace EvolucionalControleMatriculas.Application
{
    public class MatriculaApplication : IMatriculaApplication
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaRepository _matriculaRepository;

        public MatriculaApplication(
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaRepository matriculaRepository)
        {
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _matriculaRepository = matriculaRepository;
        }

        public async Task<ResultadoMatricula> MatricularAsync(MatriculaDto dto)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(dto.AlunoId);

            if (aluno == null)
                return ResultadoMatricula.AlunoNaoEncontrado;

            var turma = await _turmaRepository.ObterPorIdAsync(dto.TurmaId);

            if (turma == null)
                return ResultadoMatricula.TurmaNaoEncontrada;

            var matriculaExiste = await _matriculaRepository.ExisteAsync(
                dto.AlunoId,
                dto.TurmaId);

            if (matriculaExiste)
                return ResultadoMatricula.MatriculaJaExiste;

            var matricula = new Matricula(aluno, turma);

            var sucesso = await _matriculaRepository.MatricularAsync(matricula);

            return sucesso
                ? ResultadoMatricula.Sucesso
                : ResultadoMatricula.Falha;
        }

    }
}
