using System.Text;
using API.Middlwares;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReclameTrancoso.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
builder.Configuration.AddJsonFile($"appsettings.{env}.json", false, true).Build();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.ConfigureService(builder.Configuration);
builder.Services.ConfigureUseCasesHandlers();
builder.Services.ConfigureUseCasesHandlersRes();
builder.Services.ConfigureValidators();
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    int maxRetryAttempts = 5; // Número máximo de tentativas
    int delayBetweenRetries = 2000; // Tempo de espera entre tentativas em milissegundos

    for (int attempt = 0; attempt < maxRetryAttempts; attempt++)
    {
        try
        {
            // Obtenha o contexto do banco de dados
            var dbContext = services.GetRequiredService<DataContext>();
            
            // Aplique as migrações
            dbContext.Database.Migrate();
            break; // Saia do loop se a migração for bem-sucedida
        }
        catch (Exception ex)
        {
            // Trate qualquer erro que ocorra durante a migração
            Console.WriteLine($"Erro ao aplicar migrações (tentativa {attempt + 1}): {ex.Message}");

            if (attempt < maxRetryAttempts - 1)
            {
                // Aguarde antes de tentar novamente
                Console.WriteLine($"Aguardando {delayBetweenRetries / 1000} segundos antes de tentar novamente...");
                await Task.Delay(delayBetweenRetries);
            }
            else
            {
                // Se todas as tentativas falharem, lançar a exceção ou registrar o erro
                throw; // Opcional: lance a exceção novamente para tratamento posterior
            }
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseMiddleware<ExceptionsMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
