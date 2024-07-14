namespace ProdutosApi.Repositories
{
    public interface IUnitOfWork
    {
        public IProdutoRepository ProdutoRepository { get; }
        public Task Commit();
        public Task Dispose();
    }
}
