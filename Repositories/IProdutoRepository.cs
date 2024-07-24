using ProdutosApi.DTOs;
using ProdutosApi.Pagination;

namespace ProdutosApi.Repositories;

public interface IProdutoRepository
{
    public Task<IEnumerable<ProdutoResponseDTO>> GetProdutos();
    public Task<ProdutoResponseDTO> GetProduto(int id);
    public Task<IEnumerable<ProdutoResponseDTO>> GetProdutos(int pagina, int registrosPorPagina);
    public Task<IEnumerable<ProdutoResponseDTO>> GetProdutosFiltrados(string nome);
    public Task<IEnumerable<ProdutoResponseDTO>> GetProdutosOrdenados(string ordem);
    public Task<ProdutoResponseDTO> CreateProduto(ProdutoRequestDTO produtoRequestDTO);
    public Task<ProdutoResponseDTO> UpdateProduto(int id, ProdutoDTO produtoDTO);
    public Task<ProdutoResponseDTO> DeleteProduto(int id);
}

