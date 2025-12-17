using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities
{
    public class PagadorBase 
    {

        [JsonPropertyName("nome")]
        [Required]
        public string Nome { get; set; }

        [JsonPropertyName("email")]
           [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Email { get; set; }
       

        [JsonPropertyName("cep")]
        [Required]
        public string Cep { get; set; }

        [JsonPropertyName("uf")]
        [Required]
        public string Uf { get; set; } 

        [JsonPropertyName("cidade")]
        [Required]
        public string Cidade { get; set; }

        [JsonPropertyName("endereco")]
        [Required]
        public string Endereco { get; set; }
       

       

        public PagadorBase(string nome, string cep, UfBrasil uf, string cidade, string endereco, string? email = null)
        {
            Nome = nome;
            Email = email;
            Cep = cep;
            Uf = Enum.GetName<UfBrasil>(uf) ?? "";
            Cidade = cidade;
            Endereco = endereco;
          
        }

        public PagadorBase()
        {
        }
    }
}
