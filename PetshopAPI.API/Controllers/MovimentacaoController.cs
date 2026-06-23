using Microsoft.AspNetCore.Mvc;
using PetshopAPI.Application.DTOs;
using PetshopAPI.Application.Services;

namespace PetshopAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacaoController : ControllerBase
{
    private readonly MovimentacaoService _service;
    
    public MovimentacaoController(MovimentacaoService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CriarMovimentacaoDto dto)
    {
        var  movimentacao = await _service.CreateAsync(dto);
        return Ok(movimentacao);
    }
    [HttpGet("{produtoId}")]
    public async Task<IActionResult> GetByProduto(Guid produtoid, [FromQuery] DateTime de, [FromQuery] DateTime ate)
    {
        var movimentacoes = await _service.GetByProdutoAsync(produtoid, de, ate);
        return Ok(movimentacoes);
    }
}