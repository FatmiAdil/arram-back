using Arram.Core.API.Configurations;
using Arram.Core.Common.Logging;
using Arram.Core.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Reflection;


namespace Arram.Core.API
{

  public class Startup
  {
    private static string strConn = string.Empty;
    private static LoggingService sl = null;
    public IHostEnvironment HostingEnvironment { get; private set; }
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration, IHostEnvironment env)
    {
      Configuration = configuration;
      HostingEnvironment = env;

      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();

      Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      sl = new LoggingService(Configuration);
      sl.WriteUsage(new LogItem() { Message = "startup " });
      services.AddControllersWithViews();
      services.AddRazorPages();

      services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
      {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
      }));
      strConn = Configuration["ConnectionStrings:DefaultConnection"];
      services.AddDbContext<ArramContext>(options =>
      {
        options.EnableSensitiveDataLogging(true);
        options.UseSqlServer(strConn,
        sqlServerOptionsAction: sqlOptions =>
        {
          sqlOptions.EnableRetryOnFailure(
              maxRetryCount: 5,
              maxRetryDelay: TimeSpan.FromSeconds(30),
              errorNumbersToAdd: null);
        });
      });

      services.Configure<MvcOptions>(options =>
      {
        options.MaxValidationDepth = 2;
      });

      services.AddTransient<ILoggingService, LoggingService>();

      var service = services.BuildServiceProvider().GetService<ILoggingService>();

      services.AddMvc(options =>
      {
        options.Filters.Add(new TrackPerformanceFilter(Assembly.GetExecutingAssembly().GetName().Name.ToLower(), "IHM", service));
      });

      services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
          options.Authority = "http://localhost:5000"; // Configuration.GetValue("api_URL"); // "http://dev-amana.atlas-flotte.com";
          options.RequireHttpsMetadata = false;
          options.Audience = "web_api";
        });


      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

      services.ConfigureRepositories()
                .ConfigureSupervisor()
                .AddMiddleware()
                .AddAppSettings(Configuration)
                .AddCaching();

      services.AddSingleton<TrackPerformanceFilter>(_ => new TrackPerformanceFilter(Assembly.GetExecutingAssembly().GetName().Name.ToLower(), "IHM", service));
      services.AddControllersWithViews();

      services.AddSwaggerGen(s => s.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Arram API",
        Description = "Arram web api en dotnet core 3.1"
      }));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
    {
      if (env.IsDevelopment())
      {
        IdentityModelEventSource.ShowPII = true;
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }


      app.UseStaticFiles();
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "photos")),
        RequestPath = "/photos"
      });
      app.UseAuthentication();
      app.UseRouting();
      app.UseCors("CorsPolicy");
      app.UseAuthorization();
      app.UseHttpsRedirection();

      app.UseSwagger();
      app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.jsonswagger/v1/swagger.json", "v1 docs"));
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
    }
  }
}
