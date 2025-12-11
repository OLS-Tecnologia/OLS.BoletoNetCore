using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request
{
    public class AtualizarboletoInterRequestDto : InterBaseRequestDto
    {

        [Required(ErrorMessage = "CodigoSOlicitacao é obrigatório")]
        public string CodigoSolicitacao { get; set; } // path parameter

        [Required(ErrorMessage = "XContaCorrente é obrigatória")]
        public string XContaCorrente { get; set; }// header parameter

        [Required(ErrorMessage = "Necessário informar o corpo da requisição: RequestDto")]
        public AtualizarBoletoBody   RequestDto {  get; set; }

    }

    public class AtualizarBoletoBody
    {

        [JsonPropertyName("dataVencimento")]
        public DateOnly DataVencimento { get; set; }

        [JsonPropertyName("valorNominal")]
        public double ValorNominal { get; set; }
    }
}
