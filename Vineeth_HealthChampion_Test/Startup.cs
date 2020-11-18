using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EmployeeManagement.Data.EF;
using EmployeeManagement.Service.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Vineeth_HealthChampion_Test
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://example.com",
                                                          "http://localhost:3000",
                                                          "https://localhost:3000");
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyMethod();
                                  });
            });
            ConfigureDatabaseContextServices(services);
        }
        private void ConfigureDatabaseContextServices(IServiceCollection services)
        {
            var connectionString = @"Server=(localdb)\mssqllocaldb;Database=EmployeeDB;Integrated Security=True";
            services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        /// <summary>
        /// Configure Container.
        /// </summary>
        /// <param name="builder">Container Builder.</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule(Configuration));
        }
    }
}
