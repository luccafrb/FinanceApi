using Microsoft.EntityFrameworkCore;
using FinanceApi.Data;
using FinanceApi.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
builder.Services.AddScoped<IAdminService, AdminService>();

//Configurações de login com token
var key = Encoding.ASCII.GetBytes("SuaChaveSuperSecretaComMaisDe32Caracteres");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

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
app.UseAuthentication();
app.UseAuthorization();

app.Run();

