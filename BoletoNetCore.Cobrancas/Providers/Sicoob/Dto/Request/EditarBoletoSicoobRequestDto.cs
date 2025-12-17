using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request
{
    public class EditarBoletoSicoobRequestDto : RequestBase
    {
        /// <summary>
        /// Numero que identifica o boleto de cobranca no Sisbr
        /// </summary>
        [JsonPropertyName("nossoNumero")]
        [Required]
        public int NossoNumero {  get; set; }


        [JsonPropertyName("boleto")]
        [Required]
        public EditarBoletoSicoobRequestBody Boleto {  get; set; }

        [JsonPropertyName("client_id")]
        [Required]
        public string ClientId { get; set; }


        public bool IsValid()
        {
            OLS.LibCore.Validate.ValidationResult validationResult = new();

            if (!Enum.IsDefined(typeof(ModalidadeBoletoSicoob), Boleto.CodigoModalidade))
            {
                string options = string.Join(", ", Enum.GetValues<ModalidadeBoletoSicoob>());

                validationResult.AddMensagem($"Valor Inválido para modalidade do boleto. Valores aceitos: {options}");

            }

            if (!validationResult.IsValid)
            {
                Console.WriteLine(" Erros na validação do IncluirBoletoSicoobRequestDto");
                throw new Exception(validationResult.Message);
            }

            return validationResult.IsValid;
        }

    }

    public class EditarBoletoSicoobRequestBody
    {
        [JsonPropertyName("numeroCliente")]
        [Required]
        public int NumeroCliente { get; set; }

        [JsonPropertyName("codigoModalidade")]
        [Required]
        public int CodigoModalidade { get; set; } // ModalidadeBoletoSicoob

        [JsonPropertyName("valorNominal")]
          [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AlterarValorNominalBody? ValorNominal { get; set; }

        [JsonPropertyName("prorrogacaoVencimento")]
          [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProrrogacaoVencimentoSicoobBody? ProrrogacaoVencimento { get; set; }        

       
    }

    public class AlterarValorNominalBody
    {
        [JsonPropertyName("valor")]
        [Required]
        public double Valor { get; set; }      
    }

    public class ProrrogacaoVencimentoSicoobBody
    {
        [JsonPropertyName("dataVencimento")]
        [Required]
        public DateOnly DataVencimento { get; set; }
       
    }
}
