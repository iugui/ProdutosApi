using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProdutosApi.Context;
using ProdutosApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adicionando serviços ao container de dependências
string conexao = @"Server=.\;Database=Produtos;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
builder.Services.AddDbContext<ProdutosDb>
    (
        options => options.UseSqlServer(conexao)
        
    );

builder.Services.AddControllers();
// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Contruindo a aplicação
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
