using Microsoft.EntityFrameworkCore;
using ProdutosApi.Context;
using ProdutosApi.Models;

namespace ProdutosApi.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    // Injeção de dependência
    private readonly ProdutosDb _context;
    public ProdutoRepository(ProdutosDb context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> GetProdutos()
    {
        var produtos = await _context.Produtos.ToListAsync();
        return produtos;
    }

    public async Task<Produto> GetProduto(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        #pragma warning disable
        return produto;
        // Desabilitando o warning de null pois tratamos esse caso em ProdutosController
    }

    public async Task<Produto> CreateProduto(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    public async Task<Produto> UpdateProduto(int id, Produto produto)
    {
        if (id != produto.Id)
        {
            throw new Exception("O id deve ser igual ao id do produto");
        }

        _context.Entry(produto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProdutoExists(id))
            {
                throw new ArgumentNullException(nameof(produto));
            }
        }

        return produto;
    }

    public async Task<Produto> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null)
        {
            #pragma warning disable
            return null;
            // Desabilitando o warning de null pois tratamos esse caso em ProdutosController
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    private bool ProdutoExists(int id)
    {
        return _context.Produtos.Any(e => e.Id == id);
    }
}

