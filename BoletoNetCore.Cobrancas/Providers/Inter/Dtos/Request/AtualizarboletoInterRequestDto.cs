using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request
{
    public class AtualizarboletoInterRequestDto : InterBaseRequestDto, RequestBase
    {

        [Required(ErrorMessage = "CodigoSolicitacao é obrigatório")]
        public string CodigoSolicitacao { get; set; } // path parameter

        [Required(ErrorMessage = "XContaCorrente é obrigatória")]
        public string XContaCorrente { get; set; }// header parameter

        [Required(ErrorMessage = "Necessário informar o corpo da requisição: RequestDto")]
        public AtualizarBoletoBody   RequestDto {  get; set; }

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

    public class AtualizarBoletoBody
    {

        [JsonPropertyName("dataVencimento")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateOnly? DataVencimento { get; }

        [JsonPropertyName("valorNominal")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? ValorNominal { get; set; }

        public AtualizarBoletoBody(DateOnly dataVencimento, double valorNominal)
        {
            DataVencimento = dataVencimento;
            ValorNominal = valorNominal;
        }

        public AtualizarBoletoBody(DateOnly dataVencimento)
        {
            DataVencimento = dataVencimento;         
        }

        public AtualizarBoletoBody(double valorNominal)
        {         
            ValorNominal = valorNominal;
        }



    }
}
