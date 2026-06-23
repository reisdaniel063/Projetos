using PetshopAPI.Application.DTOs;
using PetshopAPI.Domain.Entities;
using PetshopAPI.Domain.Interfaces;

namespace PetshopAPI.Application.Services;

public class ProdutoService
{
    private readonly IProdutoRepository _repository;

    public ProdutoService(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProdutoResponseDto>> GetAllAsync()
    {
        var produtos = await _repository.GetAllAsync();
        return produtos.Select(p => new ProdutoResponseDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Unidade = p.Unidade,
            EstoqueAtual = p.EstoqueAtual,
            EstoqueMinimo = p.EstoqueMinimo,
            NomeFornecedor = p.Fornecedor.Nome
        });
    }

    public async Task<ProdutoResponseDto?> GetByIdAsync(Guid id)
    {
        var produto = await _repository.GetByIdAsync(id);
        if (produto == null) return null;

        return new ProdutoResponseDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Unidade = produto.Unidade,
            EstoqueAtual = produto.EstoqueAtual,
            EstoqueMinimo = produto.EstoqueMinimo,
            NomeFornecedor = produto.Fornecedor.Nome
        };
    }

    public async Task<ProdutoResponseDto> CreateAsync(CriarProdutoDto dto)
    {
        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome,
            Unidade = dto.Unidade,
            EstoqueAtual = dto.EstoqueAtual,
            EstoqueMinimo = dto.EstoqueMinimo,
            FornecedorId = dto.FornecedorId
        };

        await _repository.AddAsync(produto);

        return new ProdutoResponseDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Unidade = produto.Unidade,
            EstoqueAtual = produto.EstoqueAtual,
            EstoqueMinimo = produto.EstoqueMinimo,
            NomeFornecedor = string.Empty
        };
    }

    public async Task UpdateAsync(Guid id, AtualizarProdutoDto dto)
    {
        var produto = await _repository.GetByIdAsync(id);
        if (produto == null) return;

        produto.Nome = dto.Nome;
        produto.Unidade = dto.Unidade;
        produto.EstoqueMinimo = dto.EstoqueMinimo;

        await _repository.UpdateAsync(produto);
    }

    public async Task DeleteAsync(Guid id)
        => await _repository.DeleteAsync(id);
}