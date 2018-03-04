using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;

namespace HairSalon
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                app.UseDeveloperExceptionPage();
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("There is no such page or process!");
            });
        }
    }

    public static class DBConfiguration
    {
        // public static string ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=andy_grossberg;";
        // NOTE: Users will want to change the settings below to match the settings for their machines. Use the above line as a DEFAULT
        public static string ConnectionString = "server=localhost;user id=root;password=Qsw7FaaOzOyVqz2m;port=8889;database=andy_grossberg;Allow User Variables=True";
    }
}
