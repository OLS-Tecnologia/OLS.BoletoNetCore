using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Entities
{
    public class PagadorInter: PagadorBase
    {
        public PagadorInter(string cpfCnpj, TipoPessoa tipoPessoa, string nome, string cep, UfBrasil uf, string cidade, string endereco, string bairro,
            string? numero = null, string? ddd = null, string? telefone = null, string? complemento = null, string? email = null)
            : base(nome, cep, uf, cidade, endereco, email)
        {

            CpfCnpj = cpfCnpj;
            Numero = numero;
            Ddd = ddd;
            Telefone = telefone;
            Complemento = complemento;
            TipoPessoa = Enum.GetName<TipoPessoa>(tipoPessoa) ?? "";
            Bairro = bairro;
        }

        public PagadorInter()  { }
        
        [JsonPropertyName("cpfCnpj")]
        [Required]
        [StringLength(17, MinimumLength = 11)]
        public virtual string CpfCnpj { get; set; }

        [JsonPropertyName("numero")]
        [Required]
        public string? Numero { get; set; }

        [JsonPropertyName("ddd")]       
        public string? Ddd { get; set; } 

        [JsonPropertyName("telefone")]       
        public string? Telefone { get; set; }  

        [JsonPropertyName("complemento")]
        public string? Complemento { get; set; }

        [JsonPropertyName("tipoPessoa")]
        [Required]
        public string TipoPessoa { get; set; }


        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

    }
}
