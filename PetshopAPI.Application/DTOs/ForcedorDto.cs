namespace PetshopAPI.Application.DTOs;

public class CriarFornecedorDto
{
    public string Nome {get; set;} = string.Empty;
    public string Contato {get; set;} = string.Empty;
}

public class FornecedorResponseDto
{
    public Guid Id{get; set;}
    public string Nome {get; set; }= string.Empty;
    public string Contato{get; set;} = string.Empty;
}