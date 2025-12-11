using Newtonsoft.Json;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response
{
    public class AtualizarBoletoInterResponseDto : InterBaseResponseDto
    {

        [JsonProperty("status")]
        public StatusAtualizacaoBoletoInter Status {  get; set; }

        [JsonProperty("mensagem")]
        public string Mensagem {  get; set; }

        [JsonProperty("codigoEdicao")]
        public string CodigoEdicao {  get; set; }
    }


    public enum StatusAtualizacaoBoletoInter
    {
        PROCESSANDO = 1,
        SUCESSO = 2,
        FALHA = 3
    }
}
