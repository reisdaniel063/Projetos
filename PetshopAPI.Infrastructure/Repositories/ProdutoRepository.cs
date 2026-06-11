using Microsoft.EntityFrameworkCore;
using PetshopAPI.Domain.Entities;
using PetshopAPI.Domain.Interfaces;
using PetshopAPI.Infrastructure.Data;


namespace PetshopAPI.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;


    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Produto?> GetByIdAsync(Guid id)   
        => await _context.Produtos.Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Id ==id);

    public async Task<IEnumerable<Produto>> GetAllAsync()
        => await _context.Produtos.Include(p => p.Fornecedor).ToListAsync();


    public async Task AddAsync(Produto produto)
    {
        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();
    }    

    public async Task UpdateAsync(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
    }

        public async Task DeleteAsync(Guid id)
    {
        var produto = await GetByIdAsync(id);
        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }


}
