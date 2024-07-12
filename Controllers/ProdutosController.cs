using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutosApi.Context;
using ProdutosApi.Models;

namespace ProdutosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        // Injeção de dependência
        private readonly ProdutosDb _context;
        public ProdutosController(ProdutosDb context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<IResult> GetProdutos()
        {
            return TypedResults.Ok(await _context.Produtos.ToListAsync());
        }

        // GET: api/Produtos/5
        [HttpGet("{id:int:required}", Name = "GetProduto")]
        public async Task<IResult> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(produto);
        }

        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int:required}")]
        public async Task<IResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return TypedResults.BadRequest();
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
                    return TypedResults.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return TypedResults.NoContent();
        }

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IResult> PostProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return TypedResults.CreatedAtRoute("GetProduto", produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id:int:required}")]
        public async Task<IResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return TypedResults.NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
