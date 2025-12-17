using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Base;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response
{
    public class ConsultarBoletoResponseDto : ResponseBase
    {
       public Response ResultadoRequest { get; set; }
    }

    public class Response {
        [JsonPropertyName("resultado")]
        public ResultadoConsultaBoletoSicoob Resultado { get; set; }

    }

    public class ListaHistorico
    {
        [JsonPropertyName("dataHistorico")]
        public string DataHistorico { get; set; }

        [JsonPropertyName("tipoHistorico")]
        public string TipoHistorico { get; set; }

        [JsonPropertyName("descricaoHistorico")]
        public string DescricaoHistorico { get; set; }
    }    

    public class ResultadoConsultaBoletoSicoob : ResultadoBaseBoletoSicoob
    {
        [JsonPropertyName("listaHistorico")]
        public IReadOnlyList<ListaHistorico>? ListaHistorico { get; set; }

        [JsonPropertyName("pagador")]
        public PagadorSicoob Pagador { get; set; }

        [JsonPropertyName("beneficiarioFinal")]
        public BeneficiarioFinalSicoob? BeneficiarioFinal { get; set; }

        [JsonPropertyName("rateioCreditos")]
        public IReadOnlyList<RateioCredito> RateioCreditos { get; set; }

        [JsonPropertyName("situacaoBoleto")]
        public string SituacaoBoleto { get; set; }


    }

    
}
