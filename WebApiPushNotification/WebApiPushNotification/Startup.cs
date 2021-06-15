using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebApiPushNotification
{
    ///<Summary>
    /// Inicio del Web Api
    ///</Summary>
    ///
    public class Startup
    {
        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = AppContext.BaseDirectory;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        ///<Summary>
        /// Inicio del Web Api
        ///</Summary>
        ///
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        ///<Summary>
        /// Parametro ConfigurACION
        ///</Summary>
        ///
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        ///<Summary>
        /// Configuracion de los Servicios del contenedor
        ///</Summary>
        ///
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Cors
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "1.0",
                    Title = "Web Api Push Notification",
                    Description = "Manejo de Token, Subscribe Notification y Send Notification",
                    Contact = new OpenApiContact
                    {
                        Name = "Sebastian Sandoval",
                        Email = "SebastianSandoval.Softbuilder@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/SebastianSandoval/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/SebastianSandoval/WeatherForecastWebAPI/blob/master/LICENSE")
                    },
                    TermsOfService = new Uri("https://github.com/SebastianSandoval/WeatherForecastWebAPI/blob/master/TermsOfService")

                });

                // options.DocInclusionPredicate((docName, description) => true);

                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var actionApiVersionModel = (ApiVersionModel)apiDesc.ActionDescriptor?.Properties
                        .FirstOrDefault(w => ((Type)w.Key).Equals(typeof(ApiVersionModel))).Value;
                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }
                    return actionApiVersionModel.DeclaredApiVersions.Any()
                        ? actionApiVersionModel.DeclaredApiVersions.Any(version => $"v{version}".Equals(docName))
                        : actionApiVersionModel.ImplementedApiVersions.Any(version => $"v{version}".Equals(docName));
                });

                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http, // en lugar de ApiKey para la definición de: var jwtSecurityScheme = new OpenApiSecurityScheme
                    Description = "¡Coloque **_SOLO_** su token 'JWT-Bearer' en el cuadro de texto a continuación!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

                // XML Comments Swagger .Net Core
                // Link: https://medium.com/c-sharp-progarmming/xml-comments-swagger-net-core-a390942d3329
                options.IncludeXmlComments(XmlCommentsFilePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        ///<Summary>
        /// Configurador del Controlador
        ///</Summary>
        ///
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "WebApiPushNotification v1.0"));


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
