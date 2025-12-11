using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Utills
{
    /// <summary>
    /// Token utilizado na  propriedade statica para guardar o token de acesso com data de expiração
    /// </summary>
    internal class TokenAcesso
    {
        public string? access_token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
