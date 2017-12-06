using ComicsManager.API.Filters;
using ComicsManager.Common;
using ComicsManager.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace ComicsManager
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            // Lecture et construction de la configuration depuis le fichier appsettings.json
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddDbContext<ComicsManagerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ComicsManagerDatabase")));

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(GlobalExceptionsFilter));
            });

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info { Title = "ComicsManager API Documentation", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
        {
            if (env.IsDevelopment())
            {
                log.AddConsole();

                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ComicsManager API Documentation");
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ComicsManagerContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
