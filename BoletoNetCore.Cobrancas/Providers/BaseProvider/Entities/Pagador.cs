using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities
{
    public class PagadorBase 
    {

        [JsonPropertyName("nome")]
        [Required]
        public string Nome { get; set; }

        [JsonPropertyName("email")]       
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
       

        [JsonPropertyName("bairro")]
        public string? Bairro { get; set; }

        public PagadorBase(string nome, string cep, UfBrasil uf, string cidade, string endereco, string? email = null, string? bairro= null)
        {
            Nome = nome;
            Email = email;
            Cep = cep;
            Uf = Enum.GetName<UfBrasil>(uf) ?? "";
            Cidade = cidade;
            Endereco = endereco;
            Bairro = bairro;
        }

        public PagadorBase()
        {
        }
    }
}
