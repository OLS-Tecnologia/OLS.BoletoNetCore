using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response
{
    public class SegundaViaBoletoSicoobResponseDto : ResponseBase
    {
        [JsonPropertyName("resultado")]
        public ResultadoSegundaViaSicoob Resultado {  get; set; }
    }

    public class ResultadoSegundaViaSicoob
    {
        [JsonPropertyName("numeroCliente")] public int NumeroCliente { get; set; }
        [JsonPropertyName("codigoModalidade")] public int CodigoModalidade { get; set; }
        [JsonPropertyName("codigoEspecieDocumento")] public string CodigoEspecieDocumento { get; set; }
        [JsonPropertyName("dataEmissao")] public string DataEmissao { get; set; }
        [JsonPropertyName("nossoNumero")] public int NossoNumero { get; set; }
        [JsonPropertyName("seuNumero")] public string SeuNumero { get; set; }
        [JsonPropertyName("codigoBarras")] public string CodigoBarras { get; set; }
        [JsonPropertyName("linhaDigitavel")] public string LinhaDigitavel { get; set; }
        [JsonPropertyName("valor")] public double Valor { get; set; }
        [JsonPropertyName("dataVencimento")] public string DataVencimento { get; set; }
        [JsonPropertyName("valorAbatimento")] public int ValorAbatimento { get; set; }
        [JsonPropertyName("numeroParcela")] public int NumeroParcela { get; set; }
        [JsonPropertyName("aceite")] public bool Aceite { get; set; }
        [JsonPropertyName("tipoMulta")] public int TipoMulta { get; set; }
        [JsonPropertyName("valorMulta")] public double ValorMulta { get; set; }
        [JsonPropertyName("tipoJurosMora")] public int TipoJurosMora { get; set; }
        [JsonPropertyName("valorJurosMora")] public int ValorJurosMora { get; set; }
        [JsonPropertyName("pagador")] public PagadorSicoob Pagador { get; set; }
        [JsonPropertyName("beneficiarioFinal")] public BeneficiarioFinalSicoob BeneficiarioFinal { get; set; }
        [JsonPropertyName("mensagensInstrucao")] public IReadOnlyList<string> MensagensInstrucao { get; set; }
        [JsonPropertyName("pdfBoleto")] public string PdfBoleto { get; set; }
        [JsonPropertyName("qrCode")] public string QrCode { get; set; }
        [JsonPropertyName("numeroContratoCobranca")] public int NumeroContratoCobranca { get; set; }
        [JsonPropertyName("tipoDesconto")] public int TipoDesconto { get; set; }
    }   

}
