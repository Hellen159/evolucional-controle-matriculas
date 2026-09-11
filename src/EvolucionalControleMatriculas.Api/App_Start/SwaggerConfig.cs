using System.Web.Http;
using WebActivatorEx;
using EvolucionalControleMatriculas.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace EvolucionalControleMatriculas.Api
{
    public static class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion(
                        "v1",
                        "Evolucional Controle de Matrículas API");
                })
                .EnableSwaggerUi();
        }
    }
}