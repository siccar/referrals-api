using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using OpenReferrals.RegisterManagementConnector.Extensions;
using OpenReferrals.RegisterManagementConnector.Configuration;
using System.Text.Json.Serialization;
using OpenReferrals.Repositories.Configuration;
using OpenReferrals.Repositories.Common;
using OpenReferrals.Repositories.OpenReferral;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace OpenReferrals
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
            services.AddControllers();
            InjectAuth(services);


            var mongoOptions = new MongoDbSettings();
            Configuration.Bind("MongoDbSettings", mongoOptions);
            services.AddSingleton(mongoOptions);

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddTransient<IOrganisationRepository, OrganisationRepository>();


            var registerOptions = new RegisterManagmentOptions();
            Configuration.Bind("RegisterManagementAPI", registerOptions);
            services.InjectRegisterManagementServiceClient(registerOptions);


            ApplySwaggerGen(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenReferrals v1"));
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InjectAuth(IServiceCollection services)
        {
            //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddMicrosoftIdentityWebApi(options =>
            //{
            //    Configuration.Bind("AzureAdB2C", options);

            //    options.TokenValidationParameters.NameClaimType = "name";
            //},
            //    options => { Configuration.Bind("AzureAdB2C", options); })
            //        .EnableTokenAcquisitionToCallDownstreamApi()
            //         .AddDownstreamWebApi();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration, "AzureAdB2C")
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddInMemoryTokenCaches();
        }

        private void ApplySwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Open Referrals API(Cast Project)",
                    Description = "Allows a user to manage a register",
                    Contact = new OpenApiContact
                    {
                        Name = "Siccar",
                        Email = "info@siccar.net",
                        Url = new Uri("https://www.siccar.net/"),
                    },
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}