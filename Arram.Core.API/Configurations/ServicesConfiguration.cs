using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Arram.Core.Business;
using Arram.Core.Business.Interfaces;
using Arram.Core.Repo.Infrastructure.Contract;
using Arram.Core.Repo.Repositories;

namespace Arram.Core.API.Configurations
{
  public static class ServicesConfiguration
  {
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
      services.AddTransient<ILicenceLogic, LicenceLogic>();
      services.AddTransient<ILicenceRepository, LicenceRepository>();

      services.AddTransient<IArticleLogic, ArticleLogic>();
      services.AddTransient<IArticleRepository, ArticleRepository>();

      services.AddTransient<IIllustrationLogic, IllustrationLogic>();
      services.AddTransient<IIllustrationRepository, IllustrationRepository>();

      services.AddTransient<ILienLogic, LienLogic>();
      services.AddTransient<ILienRepository, LienRepository>();

      services.AddTransient<IParametreLogic, ParametreLogic>();
      services.AddTransient<IParametreRepository, ParametreRepository>();

      services.AddTransient<IPhotoLogic, PhotoLogic>();
      services.AddTransient<IPhotoRepository, PhotoRepository>();

      services.AddTransient<ICategorieLienLogic, CategorieLienLogic>();
      services.AddTransient<ICategorieLienRepository, CategorieLienRepository>();

      services.AddTransient<ITypeArticleLogic, TypeArticleLogic>();
      services.AddTransient<ITypeArticleRepository, TypeArticleRepository>();

      services.AddTransient<IRelaisLogic, RelaisLogic>();
      services.AddTransient<IRelaisRepository, RelaisRepository>();

      services.AddTransient<IVideoLogic, VideoLogic>();
      services.AddTransient<IVideoRepository, VideoRepository>();

      return services;
    }

    public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
    {
      return services;
    }

    public static IServiceCollection AddMiddleware(this IServiceCollection services)
    {
      services.AddControllers().AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

      });
      return services;
    }

    public static IServiceCollection AddLogging(this IServiceCollection services)
    {
      services.AddLogging(builder => builder
          .AddConsole()
          .AddFilter(level => level >= LogLevel.Information)
      );

      return services;
    }

    public static IServiceCollection AddCaching(this IServiceCollection services)
    {
      services.AddMemoryCache();
      services.AddResponseCaching();
      return services;
    }

    public static IServiceCollection AddCORS(this IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
            builder => builder.WithOrigins("http://example.com")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
      });

      return services;
    }
  }
}
