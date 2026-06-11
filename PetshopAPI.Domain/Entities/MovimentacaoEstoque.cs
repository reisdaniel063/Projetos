namespace PetshopAPI.Domain.Entities;

public enum TipoMovimentacao
{
    Entrada,
    Saida,
    Ajuste  
}
public class MovimentacaoEstoque
{
    public Guid Id { get; set; }
    public TipoMovimentacao Tipo { get; set; }
    public decimal Quantidade { get; set; }
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public string Observacao { get; set; } = string.Empty;

    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
}