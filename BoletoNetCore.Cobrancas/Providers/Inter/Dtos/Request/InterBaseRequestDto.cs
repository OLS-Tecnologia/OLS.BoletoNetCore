using System.ComponentModel.DataAnnotations;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request
{
    public class InterBaseRequestDto 
    {

        [Required(ErrorMessage = "ClientId é obrigatório")]
        public string ClientId { get; set; }

        [Required(ErrorMessage = "ClientSecret é obrigatório")]
        public string ClientSecret { get; set; }
       
        public string? ArquivoCertificado { get; set; }
        public string? ArquivoChave { get; set; }
       
       
    }
}
