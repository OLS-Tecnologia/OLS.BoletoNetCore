using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response
{
    public  record BoletosPagadorSicoobResponseDto
    (
        [property: JsonPropertyName("resultado")]
        IReadOnlyList<PagadorBoletosResultado> Resultado
    );

    public record BeneficiarioFinalBoletos(
      [property: JsonPropertyName("nome")] string Nome
  );

    public record PagadorBoletos(
        [property: JsonPropertyName("numeroCpfCnpj")] string NumeroCpfCnpj,
        [property: JsonPropertyName("nome")] string Nome
    );

    public record PagadorBoletosResultado(
        [property: JsonPropertyName("numeroCliente")] int NumeroCliente,
        [property: JsonPropertyName("codigoModalidade")] int CodigoModalidade,
        [property: JsonPropertyName("numeroContaCorrente")] int NumeroContaCorrente,
        [property: JsonPropertyName("codigoEspecieDocumento")] string CodigoEspecieDocumento,
        [property: JsonPropertyName("dataEmissao")] string DataEmissao,
        [property: JsonPropertyName("nossoNumero")] int NossoNumero,
        [property: JsonPropertyName("seuNumero")] string SeuNumero,
        [property: JsonPropertyName("identificacaoBoletoEmpresa")] string IdentificacaoBoletoEmpresa,
        [property: JsonPropertyName("codigoBarras")] string CodigoBarras,
        [property: JsonPropertyName("linhaDigitavel")] string LinhaDigitavel,
        [property: JsonPropertyName("valor")] double Valor,
        [property: JsonPropertyName("dataVencimento")] string DataVencimento,
        [property: JsonPropertyName("valorAbatimento")] int ValorAbatimento,
        [property: JsonPropertyName("tipoDesconto")] int TipoDesconto,
        [property: JsonPropertyName("dataPrimeiroDesconto")] string DataPrimeiroDesconto,
        [property: JsonPropertyName("valorPrimeiroDesconto")] int ValorPrimeiroDesconto,
        [property: JsonPropertyName("dataSegundoDesconto")] string DataSegundoDesconto,
        [property: JsonPropertyName("valorSegundoDesconto")] int ValorSegundoDesconto,
        [property: JsonPropertyName("dataTerceiroDesconto")] string DataTerceiroDesconto,
        [property: JsonPropertyName("valorTerceiroDesconto")] int ValorTerceiroDesconto,
        [property: JsonPropertyName("tipoMulta")] int TipoMulta,
        [property: JsonPropertyName("dataMulta")] string DataMulta,
        [property: JsonPropertyName("valorMulta")] int ValorMulta,
        [property: JsonPropertyName("tipoJurosMora")] int TipoJurosMora,
        [property: JsonPropertyName("dataJurosMora")] string DataJurosMora,
        [property: JsonPropertyName("valorJurosMora")] int ValorJurosMora,
        [property: JsonPropertyName("numeroParcela")] int NumeroParcela,
        [property: JsonPropertyName("aceite")] bool Aceite,
        [property: JsonPropertyName("codigoNegativacao")] int CodigoNegativacao,
        [property: JsonPropertyName("codigoProtesto")] int CodigoProtesto,
        [property: JsonPropertyName("quantidadeDiasFloat")] int QuantidadeDiasFloat,
        [property: JsonPropertyName("pagador")] PagadorBoletos Pagador,
        [property: JsonPropertyName("beneficiarioFinal")] BeneficiarioFinalBoletos BeneficiarioFinal,
        [property: JsonPropertyName("mensagensInstrucao")] IReadOnlyList<string> MensagensInstrucao,
        [property: JsonPropertyName("situacaoBoleto")] string SituacaoBoleto,
        [property: JsonPropertyName("qrCode")] string QrCode,
        [property: JsonPropertyName("numeroContratoCobranca")] int NumeroContratoCobranca
    );

  
}
