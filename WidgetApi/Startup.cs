using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WidgetApi.EFCore;

namespace WidgetApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services, IServiceProvider serviceProvider)
        {
            services.AddControllers();
               

            var cn = Configuration.GetConnectionString("DefaultConnection");

            if (Environment.IsDevelopment())
            {
                services
                    .AddDbContext<WidgetContext>(o => o
                    .UseSqlite(cn));

                var context = serviceProvider.GetService<WidgetContext>();

                //var context = services
                //    .BuildServiceProvider()
                //    .GetService<WidgetContext>();

                Data.Seed(context);
            }
            else
            {
                services
                    .AddDbContext<WidgetContext>(o => o
                    .UseSqlServer(cn));
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
