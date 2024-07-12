using Microsoft.EntityFrameworkCore;
using ProdutosApi.Context;

var builder = WebApplication.CreateBuilder(args);

// Adicionando servi�os ao container de depend�ncias
string conexao = @"Server=.\;Database=Produtos;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
builder.Services.AddDbContext<ProdutosDb>
    (
        options => options.UseSqlServer(conexao)
    );
builder.Services.AddControllers();
// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Contruindo a aplic���o
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
