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

        public bool IsValid()
        {
            OLS.LibCore.Validate.ValidationResult validationResult = new();

            if (CodigoSolicitacao is null)
            {
                validationResult.AddMensagem("Codigo Socilitação é obrigatório.");
            }
         

            if (!validationResult.IsValid)
            {
                Console.WriteLine(" Erros na validação do IncluirBoletoSicoobRequestDto");
                throw new Exception(validationResult.Message);
            }

            return validationResult.IsValid;
        }

    }
}
