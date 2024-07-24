//No momento ainda não implementado

namespace ProdutosApi.Pagination
{
    public class ListaPaginada<T> : List<T> where T : class
    {
        public int PaginaAtual { get; private set; }
        public int TotalPaginas { get; private set; }
        public int RegistrosPorPagina { get; private set; }
        public int QtdeRegistros { get; private set; }

        private bool _temAnterior = false;
        public bool TemAnterior
        {
            get { return _temAnterior; }
            set
            {
                if (PaginaAtual > 1)
                {
                    _temAnterior = true;
                }
            }
        }

        private bool _temProxima = true;
        public bool TemProxima
        {
            get { return _temProxima; }
            set
            {
                if (PaginaAtual >= TotalPaginas)
                {
                    _temProxima = false;
                }
            }
        }

        public ListaPaginada(List<T> itens,int quantidade, int paginaAtual, int registrosPorPagina)
        {
            QtdeRegistros = quantidade;
            RegistrosPorPagina = registrosPorPagina;
            PaginaAtual = paginaAtual;
            TotalPaginas = (int)Math.Ceiling(QtdeRegistros / (double)RegistrosPorPagina);
            AddRange(itens);
        }

        public static ListaPaginada<T> ToListaPaginada(IQueryable<T> source, int paginaAtual, int registrosPorPagina)
        {
            var count = source.Count();
            var items = source.Skip((paginaAtual - 1) * registrosPorPagina).Take(registrosPorPagina).ToList();

            return new ListaPaginada<T>(items, count, paginaAtual, registrosPorPagina);
        }
    }
}
