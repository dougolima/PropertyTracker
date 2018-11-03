using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PropertyTracker.Application.Services.Interfaces;
using PropertyTracker.Application.Services.Services;
using PropertyTracker.Data.Repository;
using PropertyTracker.Data.Repository.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace PropertyTracker.Presentation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Property Crawler API", Version = "v1" });
            });

            services.AddTransient<ISiteService, SiteService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<ISiteRepository, SiteRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddSingleton<IMongoDatabase>(m =>
            {
                var client = new MongoClient();
                return client.GetDatabase("crawler");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Property Crawler API");
                //c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
