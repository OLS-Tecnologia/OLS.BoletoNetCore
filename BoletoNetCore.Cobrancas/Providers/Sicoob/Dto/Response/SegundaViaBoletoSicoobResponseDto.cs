using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response
{
    public record SegundaViaBoletoSicoobResponseDto
    (
        [property: JsonProperty("resultado")]
        ResultadoSegundaVia Resultado
    );    


    public record ResultadoSegundaVia(
        [property: JsonProperty("numeroCliente")] int NumeroCliente,
        [property: JsonProperty("codigoModalidade")] int CodigoModalidade,
        [property: JsonProperty("codigoEspecieDocumento")] string CodigoEspecieDocumento,
        [property: JsonProperty("dataEmissao")] string DataEmissao,
        [property: JsonProperty("nossoNumero")] int NossoNumero,
        [property: JsonProperty("seuNumero")] string SeuNumero,
        [property: JsonProperty("codigoBarras")] string CodigoBarras,
        [property: JsonProperty("linhaDigitavel")] string LinhaDigitavel,
        [property: JsonProperty("valor")] double Valor,
        [property: JsonProperty("dataVencimento")] string DataVencimento,
        [property: JsonProperty("valorAbatimento")] int ValorAbatimento,
        [property: JsonProperty("numeroParcela")] int NumeroParcela,
        [property: JsonProperty("aceite")] bool Aceite,
        [property: JsonProperty("tipoMulta")] int TipoMulta,
        [property: JsonProperty("valorMulta")] double ValorMulta,
        [property: JsonProperty("tipoJurosMora")] int TipoJurosMora,
        [property: JsonProperty("valorJurosMora")] int ValorJurosMora,
        [property: JsonProperty("pagador")] Pagador Pagador,
        [property: JsonProperty("beneficiarioFinal")] BeneficiarioFinal BeneficiarioFinal,
        [property: JsonProperty("mensagensInstrucao")] IReadOnlyList<string> MensagensInstrucao,
        [property: JsonProperty("pdfBoleto")] string PdfBoleto,
        [property: JsonProperty("qrCode")] string QrCode,
        [property: JsonProperty("numeroContratoCobranca")] int NumeroContratoCobranca
    );

   

}
