using ProdutosApi.DTOs;

namespace ProdutosApi.Repositories;

public interface IProdutoRepository
{
    public Task<IEnumerable<ProdutoResponseDTO>> GetProdutos();
    public Task<ProdutoResponseDTO> GetProduto(int id);
    public Task<ProdutoResponseDTO> CreateProduto(ProdutoRequestDTO produtoRequestDTO);
    public Task<ProdutoResponseDTO> UpdateProduto(int id, ProdutoDTO produtoDTO);
    public Task<ProdutoResponseDTO> DeleteProduto(int id);
}

