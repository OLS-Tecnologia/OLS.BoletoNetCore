using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request
{
    public class CancelamentoBoletoInterRequestDto : InterBaseRequestDto, RequestBase
    {     
        /// <summary>
        ///     Conta corrente que será utilizada na operação, caso faça parte da lista de contas correntes da aplicação.
        ///     Enviar apenas números(incluindo o dígito), e não enviar zeros a esquerda.
        /// </summary>
        public string? XContaCorrente { get; set; }// header parameter

        [Required(ErrorMessage = "CodigoSolicitacao é obrigatório")]
        public string CodigoSolicitacao { get; set; }// path parameter
       
        [Required(ErrorMessage = "Necessário informar o corpo da requisição: RequestDto")]
        public CancelarBoetoBody RequestDto {  get; set; }


        public bool IsValid()
        {
            OLS.LibCore.Validate.ValidationResult validationResult = new();

            if (CodigoSolicitacao is null)
            {
                validationResult.AddMensagem("Codigo Socilitação é obrigatório.");
            }

            if (RequestDto.MotivoCancelamento is null)
            {
                validationResult.AddMensagem("Motivo do cancelamento deve ser infomado.");

            }


            if (!validationResult.IsValid)
            {
                Console.WriteLine(" Erros na validação do IncluirBoletoSicoobRequestDto");
                throw new Exception(validationResult.Message);
            }

            return validationResult.IsValid;
        }
    }

    public class CancelarBoetoBody(string motivoCancelamento)
    {
        [JsonPropertyName("motivoCancelamento")]
        [Required]
        public string MotivoCancelamento { get; set; } = motivoCancelamento;
    }
}
