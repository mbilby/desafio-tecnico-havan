using Questao5.Repositories;
using Questao5.Services;
using System.Text.Json.Serialization;

namespace Questao5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter()
                    );
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton
                <ITarefaRepository, TarefaRepository>();

            builder.Services.AddScoped
                <ITarefaService, TarefaService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.MapControllers();

            app.MapGet("/", () =>
                "API de gerenciamento de tarefas está em execução."
            );

            app.Run();
        }
    }
}