using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request
{
    public class BoletosPagadorSicoobRequestDto
    {
        [property: JsonPropertyName("client_id")]
        [Required]
        public string ClienteId { get; set; } // passar no header

        [property: JsonPropertyName("numeroCpfCnpj")]
        [Required]
        public string NumeroCpfCnpj { get; set; } // path  /pagadores/{numeroCpf}/boletos

        [property: JsonPropertyName("numeroCliente")]
        [Required]
        public string NumeroCliente { get; set; } // query

        [property: JsonPropertyName("codigoSituacao")]

        public string? CodigoSituacao { get; set; } //query

        [property: JsonPropertyName("dataInicio")]

        public DateTime? DataInicio { get; set; } // query

        [property: JsonPropertyName("dataFim")]

        public DateTime? DataFim { get; set; } // query


    }
}
