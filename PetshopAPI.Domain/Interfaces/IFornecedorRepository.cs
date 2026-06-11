using PetshopAPI.Domain.Entities;

namespace PetshopAPI.Domain.Interfaces;

public interface IFornecedorRepository
{
    Task<Fornecedor?> GetByIdAsync(Guid id);
    Task<IEnumerable<Fornecedor>> GetAllAsync();
    Task AddAsync(Fornecedor fornecedor);
    Task UpdateAsync(Fornecedor fornecedor);
    Task DeleteAsync(Guid id);
}