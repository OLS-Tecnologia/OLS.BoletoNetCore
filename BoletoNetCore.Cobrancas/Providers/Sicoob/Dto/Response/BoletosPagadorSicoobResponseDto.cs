using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response
{
    public  record BoletosPagadorSicoobResponseDto
    (
        [property: JsonProperty("resultado")]
        IReadOnlyList<PagadorBoletosResultado> Resultado
    );

    public record BeneficiarioFinalBoletos(
      [property: JsonProperty("nome")] string Nome
  );

    public record PagadorBoletos(
        [property: JsonProperty("numeroCpfCnpj")] string NumeroCpfCnpj,
        [property: JsonProperty("nome")] string Nome
    );

    public record PagadorBoletosResultado(
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
        [property: JsonProperty("valor")] double Valor,
        [property: JsonProperty("dataVencimento")] string DataVencimento,
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
        [property: JsonProperty("codigoProtesto")] int CodigoProtesto,
        [property: JsonProperty("quantidadeDiasFloat")] int QuantidadeDiasFloat,
        [property: JsonProperty("pagador")] PagadorBoletos Pagador,
        [property: JsonProperty("beneficiarioFinal")] BeneficiarioFinalBoletos BeneficiarioFinal,
        [property: JsonProperty("mensagensInstrucao")] IReadOnlyList<string> MensagensInstrucao,
        [property: JsonProperty("situacaoBoleto")] string SituacaoBoleto,
        [property: JsonProperty("qrCode")] string QrCode,
        [property: JsonProperty("numeroContratoCobranca")] int NumeroContratoCobranca
    );

  
}
