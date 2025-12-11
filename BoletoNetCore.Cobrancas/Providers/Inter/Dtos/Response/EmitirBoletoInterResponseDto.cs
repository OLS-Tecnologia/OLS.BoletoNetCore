using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response
{
    public class EmitirBoletoInterResponseDto : InterBaseResponseDto
    {
        [JsonPropertyName("codigoSolicitacao")]
        public string CodigoSolicitacao { get; set; } //   "codigoSolicitacao": "183e982a-34e5-4bc0-9643-def5432a"
    }
}
