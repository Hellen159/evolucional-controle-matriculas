using EvolucionalControleMatriculas.Application.DTOs;
using EvolucionalControleMatriculas.Application.Interfaces.Services;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace EvolucionalControleMatriculas.Api.Controllers
{
    [RoutePrefix("api/alunos")]
    public class AlunosController : ApiController
    {
        private readonly IAlunoApplication _application;

        public AlunosController(IAlunoApplication application)
        {
            _application = application;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Listar(
            [FromUri] string nome = null,
            [FromUri] int pagina = 1,
            [FromUri] int tamanhoPagina = 10)
        {
            var resultado = await _application.ListarAsync(
                nome,
                pagina,
                tamanhoPagina);

            return Ok(new
            {
                resultado.Itens,
                resultado.Total
            });
        }

        [HttpGet]
        [Route("{id:int}", Name = "ObterAluno")]
        public async Task<IHttpActionResult> ObterPorId(int id)
        {
            var aluno = await _application.ObterPorIdAsync(id);

            if (aluno == null)
                return NotFound();

            return Ok(aluno);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Criar([FromBody] AlunoGravacaoDto dto)
        {
            if (dto == null)
                return BadRequest("Os dados do aluno são obrigatórios.");

            var id = await _application.CriarAsync(dto);

            return CreatedAtRoute(
                 "ObterAluno",
                 new { id },
                 new { id });
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Atualizar(
            int id,
            [FromBody] AlunoAtualizacaoDto dto)
        {
            if (dto == null)
                return BadRequest("Os dados do aluno são obrigatórios.");

            dto.Id = id;

            var atualizado = await _application.AtualizarAsync(dto);

            if (!atualizado)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Desativar(int id)
        {
            var desativado = await _application.DesativarAsync(id);

            if (!desativado)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}