using Microsoft.EntityFrameworkCore;
using PetshopAPI.Domain.Entities;

namespace PetshopAPI.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<MovimentacaoEstoque> Movimentacoes { get; set; }
    public DbSet<OrdemDeCompra> OrdensDeCompra { get; set; }
    public DbSet<ItemOrdemDeCompra> ItensOrdemDeCompra { get; set; }
}