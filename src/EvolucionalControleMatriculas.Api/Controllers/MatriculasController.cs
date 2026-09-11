using EvolucionalControleMatriculas.Application.DTOs;
using EvolucionalControleMatriculas.Application.Enums;
using EvolucionalControleMatriculas.Application.Interfaces.Services;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace EvolucionalControleMatriculas.Api.Controllers
{
    [RoutePrefix("api/matriculas")]
    public class MatriculasController : ApiController
    {
        private readonly IMatriculaApplication _application;

        public MatriculasController(IMatriculaApplication application)
        {
            _application = application;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Matricular([FromBody] MatriculaDto dto)
        {
            if (dto == null)
                return BadRequest("Os dados da matrícula são obrigatórios.");

            var resultado = await _application.MatricularAsync(dto);

            switch (resultado)
            {
                case ResultadoMatricula.Sucesso:
                    return StatusCode(HttpStatusCode.Created);

                case ResultadoMatricula.AlunoNaoEncontrado:
                    return NotFound();

                case ResultadoMatricula.TurmaNaoEncontrada:
                    return NotFound();

                case ResultadoMatricula.MatriculaJaExiste:
                    return Content(
                        HttpStatusCode.Conflict,
                        "O aluno já está matriculado nesta turma.");

                case ResultadoMatricula.Falha:
                    return Content(
                        HttpStatusCode.Conflict,
                        "A turma não possui vagas disponíveis.");

                default:
                    return InternalServerError();
            }
        }
    }
}