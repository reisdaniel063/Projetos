namespace PetshopAPI.Domain.Entities;

public class ItemOrdemDeCompra
{
    public Guid Id { get; set; }
    public decimal MediaCalculada { get; set; }
    public decimal EstoqueAtual { get; set; }
    public decimal QuantidadeSugerida { get; set; }

    public Guid OrdemDeCompraId { get; set; }
    public OrdemDeCompra OrdemDeCompra { get; set; } = null!;

    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
}