using Abstraction.IAplication.Administration;
using Abstraction.IRepository.Administration;
using Abstraction.IService.Administration;
using Aplication.Administration;
using Repository.Administration;
using Services.Administration;

using DataAccess;
using DataAccess.CustomConnection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Abstraction.IRepository.Masters;
using Repository.Masters;
using Aplication.Masters;
using Services.Masters;
using Abstraction.IAplication.Masters;
using Abstraction.IService.Masters;
using Services;
using Abstraction;
using Application.Auth;
using Abstraction.IApplication.Auth;
using BudgetForcast.Services.Auth;
using Abstraction.IService.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



namespace ApiProyectoPatata
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string _policyName = "CorsPolicy";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var fglfrom = Configuration["flagPeticionFrontLocal"];
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {

                    Title = "Sweet Dreams.API",
                    Version = "v1",
                    Description = "Api Para Sweet Dreams.",
                    TermsOfService = new Uri("https://sample.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Desarrollador Kevin Purizaca Pazo",
                        Email = "purizacapazokevin@gmail.com",
                        Url = new Uri("https://www.sistemasVentas.pe/"),
                    }
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

            });



            /**/
            if (fglfrom == "1")
            {
                services.AddCors(opt =>
                {
                    opt.AddPolicy(name: _policyName, builder =>
                    {
                        builder.WithOrigins(
                            "*")
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod();
                    });
                });
            }
            else
            {
                services.AddCors(opt =>
                {
                    opt.AddPolicy(name: _policyName, builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "https://sweet-comunity.netlify.app")// builder.WithOrigins("http://localhost:4200", "http://localhost:50544")
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod();
                    });
                });
            }



            services.AddDbContext<ApiDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CS_SistemaVentas"),
                b => b.MigrationsAssembly("SistemaVentas.API").CommandTimeout(0))

            );

            //services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));



            //  services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //  .AddEntityFrameworkStores<ApiDbContext>();

            services.Configure<FormOptions>(o =>  // currently all set to max, configure it to your needs!
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = long.MaxValue;
                o.MultipartBoundaryLengthLimit = int.MaxValue;
                o.MultipartHeadersCountLimit = int.MaxValue;
                o.MultipartHeadersLengthLimit = int.MaxValue;
            });

            #region Agregar Clases
            services.AddScoped<ITokenHandlerService, TokenHandlerService>();

            //services.AddScoped(typeof(ITokenHandlerService), typeof(TokenHandlerService));
            services.AddScoped<ICustomConnection, CustomConnection>(_ => new CustomConnection(Configuration["ConnectionStrings:CS_SistemaVentas"]));
            
            services.AddScoped<IAuthenticationApplication, AuthenticationApplication>();
            services.AddScoped<ICaptchaGoogleApplication, CaptchaGoogleApplication>();
            services.AddScoped<ICaptchaGoogleService, CaptchaGoogleService>();

            services.AddScoped<IUsersAplication,    UsersAplication>();
            services.AddScoped<IUsersService,       UsersService>();
            services.AddScoped<IUsersRepository,    UsersRepository>();

            services.AddScoped<IAuxiliaryTablesAplication,   AuxiliaryTablesAplication>();
            services.AddScoped<IAuxiliaryTablesService,      AuxiliaryTablesService>();
            services.AddScoped<IAuxiliaryTablesRepository,   AuxiliaryTablesRepository>();

            services.AddScoped<IModuleOptionAplication, ModuleOptionAplication>();
            services.AddScoped<IModuleOptionService,    ModuleOptionService>();
            services.AddScoped<IModuleOptionRepository, ModuleOptionRepository>();

            services.AddScoped<IProfileAplication,  ProfileAplication>();
            services.AddScoped<IProfileService,     ProfileService>();
            services.AddScoped<IProfileRepository,  ProfileRepository>();

            services.AddScoped<IComunityAplication, ComunityAplication>();
            services.AddScoped<IComunityService,    ComunityService>();
            services.AddScoped<IComunityRepository, ComunityRepository>();

            services.AddScoped<ISheduleAplication,  SheduleAplication>();
            services.AddScoped<ISheduleService,     SheduleService>();
            services.AddScoped<ISheduleRepository,  SheduleRepository>();

            services.AddScoped<IRangeAplication,    RangeAplication>();
            services.AddScoped<IRangeService    ,   RangeService>();
            services.AddScoped<IRangeRepository,    RangeRepository>();
            #endregion




            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecret"])),
                        RequireExpirationTime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var fglfrom = Configuration["flagPeticionFrontLocal"];
            var swaggerBasePath = "";

            app.UseSwagger(c =>
            {
                // c.SerializeAsV2 = "";
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{swaggerBasePath}" } };
                });
            });


          
            //app.UseDeveloperExceptionPage();
            //app.UseSwagger();
            //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sweet Sdreams v1"));

            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sweet Sdreams API v1"));
            //}


            app.UseCors(_policyName);
            app.UseHttpsRedirection();

            app.UseRouting();

            if (fglfrom == "1")
            {
                app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod()
                    .WithOrigins(
                      "*"));
            }
            else
            {
                app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(
                 "http://localhost:4200"));
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use(async (context, next) =>
            {
                context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null;
                await next.Invoke();
            });
        }
    }
}
