using ProdutosApi.Models;

namespace ProdutosApi.Repositories;

public interface IProdutoRepository
{
    public Task<IEnumerable<Produto>> GetProdutos();
    public Task<Produto> GetProduto(int id);
    public Task<Produto> CreateProduto(Produto produto);
    public Task<Produto> UpdateProduto(int id, Produto produto);
    public Task<Produto> DeleteProduto(int id);
}

