using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProdutosApi.Context;
using ProdutosApi.DTOs;
using ProdutosApi.Models;
using ProdutosApi.Pagination;
using ProdutosApi.Repositories;

namespace ProdutosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProdutosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<IResult> GetProdutos()
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutos();
            return TypedResults.Ok(produtos);
        }

        // GET: api/Produtos/5
        [HttpGet("{id:int:required}", Name = "GetProduto")]
        public async Task<IResult> GetProduto([FromRoute]int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetProduto(id);
            if (produto is null) return TypedResults.NotFound();
            return TypedResults.Ok(produto);
        }

        // GET: api/produtos/paginacao
        [HttpGet("paginacao")]
        public async Task<IResult> GetProdutos([FromQuery]int pagina = 1, [FromQuery]int registrosPorPagina = 10)
        {
            if (pagina < 1) 
            { 
                return TypedResults.BadRequest(new {Message = "O valor da página não pode ser menor que 1"}); 
            }
            if (registrosPorPagina > 50)
            {
                return TypedResults.BadRequest(new {Message = "A quantidade de registros por página não pode ser maior que 50"});
            }

            var produtos = await _unitOfWork.ProdutoRepository.GetProdutos();
            var metadata = new PaginaProduto(produtos, pagina, registrosPorPagina);
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var produtosDTO = await _unitOfWork.ProdutoRepository.GetProdutos(pagina, registrosPorPagina);
            return TypedResults.Ok(produtosDTO);
        }

        // GET: api/produtos/filtro
        [HttpGet("filtro")]
        public async Task<IResult> GetProdutosFiltrados([FromQuery]string nome)
        {
            var produtosFiltrados = await _unitOfWork.ProdutoRepository.GetProdutosFiltrados(nome);
            if (produtosFiltrados is null) return TypedResults.NotFound();
            return TypedResults.Ok(produtosFiltrados);
        }

        // GET: api/produtos/ordem
        [HttpGet("ordem")]
        public async Task<IResult> GetProdutosOrdenados([FromQuery]string ordem="nome_asc")
        {
            var produtosOrdenados = await _unitOfWork.ProdutoRepository.GetProdutosOrdenados(ordem);
            if (produtosOrdenados is null) return TypedResults.NotFound();
            return TypedResults.Ok(produtosOrdenados);
        }

        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int:required}")]
        public async Task<IResult> PutProduto([FromRoute]int id, [FromBody]ProdutoDTO produtoDTO)
        {
            if (!ValidaProduto(produtoDTO)) { return TypedResults.BadRequest(); }

            await _unitOfWork.ProdutoRepository.UpdateProduto(id, produtoDTO);
            await _unitOfWork.Commit();
            var produtoResponseDTO = produtoDTO.Adapt<ProdutoResponseDTO>();
            return TypedResults.Ok(produtoResponseDTO);
        }

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IResult> PostProduto([FromBody]ProdutoRequestDTO produtoRequestDTO)
        {
            if (!ValidaProduto(produtoRequestDTO)) { return TypedResults.BadRequest(); }
            await _unitOfWork.ProdutoRepository.CreateProduto(produtoRequestDTO);
            await _unitOfWork.Commit();
            var produtoResponseDTO = produtoRequestDTO.Adapt<ProdutoResponseDTO>();
            return TypedResults.Created("GetProduto", produtoResponseDTO);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id:int:required}")]
        public async Task<IResult> DeleteProduto([FromRoute] int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.DeleteProduto(id);

            if (produto == null)
            {
                return TypedResults.NotFound();
            }

            var produtoResponseDTO = produto.Adapt<ProdutoResponseDTO>();

            await _unitOfWork.Commit();
            return TypedResults.Ok(produtoResponseDTO);
        }

        private static bool ValidaProduto(ProdutoRequestDTO produto)
        {
            if (produto is null) {  return false; }
            if (string.IsNullOrEmpty(produto.Nome)) return false;
            if (string.IsNullOrWhiteSpace(produto.Nome)) return false;
            return true;
        }

        private static bool ValidaProduto(ProdutoDTO produto)
        {
            if (produto is null) { return false; }
            if (string.IsNullOrEmpty(produto.Nome)) return false;
            if (string.IsNullOrWhiteSpace(produto.Nome)) return false;
            return true;
        }
    }
}
