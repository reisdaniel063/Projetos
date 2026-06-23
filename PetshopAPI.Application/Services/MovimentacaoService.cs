using PetshopAPI.Application.DTOs;
using PetshopAPI.Domain.Entities;
using PetshopAPI.Domain.Interfaces;

namespace PetshopAPI.Application.Services;
public class MovimentacaoService
{
    private readonly IMovimentacaoRepository _repository;
    private readonly IProdutoRepository _produtoRepository;

    public MovimentacaoService(IMovimentacaoRepository repository, IProdutoRepository produtoRepository)
    {
        _repository = repository;
        _produtoRepository = produtoRepository;
    }

    public async Task<MovimentacaoResponseDto> CreateAsync(CriarMovimentacaoDto dto)
    {
        var produto = await _produtoRepository.GetByIdAsync(dto.ProdutoId);
        if(produto == null) throw new Exception("Produto nao encontrado");

        var movimentacao = new MovimentacaoEstoque
        {
            Id = Guid.NewGuid(),
            ProdutoId = dto.ProdutoId,
            Tipo = dto.Tipo,
            Quantidade = dto.Quantidade,
            Observacao = dto.Observacao,
            Data = DateTime.UtcNow
        };

        //Atualiza o estoque do produto
        if(dto.Tipo == TipoMovimentacao.Entrada)
        produto.EstoqueAtual += dto.Quantidade;
        else
        produto.EstoqueAtual -= dto.Quantidade;

        await _repository.AddAsync(movimentacao);
        await _produtoRepository.UpdateAsync(produto);

        return new MovimentacaoResponseDto
        {
            Id = movimentacao.Id,
            TipoProduto = produto.Nome,
            Tipo = movimentacao.Tipo,
            Quantidade = movimentacao.Quantidade,
            Data = movimentacao.Data,
            Observacao = movimentacao.Observacao
        };  
    }
    public async Task<IEnumerable<MovimentacaoResponseDto>> GetByProdutoAsync(Guid produtoId, DateTime de, DateTime ate)
    {
        var movimentacoes = await _repository.GetByProdutoIdAsync(produtoId, de, ate);
        var produto = await _produtoRepository.GetByIdAsync(produtoId);

        return movimentacoes.Select( m => new MovimentacaoResponseDto
        {
            Id = m.Id,
            TipoProduto = produto?.Nome ?? string.Empty,
            Tipo = m.Tipo,
            Quantidade = m.Quantidade,
            Data = m.Data,
            Observacao = m.Observacao    
        });
    }
}