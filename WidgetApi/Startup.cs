using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WidgetApi.EFCore;

namespace WidgetApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var cn = Configuration.GetConnectionString("DefaultConnection");

            if (Environment.IsDevelopment())
            {
                services
                    .AddDbContext<WidgetContext>(o => o
                    .UseSqlite(cn));

                var context = services
                    .BuildServiceProvider()
                    .GetService<WidgetContext>();

                Data.Seed(context);
            }
            else
            {
                services
                    .AddDbContext<WidgetContext>(o => o
                    .UseSqlServer(cn));
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();
        }
    }
}
