using PetshopAPI.Application.DTOs;
using PetshopAPI.Domain.Entities;
using PetshopAPI.Domain.Interfaces;

namespace PetshopAPI.Application.Services;

public class FornecedorService
{
    private readonly IFornecedorRepository _repository;

    public FornecedorService(IFornecedorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FornecedorResponseDto>> GetAllAsync()
    {
        var fornecedores = await _repository.GetAllAsync();
        return fornecedores.Select(f => new FornecedorResponseDto
        {
            Id = f.Id,
            Nome = f.Nome,
            Contato = f.Contato
        });
    }

    public async Task<FornecedorResponseDto?> GetByIdAsync(Guid id)
    {
        var fornecedor = await _repository.GetByIdAsync(id);
        if (fornecedor == null) return null;

        return new FornecedorResponseDto
        {
            Id = fornecedor.Id,
            Nome = fornecedor.Nome,
            Contato = fornecedor.Contato
        };
    }

    public async Task<FornecedorResponseDto> CreateAsync(CriarFornecedorDto dto)
    {
        var fornecedor = new Fornecedor
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome,
            Contato = dto.Contato
        };

        await _repository.AddAsync(fornecedor);

        return new FornecedorResponseDto
        {
            Id = fornecedor.Id,
            Nome = fornecedor.Nome,
            Contato = fornecedor.Contato
        };
    }
}