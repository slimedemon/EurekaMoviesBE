using EurekaMovieBE.Extensions;
using EurekaMovieBE.HttpClientCustom;
using EurekaMovieBE.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EurekaMovieBE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var httpClientConfig = builder.Configuration.GetSection(HttpClientOption.OptionName).Get<HttpClientOption>();

            if(httpClientConfig == null )
            {
                throw new Exception("HttpClientConfig is not configured properly.");
            }

            builder.Services.Configure<LLMServiceOption>(builder.Configuration.GetSection(LLMServiceOption.OptionName));
            builder.Services.Configure<DbSettingsOptions>(builder.Configuration.GetSection(DbSettingsOptions.OptionName));
            builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection(AuthenticationOptions.OptionName));
            builder.Services.AddOpenApi(); ;

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddCustomDbContexts(builder.Configuration);

            builder.Services.AddServices();

            builder.Services.AddUnitOfWorks();

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServerConfiguration(builder.Configuration);

            builder.Services.AddAuthenticationConfiguration(builder.Configuration);

            builder.Services.AddAuthorizationConfiguration();

            builder.Services.AddMeditorConfiguration();

            builder.Services.AddMemoryCacheService();

            builder.Services.AddSwaggerConfiguration();

            builder.Services.AddCustomHttpContextAccessor();

            builder.Services.AddHttpClientCustom(httpClientConfig);

            builder.Services.AddMiddlewares();

            builder.Services.AddValidators();

            builder.Services.AddCustomCors();

            var app = builder.Build();

            if (!app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseCors("AllowAllOrigins");

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
