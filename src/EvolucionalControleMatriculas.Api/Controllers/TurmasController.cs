using EvolucionalControleMatriculas.Application.Interfaces.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace EvolucionalControleMatriculas.Api.Controllers
{
    [RoutePrefix("api/turmas")]
    public class TurmasController : ApiController
    {
        private readonly ITurmaApplication _turmaApplication;

        public TurmasController(ITurmaApplication turmaApplication)
        {
            _turmaApplication = turmaApplication;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Listar()
        {
            var turmas = await _turmaApplication.ListarAsync();

            return Ok(turmas);
        }
    }
}