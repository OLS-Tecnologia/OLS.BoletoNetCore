using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response
{
    public class EmitirBoletoInterResponseDto : InterBaseResponseDto, ResponseBase
    {
        [JsonPropertyName("codigoSolicitacao")]
        public string CodigoSolicitacao { get; set; }
    }
}
