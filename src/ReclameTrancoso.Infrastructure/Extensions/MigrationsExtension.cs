using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure.Extensions;

public static class MigrationsExtension
{
    public async static Task RunMigrations(this IServiceProvider service)
    {
        using (var scope = service.CreateScope())
        {
            var services = scope.ServiceProvider;
            int maxRetryAttempts = 2;
            int delayBetweenRetries = 2000;

            for (int attempt = 0; attempt < maxRetryAttempts; attempt++)
            {
                try
                {
                    var dbContext = services.GetRequiredService<DataContext>();

                    var pendingMigrations = dbContext.Database.GetPendingMigrations();
                    if (pendingMigrations.Any())
                    {
                        Console.WriteLine("Há migrações pendentes. Aplicando migrações...");
                        dbContext.Database.Migrate();
                    }
                    else
                    {
                        Console.WriteLine("Nenhuma migração pendente.");
                    }
                    break;
                }
                catch (PostgresException ex)
                {
                    if (ex.SqlState == "42P07")
                    {
                        Console.WriteLine("Erro: uma tabela já existe. Abortando migrações.");
                        break;
                    }
                    Console.WriteLine($"Erro ao verificar/aplicar migrações (tentativa {attempt + 1}): {ex.Message}");

                    if (attempt < maxRetryAttempts - 1)
                    {
                        Console.WriteLine($"Aguardando {delayBetweenRetries / 1000} segundos antes de tentar novamente...");
                        await Task.Delay(delayBetweenRetries);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}