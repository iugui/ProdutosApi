using ProdutosApi.Context;

namespace ProdutosApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository? _produtoRepository;
        public ProdutosDb context;

        public UnitOfWork(ProdutosDb context)
        {
            this.context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                if (_produtoRepository == null)
                {
                    _produtoRepository = new ProdutoRepository(context);
                }
                return _produtoRepository;
            }
        }

        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await context.DisposeAsync();
        }
    }
}
