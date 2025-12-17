using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Base
{
    public class PagadorSicoob : PagadorBase
    {
        [JsonPropertyName("numeroCpfCnpj")]
        public string NumeroCpfCnpj { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }  

       
    }
}
