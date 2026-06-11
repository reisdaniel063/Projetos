using Microsoft.EntityFrameworkCore;
using PetshopAPI.Domain.Entities;
using PetshopAPI.Domain.Interfaces;
using PetshopAPI.Infrastructure.Data;

namespace PetshopAPI.Infrastructure.Repositories;

public class FornecedorRepository : IFornecedorRepository
{
    private readonly AppDbContext _context;

    public FornecedorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Fornecedor?> GetByIdAsync(Guid id)
        => await _context.Fornecedores.Include(f => f.Produtos).FirstOrDefaultAsync(f => f.Id == id);

    public async Task<IEnumerable<Fornecedor>> GetAllAsync()
        => await _context.Fornecedores.ToListAsync();

    public async Task AddAsync(Fornecedor fornecedor)
    {
        await _context.Fornecedores.AddAsync(fornecedor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Fornecedor fornecedor)
    {
        _context.Fornecedores.Update(fornecedor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var fornecedor = await GetByIdAsync(id);
        if (fornecedor != null)
        {
            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();
        }
    }
}