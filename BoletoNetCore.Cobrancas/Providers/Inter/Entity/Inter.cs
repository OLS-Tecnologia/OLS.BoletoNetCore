using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Entity
{
    public class Inter : BaseProviderEntity
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ArquivoCertificado { get; set; }
        public string ArquivoChave { get; set; }

        public IRequestDto reqDto { get; set; } // Dto da requisição a ser enviada
    }
}
