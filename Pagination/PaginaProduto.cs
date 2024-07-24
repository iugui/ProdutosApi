using ProdutosApi.DTOs;

namespace ProdutosApi.Pagination
{
    public class PaginaProduto
    {
        public int PaginaAtual { get; private set; }
        public int TotalPaginas { get; private set; }
        public int RegistrosPorPagina { get; private set; }
        public int QtdeRegistros { get; private set; }
        public bool TemAnterior { get; private set; } = true;
        public bool TemProxima { get; private set; } = true;

        public PaginaProduto(IEnumerable<ProdutoResponseDTO> itens, int paginaAtual, int registrosPorPagina)
        {
            QtdeRegistros = itens.Count();
            RegistrosPorPagina = registrosPorPagina;
            PaginaAtual = paginaAtual;
            TotalPaginas = (int)Math.Ceiling(QtdeRegistros / (double)RegistrosPorPagina);
            if (PaginaAtual >= TotalPaginas) {TemProxima = false;}
            if (PaginaAtual == 1) { TemAnterior = false;}
        }
    }
}
