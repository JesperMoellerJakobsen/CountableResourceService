using System.Data;
using System.Data.SqlClient;
using Domain.Model;
using Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories.RabbitMQStub;
using Repositories.Repositories;

namespace API
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
            services.AddOptions();
            var connectionString = Configuration.GetConnectionString("DbConnectionString");
            services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));
            services.AddScoped<ICounterService, CounterService>();
            services.AddScoped<ICounterRepository, CounterRepository>();
            services.AddScoped<IRabbitMqStub, RabbitMqStub>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
