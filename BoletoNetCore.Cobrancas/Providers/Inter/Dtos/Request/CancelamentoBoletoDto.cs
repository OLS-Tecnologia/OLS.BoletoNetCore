using BoletoNetCore.Cobrancas.Providers.BaseProvider.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request
{
    public class CancelamentoBoletoInterDto : IRequestDto
    {      

        [JsonProperty("motivoCancelamento")]
        [Required]
        public string MotivoCancelamento { get; set; }
    }
}
