using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Conventions;
using Nancy.TinyIoc;
using PatientAdministrationBoundedContext.Core;
using PatientAdministrationBoundedContext.Data;

namespace PatientAdministration.Api
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<IPatientRepository, InMemoryPatientRepository>().AsSingleton();
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                if (ctx.Request.Headers.Keys.Contains("Origin"))
                {
                    var origins = "" + string.Join(" ", ctx.Request.Headers["Origin"]);
                    ctx.Response.Headers["Access-Control-Allow-Origin"] = origins;

                    if (ctx.Request.Method == "OPTIONS")
                    {
                        // handle CORS preflight request

                        ctx.Response.Headers["Access-Control-Allow-Methods"] =
                            "GET, POST, PUT, DELETE, OPTIONS";

                        if (ctx.Request.Headers.Keys.Contains("Access-Control-Request-Headers"))
                        {
                            var allowedHeaders = "" + string.Join(
                                ", ", ctx.Request.Headers["Access-Control-Request-Headers"]);
                            ctx.Response.Headers["Access-Control-Allow-Headers"] = allowedHeaders;
                        }
                    }
                }
            });
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
        }
    }
}