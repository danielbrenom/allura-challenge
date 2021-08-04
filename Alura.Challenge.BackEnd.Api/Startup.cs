using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alura.Challenge.BackEnd.Api.Configurations;
using Alura.Challenge.BackEnd.Api.OpenAPIFilters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Alura.Challenge.BackEnd.Api
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
            services.ConfigureContainer(ApplicationConfigurator.Instance.Get());
            services.AddSwaggerGen(c =>
                     {
                         c.SwaggerDoc(
                             "v1",
                             new OpenApiInfo
                             {
                                 Title = "Alura Challenge BackEnd Api",
                                 Version = "v1",
                                 Description = "API for AluraFlix",
                                 License = new OpenApiLicense { Name = "MIT", Url = new Uri("http://opensource.org/licenses/MIT") }
                             });
                         c.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, "Alura.Challenge.BackEnd.Api.xml"));
                         c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                         {
                             Type = SecuritySchemeType.Http,
                             Scheme = JwtBearerDefaults.AuthenticationScheme,
                             BearerFormat = "jwt",
                             Description = "JWT Bearer Token Authorization"
                         });
                         c.OperationFilter<AuthorizationFilter>();
                     })
                    .AddSwaggerGenNewtonsoftSupport();
            
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var key = System.Text.Encoding.ASCII.GetBytes(ConfigurationsWrapper.GetConfiguration().GetSection("Jwt:Secret").Value);
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddHttpContextAccessor();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                   .UseSwagger()
                   .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alura.Challenge.BackEnd.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(x => x
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}