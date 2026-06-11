namespace PetshopAPI.Domain.Entities
{
    public class OrdemDeCompra
    {
        public Guid Id { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public DateTime DataGeracao { get; set; } = DateTime.UtcNow;
        
        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; } = null!;

        public ICollection<ItemOrdemDeCompra> Itens { get; set; } = new List<ItemOrdemDeCompra>();
        }
}