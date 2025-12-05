using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Enums;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request
{
    public class SegundaViaBoletosSicoobRequestDto
    {
        [property: JsonProperty("client_id")]
        [Required]
        public string ClienteId { get; set; } // passar no header, demais parametros via query
        [property: JsonProperty("numeroCliente")]
        [Required]
        public string NumeroCliente { get; set; } 

        [property: JsonProperty("codigoModalidade")]
        [Required]
        public ModalidadeBoleto CodigoModalidade { get; set; }

        [property: JsonProperty("nossoNumero")] 
         public int? NossoNumero {  get; set; }

        [property: JsonProperty("linhaDigitavel")]
        public string? LinhaDigitavel { get; set; }

        [property: JsonProperty("codigoBarras")]
        public string? CodigoBarras { get; set; }

        [property: JsonProperty("gerarPdf")]
        public bool? GerarPdf { get; set; }

        [property: JsonProperty("numeroContratoCobranca")]
        public int? NumeroContratoCobranca { get; set; }





    }
   
}
