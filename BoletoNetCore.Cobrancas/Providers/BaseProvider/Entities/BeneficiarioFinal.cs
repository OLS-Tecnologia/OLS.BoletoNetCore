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
    public class BeneficiarioFinal
    {
       

        [JsonPropertyName("cpfCnpj")]
        public string CpfCnpj { get; set; }


        [JsonPropertyName("numeroCpfCnpj")] // recebe de CpfCnpj
        public string NumeroCpfCnpj { get; private set; }



        [JsonPropertyName("nome")]
        [Required]
        public string Nome { get; set; }

        [JsonPropertyName("tipoPessoa")]
        [Required]
        public TipoPessoa TipoPessoa { get; set; }

        [JsonPropertyName("cep")]
        [Required]
        public string Cep { get; set; }

        [JsonPropertyName("uf")]
        [Required]
        public UfBrasil Uf { get; set; }

        [JsonPropertyName("cidade")]
        [Required]
        public string Cidade { get; set; }

        [JsonPropertyName("endereco")]
        [Required]
        public string Endereco { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }


    }
}
