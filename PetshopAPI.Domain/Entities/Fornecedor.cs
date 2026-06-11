namespace PetshopAPI.Domain.Entities;

public class Fornecedor
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Contato { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}