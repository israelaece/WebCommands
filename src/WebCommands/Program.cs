using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Net.Http;
using WebCommands.Dominio.Comandos;
using WebCommands.Dominio.Repositorios;
using WebCommands.Infrastructure.Bus;
using WebCommands.Infrastructure.Dependencies;
using WebCommands.Infrastructure.Extensions;
using WebCommands.Repositorios;

namespace WebCommands
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .ConfigureServices(s => s.AddRouting())
                .Configure(app =>
                {
                    app.UseRouter(r =>
                    {
                        var bus = CreateBus();

                        r.MapPost("fiscal/{action}", new RequestDelegate(async context =>
                        {
                            var action = context.GetRouteData().Values["action"].ToString();

                            if (!bus.Handlers.ContainsKey(action))
                            {
                                context.Response.StatusCode = 404;
                                return;
                            }

                            var command = context.Request.ReadAsCommand(bus.Handlers[action]);
                            var validateResult = command.Validate();

                            if (!validateResult.IsValid)
                            {
                                context.Response.StatusCode = 400;
                                await context.Response.WriteAsync(validateResult.ToJson());
                                return;
                            }

                            await bus.Send(command);

                            context.Response.StatusCode = 202;
                        }));
                    });
                })
                .Build()
                .Run();
        }

        private static InMemoryBus CreateBus()
        {
            var dependencies = new DefaultDependencyResolver();
            dependencies.Add<IRepositorioDeProdutos>(new RepositorioDeProdutos());
            dependencies.Add<IRepositorioDeClientes>(new RepositorioDeClientes());
            dependencies.Add<IRepositorioDeNotasFiscais>(new RepositorioDeNotasFiscais());

            var bus = new InMemoryBus(dependencies);

            bus.RegisterHandler<EmitirNotaFiscal>();
            bus.RegisterHandler<CancelarNotaFiscal>();

            return bus;
        }
    }
}