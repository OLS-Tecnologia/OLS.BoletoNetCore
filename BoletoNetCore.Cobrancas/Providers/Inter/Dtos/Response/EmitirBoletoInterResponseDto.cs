using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response
{
    public record EmitirBoletoInterResponseDto
    {
        [JsonProperty("codigoSolicitacao")]
        public string CodigoSolicitacao { get; set; } //   "codigoSolicitacao": "183e982a-34e5-4bc0-9643-def5432a"
    }
}
