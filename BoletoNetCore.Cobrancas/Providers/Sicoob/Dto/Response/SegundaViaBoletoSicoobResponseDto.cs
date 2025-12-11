using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response
{
    public record SegundaViaBoletoSicoobResponseDto
    (
        [property: JsonPropertyName("resultado")]
        ResultadoSegundaVia Resultado
    );    


    public record ResultadoSegundaVia(
        [property: JsonPropertyName("numeroCliente")] int NumeroCliente,
        [property: JsonPropertyName("codigoModalidade")] int CodigoModalidade,
        [property: JsonPropertyName("codigoEspecieDocumento")] string CodigoEspecieDocumento,
        [property: JsonPropertyName("dataEmissao")] string DataEmissao,
        [property: JsonPropertyName("nossoNumero")] int NossoNumero,
        [property: JsonPropertyName("seuNumero")] string SeuNumero,
        [property: JsonPropertyName("codigoBarras")] string CodigoBarras,
        [property: JsonPropertyName("linhaDigitavel")] string LinhaDigitavel,
        [property: JsonPropertyName("valor")] double Valor,
        [property: JsonPropertyName("dataVencimento")] string DataVencimento,
        [property: JsonPropertyName("valorAbatimento")] int ValorAbatimento,
        [property: JsonPropertyName("numeroParcela")] int NumeroParcela,
        [property: JsonPropertyName("aceite")] bool Aceite,
        [property: JsonPropertyName("tipoMulta")] int TipoMulta,
        [property: JsonPropertyName("valorMulta")] double ValorMulta,
        [property: JsonPropertyName("tipoJurosMora")] int TipoJurosMora,
        [property: JsonPropertyName("valorJurosMora")] int ValorJurosMora,
        [property: JsonPropertyName("pagador")] Pagador Pagador,
        [property: JsonPropertyName("beneficiarioFinal")] BeneficiarioFinal BeneficiarioFinal,
        [property: JsonPropertyName("mensagensInstrucao")] IReadOnlyList<string> MensagensInstrucao,
        [property: JsonPropertyName("pdfBoleto")] string PdfBoleto,
        [property: JsonPropertyName("qrCode")] string QrCode,
        [property: JsonPropertyName("numeroContratoCobranca")] int NumeroContratoCobranca
    );

   

}
