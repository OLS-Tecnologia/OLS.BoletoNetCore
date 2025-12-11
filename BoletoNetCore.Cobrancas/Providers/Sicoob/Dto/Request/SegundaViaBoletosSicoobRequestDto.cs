using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Enums;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request
{
    public class SegundaViaBoletosSicoobRequestDto
    {
        [property: JsonPropertyName("client_id")]
        [Required]
        public string ClienteId { get; set; } // passar no header, demais parametros via query
        [property: JsonPropertyName("numeroCliente")]
        [Required]
        public string NumeroCliente { get; set; } 

        [property: JsonPropertyName("codigoModalidade")]
        [Required]
        public ModalidadeBoleto CodigoModalidade { get; set; }

        [property: JsonPropertyName("nossoNumero")] 
         public int? NossoNumero {  get; set; }

        [property: JsonPropertyName("linhaDigitavel")]
        public string? LinhaDigitavel { get; set; }

        [property: JsonPropertyName("codigoBarras")]
        public string? CodigoBarras { get; set; }

        [property: JsonPropertyName("gerarPdf")]
        public bool? GerarPdf { get; set; }

        [property: JsonPropertyName("numeroContratoCobranca")]
        public int? NumeroContratoCobranca { get; set; }





    }
   
}
