using Microsoft.EntityFrameworkCore;
using FinanceApi.Data;
using FinanceApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

// --- BLOCO 1: Adiciona o gerador do Swagger aos serviços ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Meus Serviços
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=financas.db"));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

//Adiciona configuração que lida com problemas de ciclos nos jsons
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

// --- BLOCO 2: Ativa o Swagger no pipeline de execução ---
// Geralmente ativamos apenas em ambiente de Desenvolvimento (IsDevelopment)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(); // Aqui é onde a interface visual é criada
}

app.MapControllers(); // Isso é o que "ativa" os atributos [Route]
app.UseHttpsRedirection();

app.Run();

