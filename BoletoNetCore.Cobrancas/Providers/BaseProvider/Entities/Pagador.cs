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
        //public PagadorBase()
        //{
        //    NumeroCpfCnpj = base.CpfCnpj;
        //}

        //[JsonPropertyName("ddd")]
        //public string Ddd { get; set; }

        //[JsonPropertyName("telefone")]
        //public string Telefone { get; set; }

        //[JsonPropertyName("numeroCpfCnpj")] // recebe do campo CpfCnpj da genérica
        //[Required]
        //public string NumeroCpfCnpj { get; private set; }

        [JsonPropertyName("cpfCnpj")]
        public string CpfCnpj { get; set; }

        //[JsonPropertyName("numeroCpfCnpj")] // recebe de CpfCnpj
        //public string NumeroCpfCnpj { get; private set; }


        [JsonPropertyName("nome")]
        [Required]
        public string Nome { get; set; }

        [JsonPropertyName("email")]
        [Required]
        public string Email { get; set; }

        [JsonPropertyName("tipoPessoa")] 
        [Required]
        public string TipoPessoa { get; set; } //Enum TipoPessoa

        [JsonPropertyName("cep")]
        [Required]
        public string Cep { get; set; }

        [JsonPropertyName("uf")]
        [Required]
        public string Uf { get; set; } //UfBrasil enum

        [JsonPropertyName("cidade")]
        [Required]
        public string Cidade { get; set; }

        [JsonPropertyName("endereco")]
        [Required]
        public string Endereco { get; set; }

        [JsonPropertyName("numero")]
        [Required]
        public string Numero { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("ddd")]
        public string Ddd { get; set; }

        [JsonPropertyName("telefone")]
        public string Telefone { get; set; }


        [JsonPropertyName("complemento")]
        public string Complemento { get; set; }


    }
}
