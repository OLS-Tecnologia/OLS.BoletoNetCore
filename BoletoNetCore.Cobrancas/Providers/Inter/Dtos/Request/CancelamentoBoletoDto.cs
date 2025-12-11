using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request
{
    public class CancelamentoBoletoInterRequestDto : InterBaseRequestDto 
    {

        [Required(ErrorMessage = "CodigoSolicitacao é obrigatório")]
        public string CodigoSolicitacao { get; set; }// path parameter

        [Required(ErrorMessage = "XContaCorrente é obrigatório")]
        public string XContaCorrente { get; set; }// header parameter

        [Required(ErrorMessage = "Necessário informar o corpo da requisição: RequestDto")]
        public CancelarBoetoBody RequestDto {  get; set; }
    }

    public class CancelarBoetoBody
    {
        [JsonProperty("motivoCancelamento")]
        [Required]
        public string MotivoCancelamento { get; set; }

    }
}
