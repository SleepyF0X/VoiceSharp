using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using VoiceSharp.API.Helpers;
using VoiceSharp.API.Middlewares;
using VoiceSharp.ApplicationServices;
using VoiceSharp.ApplicationServices.Auth.Commands;
using VoiceSharp.Persistence;

namespace VoiceSharp.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "VoiceSharp.API", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = @"Bearer",
                            },
                        },
                        new string[] { }
                    },
                });
            });
            services
                .AddPersistence(Configuration)
                .AddApplicationServices(Configuration)
                .AddEmailService(Configuration, "EmailConfig")
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidatorPipelineValidationBehavior<,>));
            services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(ValidateModelStateAttribute));
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginCommand>());
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VoiceSharp.API v1"));
            }

            app.UseHttpsRedirection();

            app
                .UseMiddleware<GlobalErrorHandler>()
                .UseRouting()
                .UseCors("AllowAll")
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                })
                .EnsureDbMigrated<VoiceSharpContext>();
        }
    }
}