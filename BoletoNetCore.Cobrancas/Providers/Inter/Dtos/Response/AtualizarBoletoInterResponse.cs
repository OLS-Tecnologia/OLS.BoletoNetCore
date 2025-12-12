using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response
{
    public class AtualizarBoletoInterResponseDto : InterBaseResponseDto, ResponseBase
    {

        [JsonPropertyName("status")]
        public string  Status {  get; set; } //StatusAtualizacaoBoletoInter

        [JsonPropertyName("mensagem")]
        public string Mensagem {  get; set; }

        [JsonPropertyName("codigoEdicao")]
        public string CodigoEdicao {  get; set; }
    }


    public enum StatusAtualizacaoBoletoInter
    {
        PROCESSANDO = 1,
        SUCESSO = 2,
        FALHA = 3
    }
}
