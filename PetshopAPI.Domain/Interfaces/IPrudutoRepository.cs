using PetshopAPI.Domain.Entities;

namespace PetshopAPI.Domain.Interfaces;
public interface IProdutoRepository
{
    Task<Produto?> GetByIdAsync(Guid id);
    Task<IEnumerable<Produto>> GetAllAsync();
    Task AddAsync(Produto produto);
    Task UpdateAsync(Produto produto);
    Task DeleteAsync(Guid id);
}