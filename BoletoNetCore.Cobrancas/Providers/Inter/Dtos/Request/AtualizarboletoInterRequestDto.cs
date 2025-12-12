using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
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

        public AtualizarboletoInterRequestDto(string codigoSolicitacao, string xContaCorrente, AtualizarBoletoBody requestDto, 
            string clientId, string clientSecret, string arquivoCertificado, string arquivoChave) : base(clientId, clientSecret, arquivoCertificado, arquivoChave)
        {
            CodigoSolicitacao = codigoSolicitacao;
            XContaCorrente = xContaCorrente;
            RequestDto = requestDto;
        }
    }

    public class AtualizarBoletoBody
    {

        [JsonPropertyName("dataVencimento")]
        public DateOnly? DataVencimento { get; }

        [JsonPropertyName("valorNominal")]
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
