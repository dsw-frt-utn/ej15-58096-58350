using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Api.Middleware;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Data;

namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddHealthChecks();

            builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapHealthChecks("/health-check");
            

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); 
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            // --- ENDPOINTS TEMPORALES PARA PROBAR EL MIDDLEWARE ---

            app.MapGet("/error-500", () => {
                // Simulamos que se rompe algo genérico
                throw new Exception("¡Se cayó la base de datos de mentira!");
            });

            app.MapGet("/error-400", () => {
                // Simulamos que falló una validación de negocio
                throw new ValidationException("El nombre del médico no puede estar vacío");
            });

            app.Run();
        }
    }
}
