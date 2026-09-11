using EvolucionalControleMatriculas.Application.Interfaces.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace EvolucionalControleMatriculas.Api.Controllers
{
    [RoutePrefix("api/relatorios")]
    public class RelatoriosController : ApiController
    {
        private readonly IRelatorioApplication _application;

        public RelatoriosController(IRelatorioApplication application)
        {
            _application = application;
        }

        [HttpGet]
        [Route("alunos-por-turma")]
        public async Task<IHttpActionResult> ListarAlunosPorTurma()
        {
            var resultado = await _application.ListarAlunosPorTurmaAsync();

            return Ok(resultado);
        }
    }
}