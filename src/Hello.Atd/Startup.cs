using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Dnx.Runtime;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Logging;
using Hello.Atd.Middleware;
using Utilites;

namespace Hello.Atd
{
    public class Startup
    {
        private IConfigurationRoot _configuration;

        public Startup(IApplicationEnvironment appEnv, IHostingEnvironment env)
        {
            var confBuilder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            _configuration = confBuilder.Build();
        }   
          
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // ioc
            services.AddSingleton(typeof(IMyCulture), typeof(MyCulture));
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory logger)
        {
            logger.AddConsole(LogLevel.Information);

            app.UseHttpLog();

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvc();

            app.MapWhen(ctx => ctx.Request.Path.Value.Equals("/super-secret"), HandleSecret);

            app.Use(async (context, next) =>
            {
                Console.WriteLine("1");
                await next.Invoke();
                Console.WriteLine("2");
            });            

            app.Use(async (context, next) =>
            {
                Console.WriteLine("3");
                await next.Invoke();
                Console.WriteLine("4");
            });            

            app.Run(async (context) =>
            {
                //throw new Exception("Something bad happened...");
                await context.Response.WriteAsync(_configuration.Get<string>("HelloMessage"));
            });
        }

        private void HandleSecret(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(_configuration.Get<string>("Foo"));
            });
        }
    }
}
