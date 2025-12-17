using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Base
{
    public class BeneficiarioFinalSicoob
    {
        [JsonPropertyName("numeroCpfCnpj")] 
        public string NumeroCpfCnpj { get;  set; }

        [JsonPropertyName("nome")]
        [Required]
        public string Nome { get; set; }
       
    }
}
