using ITHS.Application.Services;
using ITHS.Infrastructure.Configurations.Dependencies;
using ITHS.Infrastructure.Configurations.EntityFramework;
using ITHS.Infrastructure.Configurations.Security;
using ITHS.Infrastructure.Configurations.Swagger;
using System.Text.Json.Serialization;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;

        builder.Services.AddScoped<IPersonService, PersonService>();
        builder.Services.AddInfrastructureDependencies(config);

        builder.Services.AddITHSDbContextUsingSQLLite();
        builder.Services.AddControllers()
            .AddJsonOptions(SwaggerJsonOptionsUsingEnumAsString);

        builder.Services.AddJwtBearerAuthentication(config);

        builder.Services.AddSwaggerConfigurations();

        builder.Services.AddResponseCompression();

        RunWebApplication(builder);
    }

    static Action<Microsoft.AspNetCore.Mvc.JsonOptions> SwaggerJsonOptionsUsingEnumAsString = options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

    };

    /// <summary>
    /// Build and start the web application
    /// </summary>
    /// <param name="builder"></param>
    static void RunWebApplication(WebApplicationBuilder builder)
     {
        var app = builder.Build();

        app.UseResponseCompression();

        app.UseSwaggerConfigurations();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}