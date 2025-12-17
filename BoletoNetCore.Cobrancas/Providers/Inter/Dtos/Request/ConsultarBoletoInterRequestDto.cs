using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using System.ComponentModel.DataAnnotations;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request
{
    public class ConsultarBoletoInterRequestDto : InterBaseRequestDto, RequestBase
    {      
        [Required(ErrorMessage = "CodigoSolicitacao é obrigatório")]
        public string CodigoSolicitacao { get; set; }// path parameter

        [Required(ErrorMessage = "XContaCorrente é obrigatória")]
        public string XContaCorrente { get; set; }// header parameter

    }
}
