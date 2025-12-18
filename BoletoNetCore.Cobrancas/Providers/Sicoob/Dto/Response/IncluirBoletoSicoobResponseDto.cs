using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Base;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request;
using Newtonsoft.Json;
using System.Text.Json.Serialization;


namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response
{
    public class IncluirBoletoSicoobResponseDto : ResponseBase
    {
        [JsonPropertyName("resultado")]
        public ResultadoEmissaoBoletoSicoob Resultado { get; set; }
    }  


    public class ResultadoEmissaoBoletoSicoob : ResultadoBaseBoletoSicoob
    {

        [property: JsonPropertyName("pdfBoleto")]
        public string PdfBoleto { get; set; }

        [property: JsonPropertyName("descricaoRejeicaoPix")]
        public string DescricaoRejeicaoPix {  get; set; }

        [JsonPropertyName("codigoProtesto")]
        public int CodigoProtesto { get; set; }


        [JsonPropertyName("codigoNegativacao")]
        public int CodigoNegativacao { get; set; }

        [JsonPropertyName("pagador")]
        public PagadorSicoob Pagador { get; set; }

        [JsonPropertyName("beneficiarioFinal")]
        public BeneficiarioFinalSicoob BeneficiarioFinal { get; set; }

        [JsonPropertyName("rateioCreditos")]
        public IReadOnlyList<RateioCredito> RateioCreditos { get; set; }


    };

   

}
