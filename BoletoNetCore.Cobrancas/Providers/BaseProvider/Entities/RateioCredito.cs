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
    public class RateioCredito
    {

        [JsonPropertyName("numeroBanco")]
        [Required]
        public int NumeroBanco { get; set; }

        [JsonPropertyName("numeroAgencia")]
        [Required]
        public int NumeroAgencia { get; set; }

        [JsonPropertyName("numeroContaCorrente")]
        [Required]
        public int NumeroContaCorrente { get; set; }

        [JsonPropertyName("contaPrincipal")]
        [Required]
        public bool ContaPrincipal { get; set; }

        [JsonPropertyName("codigoTipoValorRateio")]
        [Required]
        public int CodigoTipoValorRateio { get; set; }

        [JsonPropertyName("valorRateio")]
        [Required]
        public int ValorRateio { get; set; }

        [JsonPropertyName("codigoTipoCalculoRateio")]
        [Required]
        public int CodigoTipoCalculoRateio { get; set; }

        [JsonPropertyName("numeroCpfCnpjTitular")]
        [Required]
        public string NumeroCpfCnpjTitular { get; set; }

        [JsonPropertyName("nomeTitular")]
        [Required]
        public string NomeTitular { get; set; }

        [JsonPropertyName("codigoFinalidadeTed")]
        [Required]
        public int CodigoFinalidadeTed { get; set; }

        [JsonPropertyName("codigoTipoContaDestinoTed")]
        [Required]
        public string CodigoTipoContaDestinoTed { get; set; }

        [JsonPropertyName("quantidadeDiasFloat")]
        [Required]
        public int QuantidadeDiasFloat { get; set; }

        [JsonPropertyName("dataFloatCredito")]
        [Required]
        public string DataFloatCredito { get; set; }

    }
}
