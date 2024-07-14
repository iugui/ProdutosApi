using System.ComponentModel.DataAnnotations;

namespace ProdutosApi.DTOs
{
    public class ProdutoRequestDTO
    {
        [StringLength(255)]
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
    }
}
