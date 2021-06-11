using AutoMapper;
using ChatRoomServer.Domain.Repositories;
using ChatRoomService.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Domain;
using Infrastructure;

namespace ChatRoomServer.WebApi
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
            var mapperConfig =
                new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new MappingProfile());
                    });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.Scan(selector => selector
                .FromAssemblies(DomainAssembly.Value, InfrastructureAssembly.Value)
                .AddClasses(publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddControllers();
            services
                .AddSwaggerGen(options =>
                {
                    options
                        .SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = "ChatRoomServer.WebApi",
                            Version = "v1"
                        });
                });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app
                    .UseSwaggerUI(uiOptions =>
                        uiOptions
                            .SwaggerEndpoint("/swagger/v1/swagger.json",
                            "ChatRoomServer.WebApi v1"));
            }
            else
            {
                app.UseHttpsRedirection();
            }
            
            app.UseCors();

            app.UseRouting();
            app.UseAuthorization();
            app
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            new ConfigurationBuilder().AddEnvironmentVariables();
        }
    }
}
