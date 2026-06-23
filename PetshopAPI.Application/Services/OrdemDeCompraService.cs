using PetshopAPI.Application.DTOs;
using PetshopAPI.Domain.Entities;
using PetshopAPI.Domain.Interfaces;

namespace PetshopAPI.Application.Services;

public class OrdemDeCompraService
{
    private readonly IOrdemDeCompraRepository _repository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly IFornecedorRepository _fornecedorRepository;

    public OrdemDeCompraService(
        IOrdemDeCompraRepository repository,
        IProdutoRepository produtoRepository,
        IMovimentacaoRepository movimentacaoRepository,
        IFornecedorRepository fornecedorRepository)
    {
        _repository = repository;
        _produtoRepository = produtoRepository;
        _movimentacaoRepository = movimentacaoRepository;
        _fornecedorRepository = fornecedorRepository;
    }

    public async Task<OrdemDeCompraResponseDto> GerarAsync(GerarOrdemDeCompraDto dto)
    {
        var fornecedor = await _fornecedorRepository.GetByIdAsync(dto.FornecedorId);
        if (fornecedor == null) throw new Exception("Fornecedor não encontrado.");

        var itens = new List<ItemOrdemDeCompra>();
        var itensResponse = new List<ItemOrdemResponseDto>();

        var hoje = DateTime.UtcNow;
        var de = hoje.AddDays(-120);

        foreach (var produtoId in dto.ProdutoIds)
        {
            var produto = await _produtoRepository.GetByIdAsync(produtoId);
            if (produto == null) continue;

            var movimentacoes = await _movimentacaoRepository.GetByProdutoIdAsync(produtoId, de, hoje);

            var saidas = movimentacoes
                .Where(m => m.Tipo == TipoMovimentacao.Saida)
                .ToList();

            var mediaPonderada = CalcularMediaPonderada(saidas, hoje);

            var diasRestantesMes = DateTime.DaysInMonth(hoje.Year, hoje.Month) - hoje.Day;
            var quantidadeSugerida = (mediaPonderada * diasRestantesMes / 30) - produto.EstoqueAtual;

            var item = new ItemOrdemDeCompra
            {
                Id = Guid.NewGuid(),
                ProdutoId = produtoId,
                MediaCalculada = mediaPonderada,
                EstoqueAtual = produto.EstoqueAtual,
                QuantidadeSugerida = quantidadeSugerida
            };

            itens.Add(item);

            itensResponse.Add(new ItemOrdemResponseDto
            {
                NomeProduto = produto.Nome,
                Unidade = produto.Unidade,
                MediaCalculada = Math.Round(mediaPonderada, 2),
                EstoqueAtual = produto.EstoqueAtual,
                QuantidadeSugerida = Math.Round(Math.Max(quantidadeSugerida, 0), 0),
                NecessitaCompra = quantidadeSugerida > 0
            });
        }

        var ordem = new OrdemDeCompra
        {
            Id = Guid.NewGuid(),
            FornecedorId = dto.FornecedorId,
            Mes = hoje.Month,
            Ano = hoje.Year,
            DataGeracao = hoje,
            Itens = itens
        };

        await _repository.AddAsync(ordem);

        return new OrdemDeCompraResponseDto
        {
            Id = ordem.Id,
            NomeFornecedor = fornecedor.Nome,
            Mes = ordem.Mes,
            Ano = ordem.Ano,
            DataGeracao = ordem.DataGeracao,
            Itens = itensResponse
        };
    }

    private decimal CalcularMediaPonderada(List<MovimentacaoEstoque> saidas, DateTime hoje)
    {
        if (!saidas.Any()) return 0;

        // Agrupa saídas por mês
        var saidasPorMes = saidas
            .GroupBy(s => new { s.Data.Year, s.Data.Month })
            .OrderBy(g => g.Key.Year)
            .ThenBy(g => g.Key.Month)
            .ToList();

        var totalPeso = 0;
        var totalPonderado = 0m;

        for (int i = 0; i < saidasPorMes.Count; i++)
        {
            var grupo = saidasPorMes[i];
            var peso = i + 1; // mês mais antigo = peso 1, mais recente = peso maior

            var totalMes = grupo.Sum(s => s.Quantidade);

            // Projeta o mês atual com base nos dias decorridos
            if (grupo.Key.Year == hoje.Year && grupo.Key.Month == hoje.Month)
            {
                var diasDecorridos = hoje.Day;
                var diasNoMes = DateTime.DaysInMonth(hoje.Year, hoje.Month);
                totalMes = totalMes * diasNoMes / diasDecorridos;
            }

            totalPonderado += totalMes * peso;
            totalPeso += peso;
        }

        return totalPeso == 0 ? 0 : totalPonderado / totalPeso;
    }

    public async Task<OrdemDeCompraResponseDto?> GetByIdAsync(Guid id)
    {
        var ordem = await _repository.GetByIdAsync(id);
        if (ordem == null) return null;

        return new OrdemDeCompraResponseDto
        {
            Id = ordem.Id,
            NomeFornecedor = ordem.Fornecedor.Nome,
            Mes = ordem.Mes,
            Ano = ordem.Ano,
            DataGeracao = ordem.DataGeracao,
            Itens = ordem.Itens.Select(i => new ItemOrdemResponseDto
            {
                NomeProduto = i.Produto.Nome,
                Unidade = i.Produto.Unidade,
                MediaCalculada = Math.Round(i.MediaCalculada, 2),
                EstoqueAtual = i.EstoqueAtual,
                QuantidadeSugerida = Math.Round(Math.Max(i.QuantidadeSugerida, 0), 0),
                NecessitaCompra = i.QuantidadeSugerida > 0
            }).ToList()
        };
    }
}