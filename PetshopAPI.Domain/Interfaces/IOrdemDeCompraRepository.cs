using PetshopAPI.Domain.Entities;
namespace PetshopAPI.Domain.Interfaces;
public interface IOrdemDeCompraRepository
{
    Task<OrdemDeCompra?> GetByIdAsync(Guid id);
    Task AddAsync(OrdemDeCompra ordem);
}