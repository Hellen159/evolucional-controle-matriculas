using EvolucionalControleMatriculas.Api.App_Start;
using System.Web.Http;

namespace EvolucionalControleMatriculas.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();


        }

        protected void Application_BeginRequest()
        {
            if (Request.AppRelativeCurrentExecutionFilePath == "~/")
            {
                Response.Redirect("~/swagger");
            }
        }
    }
}
