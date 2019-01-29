using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestWithAspNet.Model.Context;
using Microsoft.EntityFrameworkCore;
using RestWithAspNet.Business;
using RestWithAspNet.Business.Implementations;
using RestWithAspNet.Repository;
using RestWithAspNet.Repository.Implementations;
using x = MySql.Data.MySqlClient;
using System.Collections.Generic;
using RestWithAspNet.Repository.Generic;
using Microsoft.Net.Http.Headers;
using Tapioca.HATEOAS;
using RestWithAspNet.Hypermedia;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using RestWithAspNet.Security.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace RestWithAspNet
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        private readonly ILogger _logger;
        public IHostingEnvironment _environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILoggerFactory logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger.CreateLogger<Startup>();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connectionString));


            // Adding Migrations Support
            ExecuteMigrations(connectionString);

            //Adding JWT SUPPORT
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);
            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(_configuration.GetSection("TokenConfigurations"))
                .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                //validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;
                //Check if a received toklen is still valid
                paramsValidation.ValidateLifetime = true;

                //Tolerance time for the expiration of a token(used in case 
                //of time synchronization problems between different cmputes involved in the comunication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            //Enables the use of the token as a means of authorizing access to this projects resourses
            services.AddAuthorization(auth=>
            {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
            });

            //Content Negotiation -- Support to XML and JSON
            services.AddMvc(opt =>
                {
                    opt.RespectBrowserAcceptHeader = true;
                    opt.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                    opt.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/json"));
                })
                .AddXmlSerializerFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Define as opções do filtro HATEOAS
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ObjectContentResponseEnricherList.Add(new BookEnricher());
            //Injeta o serviço HATEOAS
            services.AddSingleton(filterOptions);


            //Versionamento
            services.AddApiVersioning();


            //Swagger
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Info
                {
                    Title = "RESTful API with ASP .NET Core 2.1",
                    Version = "V1"
                });
            });

            //Dependency Injection
            services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
            services.AddScoped<IBookBusiness, BookBusinessImpl>();
            services.AddScoped<ILoginBusiness, LoginBusinessImpl>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            //Não é necessário mais fazer desta forma as injeções de dep dos repositorios estão na proxima linha não comentada
            //services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }


        private void ExecuteMigrations(string connectionString)
        {
            if (_environment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new x.MySqlConnection(connectionString);
                    var evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true
                    };

                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Database migration failed", ex);
                    throw;
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Usando SWagger
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");

            app.UseRewriter(option);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}"
                    );
            });
        }
    }
}
