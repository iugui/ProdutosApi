// No momento ainda não implementado
namespace ProdutosApi.Pagination
{
    public class ProdutosParametros
    {
        private const int _maxRegistrosPorPagina = 50;
        public int Pagina { get; set; } = 1;
        private int _registrosPorPagina = 10;
        public int RegistrosPorPagina
        {
            get 
            {
                return _registrosPorPagina;
            }
            set
            {
                if (value > _maxRegistrosPorPagina)
                {
                    _registrosPorPagina = _maxRegistrosPorPagina;
                }
                _registrosPorPagina = value;
            }
        }
    }
}
