using Api.EF.Books.Data.Models;
using Api.EF.Books.Services;

namespace Api.EF.Api
{
    public static class AddHealthApiExtension
    {
        public static WebApplication AddHealthApi(this WebApplication app)
        {
            app
                .MapGet("/health", (BookService service) =>
                {
                    return Results.Ok(new { Health = "Healthy" });
                })
                .WithName("HealthCheck");


            if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Test")
            {
                app
                    .MapGet("/crash", async (BookService service) =>
                    {
                        await Task.CompletedTask;
                        throw new Exception("Simulated exception for testing purposes.");
                    })
                    .WithName("SimulateException");
            }

            return app;
        }
    }
}
