using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using PetshopAPI.Application.DTOs;
using PetshopAPI.Application.Services;

namespace PetshopAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdemDeCompraController : ControllerBase
{
    private readonly OrdemDeCompraService _service;
    public OrdemDeCompraController(OrdemDeCompraService service)
    {
        _service = service;
    }
    [HttpPost("gerar")]
    public async Task<IActionResult> Gerar([FromBody] GerarOrdemDeCompraDto dto)
    {
        var ordem = await _service.GerarAsync(dto);
        return Ok(ordem);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var ordem = await _service.GetByIdAsync(id);
        if(ordem == null) return NotFound();
        return Ok(ordem);
    }
}