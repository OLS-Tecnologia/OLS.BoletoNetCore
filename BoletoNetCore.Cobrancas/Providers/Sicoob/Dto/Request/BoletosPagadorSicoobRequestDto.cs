using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request
{
    public class BoletosPagadorSicoobRequestDto
    {
        [property: JsonProperty("client_id")]
        [Required]
        public string ClienteId { get; set; } // passar no header

        [property: JsonProperty("numeroCpfCnpj")]
        [Required]
        public string NumeroCpfCnpj { get; set; } // path  /pagadores/{numeroCpf}/boletos

        [property: JsonProperty("numeroCliente")]
        [Required]
        public string NumeroCliente { get; set; } // query

        [property: JsonProperty("codigoSituacao")]

        public string? CodigoSituacao { get; set; } //query

        [property: JsonProperty("dataInicio")]

        public DateTime? DataInicio { get; set; } // query

        [property: JsonProperty("dataFim")]

        public DateTime? DataFim { get; set; } // query


    }
}
