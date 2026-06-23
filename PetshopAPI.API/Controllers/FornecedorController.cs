using Microsoft.AspNetCore.Mvc;
using PetshopAPI.Application.DTOs;
using PetshopAPI.Application.Services;

namespace PetshopAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FornecedorController : ControllerBase
{
    private readonly FornecedorService _service;

    public FornecedorController(FornecedorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var fornecedores = await _service.GetAllAsync();
        return Ok(fornecedores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var fornecedor = await _service.GetByIdAsync(id);
        if (fornecedor == null) return NotFound();
        return Ok(fornecedor);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CriarFornecedorDto dto)
    {
        var fornecedor = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = fornecedor.Id }, fornecedor);
    }
}