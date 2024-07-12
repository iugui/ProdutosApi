using Microsoft.EntityFrameworkCore;
using ProdutosApi.Models;

namespace ProdutosApi.Context;

public class ProdutosDb : DbContext
{
    public ProdutosDb(DbContextOptions<ProdutosDb> options) : base(options) { }
        
    // Configurando a tabela produto
    public DbSet<Produto> Produtos { get; set; }
}

