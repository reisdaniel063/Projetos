namespace PetshopAPI.Application.DTOs;

public class GerarOrdemDeCompraDto
{
    public Guid FornecedorId { get; set; }
    public List<Guid> ProdutoIds { get; set; } = new();
}

public class ItemOrdemResponseDto
{
    public string NomeProduto { get; set; } = string.Empty;
    public string Unidade { get; set; } = string.Empty;
    public decimal MediaCalculada { get; set; }
    public decimal EstoqueAtual { get; set; }
    public decimal QuantidadeSugerida { get; set; }
    public bool NecessitaCompra { get; set; }
}

public class OrdemDeCompraResponseDto
{
    public Guid Id { get; set; }
    public string NomeFornecedor { get; set; } = string.Empty;
    public int Mes { get; set; }
    public int Ano { get; set; }
    public DateTime DataGeracao { get; set; }
    public List<ItemOrdemResponseDto> Itens { get; set; } = new();
}