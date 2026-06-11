using PetshopAPI.Domain.Entities;

namespace PetshopAPI.Domain.Interfaces;
public interface IMovimentacaoRepository
{
    Task<IEnumerable<MovimentacaoEstoque>> GetByProdutoIdAsync(Guid produtoId, DateTime de, DateTime ate);
    Task AddAsync(MovimentacaoEstoque movimentacao);
}