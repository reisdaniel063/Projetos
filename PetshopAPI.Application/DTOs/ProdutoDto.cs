namespace PetshopAPI.Application.DTOs;

public class CriarProdutoDto
{
    public string Nome {get; set;} = string.Empty;
    public string Unidade {get; set;}=string.Empty;
    public decimal EstoqueAtual {get; set;}
    public decimal EstoqueMinimo {get; set;}
    public Guid FornecedorId{get; set;}

    
}
public class AtualizarProdutoDto
    {
        public string Nome {get; set;} = string.Empty;
        public string Unidade {get; set;} = string.Empty;
        public decimal EstoqueMinimo {get; set;}
    }

    public class ProdutoResponseDto
    {
        public Guid Id { get; set; }
        public string Nome {get; set;} = string.Empty;
        public string Unidade { get; set;} = string.Empty;
        public decimal EstoqueAtual {get;set;}
        public decimal EstoqueMinimo {get; set;}
        public string NomeFornecedor {get; set;} = string.Empty;
    }