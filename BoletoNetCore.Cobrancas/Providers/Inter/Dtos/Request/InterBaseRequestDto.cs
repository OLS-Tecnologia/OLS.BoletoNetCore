using System.ComponentModel.DataAnnotations;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request
{
    public class InterBaseRequestDto 
    {

        [Required(ErrorMessage = "ClientId é obrigatório")]
        public string ClientId { get; set; }      

        [Required(ErrorMessage = "ClientSecret é obrigatório")]
        public string ClientSecret { get; set; }
       
        public string ArquivoCertificado { get; set; }
        public string ArquivoChave { get; set; }


        public InterBaseRequestDto(string clientId, string clientSecret, string arquivoCertificado, string arquivoChave)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            ArquivoCertificado = arquivoCertificado;
            ArquivoChave = arquivoChave;
        }

    }
}
