using AdamSmith.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace AdamSmith.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/rate/{currencyCode:alpha}", async context =>
                {
                    var currencyCode = context.Request.RouteValues["currencyCode"].ToString();
                    await context.Response.WriteAsync(
                        (await new AdamSmithBot().GetDataAsync(currencyCode)).ToString(), 
                        Encoding.GetEncoding("windows-1251"));
                });
            });
        }
    }
}
