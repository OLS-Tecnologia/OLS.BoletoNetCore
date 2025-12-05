using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using Newtonsoft.Json;


namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response
{
    public record IncluirBoletoSicoobResponseDto
    {
        [property: JsonProperty("resultado")]
       public Resultado Resultado;
    }
    public record BeneficiarioFinal(
       [property: JsonProperty("numeroCpfCnpj")] string NumeroCpfCnpj,
       [property: JsonProperty("nome")] string Nome
   );   

   
    public record Resultado(
       [property: JsonProperty("numeroCliente")] int NumeroCliente,
       [property: JsonProperty("codigoModalidade")] int CodigoModalidade,
       [property: JsonProperty("numeroContaCorrente")] int NumeroContaCorrente,
       [property: JsonProperty("codigoEspecieDocumento")] string CodigoEspecieDocumento,
       [property: JsonProperty("dataEmissao")] string DataEmissao,
       [property: JsonProperty("nossoNumero")] int NossoNumero,
       [property: JsonProperty("seuNumero")] string SeuNumero,
       [property: JsonProperty("identificacaoBoletoEmpresa")] string IdentificacaoBoletoEmpresa,
       [property: JsonProperty("codigoBarras")] string CodigoBarras,
       [property: JsonProperty("linhaDigitavel")] string LinhaDigitavel,
       [property: JsonProperty("identificacaoEmissaoBoleto")] int IdentificacaoEmissaoBoleto,
       [property: JsonProperty("identificacaoDistribuicaoBoleto")] int IdentificacaoDistribuicaoBoleto,
       [property: JsonProperty("valor")] double Valor,
       [property: JsonProperty("dataVencimento")] string DataVencimento,
       [property: JsonProperty("dataLimitePagamento")] string DataLimitePagamento,
       [property: JsonProperty("valorAbatimento")] int ValorAbatimento,
       [property: JsonProperty("tipoDesconto")] int TipoDesconto,
       [property: JsonProperty("dataPrimeiroDesconto")] string DataPrimeiroDesconto,
       [property: JsonProperty("valorPrimeiroDesconto")] int ValorPrimeiroDesconto,
       [property: JsonProperty("dataSegundoDesconto")] string DataSegundoDesconto,
       [property: JsonProperty("valorSegundoDesconto")] int ValorSegundoDesconto,
       [property: JsonProperty("dataTerceiroDesconto")] string DataTerceiroDesconto,
       [property: JsonProperty("valorTerceiroDesconto")] int ValorTerceiroDesconto,
       [property: JsonProperty("tipoMulta")] int TipoMulta,
       [property: JsonProperty("dataMulta")] string DataMulta,
       [property: JsonProperty("valorMulta")] int ValorMulta,
       [property: JsonProperty("tipoJurosMora")] int TipoJurosMora,
       [property: JsonProperty("dataJurosMora")] string DataJurosMora,
       [property: JsonProperty("valorJurosMora")] int ValorJurosMora,
       [property: JsonProperty("numeroParcela")] int NumeroParcela,
       [property: JsonProperty("aceite")] bool Aceite,
       [property: JsonProperty("codigoNegativacao")] int CodigoNegativacao,
       [property: JsonProperty("numeroDiasNegativacao")] int NumeroDiasNegativacao,
       [property: JsonProperty("codigoProtesto")] int CodigoProtesto,
       [property: JsonProperty("numeroDiasProtesto")] int NumeroDiasProtesto,
       [property: JsonProperty("quantidadeDiasFloat")] int QuantidadeDiasFloat,
       [property: JsonProperty("pagador")] Pagador Pagador,
       [property: JsonProperty("beneficiarioFinal")] BeneficiarioFinal BeneficiarioFinal,
       [property: JsonProperty("mensagensInstrucao")] IReadOnlyList<string> MensagensInstrucao,
       [property: JsonProperty("rateioCreditos")] IReadOnlyList<RateioCredito> RateioCreditos,
       [property: JsonProperty("pdfBoleto")] string PdfBoleto,
       [property: JsonProperty("qrCode")] string QrCode,
       [property: JsonProperty("numeroContratoCobranca")] int NumeroContratoCobranca,
       [property: JsonProperty("descricaoRejeicaoPix")] string DescricaoRejeicaoPix
    );





}
