using Microsoft.EntityFrameworkCore;
using PetshopAPI.Domain.Entities;
using PetshopAPI.Domain.Interfaces;
using PetshopAPI.Infrastructure.Data;

namespace PetshopAPI.Infrastructure.Repositories;

public class OrdemDeCompraRepository : IOrdemDeCompraRepository
{
    private readonly AppDbContext _context;

    public OrdemDeCompraRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrdemDeCompra?> GetByIdAsync(Guid id)
        => await _context.OrdensDeCompra
            .Include(o => o.Fornecedor)
            .Include(o => o.Itens)
                .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task AddAsync(OrdemDeCompra ordem)
    {
        await _context.OrdensDeCompra.AddAsync(ordem);
        await _context.SaveChangesAsync();
    }
}