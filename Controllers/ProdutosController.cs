using Microsoft.AspNetCore.Mvc;
using ProdutosApi.Models;
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

        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int:required}")]
        public async Task<IResult> PutProduto([FromRoute]int id, [FromBody]Produto produto)
        {
            if (!ValidaProduto(produto)) { return TypedResults.BadRequest(); }

            await _unitOfWork.ProdutoRepository.UpdateProduto(id, produto);
            await _unitOfWork.Commit();
            return TypedResults.Ok(produto);
        }

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IResult> PostProduto([FromBody]Produto produto)
        {
            if (!ValidaProduto(produto)) { return TypedResults.BadRequest(); }
            await _unitOfWork.ProdutoRepository.CreateProduto(produto);
            await _unitOfWork.Commit();
            return TypedResults.Created("GetProduto", produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id:int:required}")]
        public async Task<IResult> DeleteProduto([FromRoute] int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.DeleteProduto(id);
            await _unitOfWork.Commit();

            if (produto == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(produto);
        }

        private static bool ValidaProduto(Produto produto)
        {
            if (produto is null) {  return false; }
            if (string.IsNullOrEmpty(produto.Nome)) return false;
            if (string.IsNullOrWhiteSpace(produto.Nome)) return false;
            return true;
        }
    }
}
