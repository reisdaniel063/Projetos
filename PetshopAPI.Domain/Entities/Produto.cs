namespace PetshopAPI.Domain.Entities;

public class Produto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Unidade { get; set; } = string.Empty;
    public decimal EstoqueAtual { get; set; }
    public decimal EstoqueMinimo { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid FornecedorId { get; set; }
    public Fornecedor Fornecedor { get; set; } = null!;

    public ICollection<MovimentacaoEstoque> Movimentacoes { get; set; } = new List<MovimentacaoEstoque>();
}
