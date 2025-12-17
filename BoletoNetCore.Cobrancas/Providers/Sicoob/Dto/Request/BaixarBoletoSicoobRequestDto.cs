using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request
{
    public class BaixarBoletoSicoobRequestDto : RequestBase
    {        
        [JsonPropertyName("nossoNumero")]
        public string NossoNumero {  get; set; }

        [JsonPropertyName("boleto")]
        public BaixarBoletoRequestBody Boleto { get; set; }

        [JsonPropertyName("clientId")]
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
                throw new Exception(validationResult.Message);
            }


            return validationResult.IsValid;
        }
       
    }

    public class BaixarBoletoRequestBody
    {
        [JsonPropertyName("numeroCliente")]
        [Required]
        public int NumeroCliente { get; set; }

        [JsonPropertyName("codigoModalidade")]
        [Required]
        public int CodigoModalidade { get; set; } // Int  ModalidadeBoletoSicoob
      
    }
}
