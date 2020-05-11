using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HttpPatchSample.Swagger;
using HttpPatchSample.Database;
using HttpPatchSample.Models;
using Microsoft.EntityFrameworkCore;

namespace HttpPatchSample
{
    public class Startup
    {
        private readonly NSwagConfigurator _nswagConfigurator;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _nswagConfigurator = new NSwagConfigurator(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new PatchRequestContractResolver();
                });


            services.AddDbContext<HttpPatchSampleDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            _nswagConfigurator.AddSwagger(services);
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

            app.UseAuthorization();

            _nswagConfigurator.UseSwagger(app);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}