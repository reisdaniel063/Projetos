using Microsoft.AspNetCore.Mvc;
using PetshopAPI.Application.DTOs;
using PetshopAPI.Application.Services;
using PetshopAPI.Domain.Entities;

namespace PetshopAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoService _service;

    public ProdutoController (ProdutoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var produtos = await _service.GetAllAsync();
        return Ok(produtos);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var produto = await _service.GetByIdAsync(id);
        if(produto ==null) return NotFound();
        return Ok(produto);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CriarProdutoDto dto)
    {
        var produto = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = produto.Id}, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AtualizarProdutoDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
