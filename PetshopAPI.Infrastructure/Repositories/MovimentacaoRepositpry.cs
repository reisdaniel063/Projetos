using Microsoft.EntityFrameworkCore;
using PetshopAPI.Domain.Entities;
using PetshopAPI.Domain.Interfaces;
using PetshopAPI.Infrastructure.Data;

namespace PetshopAPI.Infrastructure.Repositories;

public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly AppDbContext _context;

    public MovimentacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovimentacaoEstoque>> GetByProdutoIdAsync(Guid produtoId, DateTime de, DateTime ate)
        => await _context.Movimentacoes
            .Where(m => m.ProdutoId == produtoId && m.Data >= de && m.Data <= ate)
            .OrderBy(m => m.Data)
            .ToListAsync();

    public async Task AddAsync(MovimentacaoEstoque movimentacao)
    {
        await _context.Movimentacoes.AddAsync(movimentacao);
        await _context.SaveChangesAsync();
    }
}