using Microsoft.AspNetCore.Mvc;
using ProdutosApi.Models;
using ProdutosApi.Repositories;

namespace ProdutosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repository;
        public ProdutosController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<IResult> GetProdutos()
        {
            var produtos = await _repository.GetProdutos();
            return TypedResults.Ok(produtos);
        }

        // GET: api/Produtos/5
        [HttpGet("{id:int:required}", Name = "GetProduto")]
        public async Task<IResult> GetProduto(int id)
        {
            var produto = await _repository.GetProduto(id);
            if (produto is null) return TypedResults.NotFound();
            return TypedResults.Ok(produto);
        }

        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int:required}")]
        public async Task<IResult> PutProduto(int id, Produto produto)
        {
            if (produto is null) return TypedResults.BadRequest();
            await _repository.UpdateProduto(id, produto);
            return TypedResults.Ok(produto);
        }

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IResult> PostProduto(Produto produto)
        {
            if (produto is null) return TypedResults.BadRequest();
            await _repository.CreateProduto(produto);
            return TypedResults.Created("GetProduto", produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id:int:required}")]
        public async Task<IResult> DeleteProduto(int id)
        {
            var produto = await _repository.DeleteProduto(id);

            if (produto == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(produto);
        }
    }
}
