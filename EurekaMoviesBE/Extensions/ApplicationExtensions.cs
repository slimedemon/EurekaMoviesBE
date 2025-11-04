using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using EurekaMoviesBE.Features.Commands.UserCommands.Register;
using EurekaMoviesBE.Features.Commands.UserCommands.Register.PostProcessor;
using EurekaMoviesBE.Middlewares;
using EurekaMoviesBE.Services.DuendeServices;
using EurekaMoviesBE.Validation;
using FluentValidation;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace EurekaMoviesBE.Extensions
{
    public static class ApplicationExtensions
    {

        public static IServiceCollection AddIdentityServerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection(DbSettingsOptions.OptionName).Get<DbSettingsOptions>();
            var migrationsAssembly = typeof(Program).Assembly.FullName;

            services.AddIdentityServer()
                .AddSigningCredential(CryptographyHelper.CreateRsaKey(), IdentityServerConstants.RsaSigningAlgorithm.RS256)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseNpgsql(dbSettings?.PostgresConnection ?? throw new InvalidOperationException("PostgresConnection is not configured"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseNpgsql(dbSettings?.PostgresConnection ?? throw new InvalidOperationException("PostgresConnection is not configured"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddAspNetIdentity<User>()
                .AddProfileService<ProfileService>();

            return services;
        }


        public static IServiceCollection AddCustomDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseOptions = configuration.GetSection(DbSettingsOptions.OptionName).Get<DbSettingsOptions>();
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseNpgsql(databaseOptions?.PostgresConnection ?? throw new InvalidOperationException("PostgresConnection is not configured"));
                });

            services.AddDbContext<TmdbDbContext>(
                options =>
                {
                    options.UseMongoDB(databaseOptions?.MongoDbConnection ?? throw new InvalidOperationException("MongoDbConnection is not configured"),
                        databaseOptions?.MongoDbName ?? throw new InvalidOperationException("MongoDbName is not configured"));
                });

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMailSenderService, MailSenderService>();
            services.AddScoped<IGoogleService, GoogleService>();
            services.AddScoped<IRecommendationService, RecommendationService>();
            services.AddTransient<IProfileService, ProfileService>();
            return services;
        }

        public static IServiceCollection AddUnitOfWorks(this IServiceCollection services)
        {
            services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();
            services.AddScoped<ITmdbUnitOfWork, TmdbUnitOfWork>();
            return services;
        }

        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationOptions = configuration.GetSection(AuthenticationOptions.OptionName).Get<AuthenticationOptions>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
                {
                    option.Authority = authenticationOptions?.Authority ?? throw new InvalidOperationException("Authority is not configured");
                    option.RequireHttpsMetadata = false;
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = CryptographyHelper.CreateRsaKey()
                    };
                });

            return services;
        }

        public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
        {
            services.AddAuthorization(option =>
            {
                option.AddPolicy(SystemRole.Administrator,
                    policy => policy.RequireAssertion(context =>
                        context.User.HasClaim(claim => claim.Type == "Role" && claim.Value.Equals(SystemRole.Administrator))
                    ));
                option.AddPolicy(SystemRole.Viewer,
                    policy => policy.RequireAssertion(context =>
                        context.User.HasClaim(claim => claim.Type == "Role" && claim.Value.Equals(SystemRole.Viewer))
                    ));
            });
            return services;
        }

        public static IServiceCollection AddMemoryCacheService(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IMemoryCacheService, MemoryCacheService>();
            return services;
        }

        public static IServiceCollection AddMeditorConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.AutoRegisterRequestProcessors = true;
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));

            return services;
        }

        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                );
            });
            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "EurekaMoviesBE", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            return services;
        }

        public static IServiceCollection AddCustomHttpContextAccessor(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<ICustomHttpContextAccessor, CustomHttpContextAccessor>();
            return services;
        }

        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<ErrorHandlingMiddleware, ErrorHandlingMiddleware>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
