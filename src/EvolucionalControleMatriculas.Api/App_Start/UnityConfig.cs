using EvolucionalControleMatriculas.Application;
using EvolucionalControleMatriculas.Application.Interfaces;
using EvolucionalControleMatriculas.Application.Interfaces.Repositories;
using EvolucionalControleMatriculas.Application.Interfaces.Services;
using EvolucionalControleMatriculas.Infrastructure.Data;
using EvolucionalControleMatriculas.Infrastructure.Repositories;
using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace EvolucionalControleMatriculas.Api.App_Start
{
    public class UnityConfig
    {

        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Application
            container.RegisterType<IAlunoApplication, AlunoApplication>();
            container.RegisterType<ITurmaApplication, TurmaApplication>();
            container.RegisterType<IMatriculaApplication, MatriculaApplication>();
            container.RegisterType<IRelatorioApplication, RelatorioApplication>();

            // Repositories
            container.RegisterType<IAlunoRepository, AlunoRepository>();
            container.RegisterType<ITurmaRepository, TurmaRepository>();
            container.RegisterType<IMatriculaRepository, MatriculaRepository>();
            container.RegisterType<IRelatorioRepository, RelatorioRepository>();

            // Connection factory
            var connectionString = ConfigurationManager
                .ConnectionStrings["TesteEscola"]
                .ConnectionString;

            container.RegisterInstance(
                new DbConnectionFactory(connectionString));

            GlobalConfiguration.Configuration.DependencyResolver =
                new UnityDependencyResolver(container);
        }

    }
}