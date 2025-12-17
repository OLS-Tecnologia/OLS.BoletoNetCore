using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Enums;
using OLS.LibCore.Validate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request
{
    public class ConsultarBoletoRequestDto : RequestBase
    {
        [JsonPropertyName("clientId")]
        public string ClienteId { get; set; } = string.Empty;


        [JsonPropertyName("body")]
        public ConsultarBoletoSicoobRequestBody Body { get; set; }
      
    }

    public class ConsultarBoletoSicoobRequestBody
    {
        [JsonPropertyName("numeroCliente")]
        [Required]
        public int NumeroCliente { get; set; }

        [property: JsonPropertyName("codigoModalidade")]
        [Required]
        public int CodigoModalidade { get; set; }

        [property: JsonPropertyName("nossoNumero")]
        public int? NossoNumero { get; set; }

        [property: JsonPropertyName("linhaDigitavel")]
        public string? LinhaDigitavel { get; set; }

        [property: JsonPropertyName("codigoBarras")]
        public string? CodigoBarras { get; set; }

        [property: JsonPropertyName("numeroContratoCobranca")]
        public int? NumeroContratoCobranca { get; set; }


        public bool IsValid()
        {
            OLS.LibCore.Validate.ValidationResult validationResult = new();

            if (!Enum.IsDefined(typeof(ModalidadeBoletoSicoob),CodigoModalidade)) {
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

        // ModalidadeBoletoSicoob
    }
}
