using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request
{
    internal class SicoobRequestBase
    {
        [Required(ErrorMessage = "ClientId é obrigatório")]
        public string ClientId { get; set; }       

        public string ArquivoCertificado { get; set; }
        public string ArquivoChave { get; set; }
     
    }
}
