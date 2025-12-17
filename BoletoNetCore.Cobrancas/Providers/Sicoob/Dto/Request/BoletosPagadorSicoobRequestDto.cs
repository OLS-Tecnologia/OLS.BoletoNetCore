using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request
{
    public class BoletosPagadorSicoobRequestDto : RequestBase
    {       

        [property: JsonPropertyName("numeroCpfCnpj")]
        [Required]
        public string NumeroCpfCnpj { get; set; } // path  /pagadores/{numeroCpf}/boletos


        [property: JsonPropertyName("numeroCliente")]
        [Required]
        public int NumeroCliente { get; set; } // query      
      

        [property: JsonPropertyName("codigoSituacao")]

        public int? CodigoSituacao { get; set; } //query  SituacaoBoletoSicoobEnum

        [property: JsonPropertyName("dataInicio")]

        public DateTime? DataInicio { get; set; } // query

        [property: JsonPropertyName("dataFim")]

        public DateTime? DataFim { get; set; } // query

        public bool IsValid()
        {

            OLS.LibCore.Validate.ValidationResult validationResult = new();

            if (CodigoSituacao is not null)
            {
                if (!Enum.IsDefined(typeof(SituacaoBoletoSicoobEnum), CodigoSituacao))
                {
                    string options = string.Join(", ", Enum.GetValues<SituacaoBoletoSicoobEnum>());

                    validationResult.AddMensagem($"Valor Inválido para codigo da situação do boleto. Valores aceitos: {options}");

                }

            }

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.Message);
            }


            return validationResult.IsValid;
        }
    }
}
