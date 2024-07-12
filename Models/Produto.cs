using System.ComponentModel.DataAnnotations;

namespace ProdutosApi.Models;

public class Produto
{
    public int Id { get; set; }
    [StringLength(255)]
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
}

