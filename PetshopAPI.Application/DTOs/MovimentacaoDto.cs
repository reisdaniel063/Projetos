using PetshopAPI.Domain.Entities;

namespace PetshopAPI.Application.DTOs;

public class CriarMovimentacaoDto
{
    public Guid ProdutoId { get; set;}
    public TipoMovimentacao Tipo {get; set;}
    public decimal Quantidade {get; set; }
    public string Observacao {get; set; } = string.Empty;
}
public class MovimentacaoResponseDto
{
    public Guid Id { get; set;}
    public string TipoProduto {get; set; }= string.Empty;
    public TipoMovimentacao Tipo {get; set;}
    public decimal Quantidade {get; set;}
    public DateTime Data { get; set;}
    public string Observacao {get; set;} = string.Empty;
}