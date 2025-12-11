using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response
{
    public class RecuperarCobrancaInterResponse : InterBaseResponseDto
    {

        [property: JsonPropertyName("cobranca")]
        public Cobranca Cobranca { get; set; }

        [property: JsonPropertyName("boleto")]
        public Boleto? Boleto { get; set; }

        [property: JsonPropertyName("pix")] 
        public Pix? Pix { get; set; }



    };

    public record Boleto(
        [property: JsonPropertyName("nossoNumero")] string NossoNumero,
        [property: JsonPropertyName("codigoBarras")] string CodigoBarras,
        [property: JsonPropertyName("linhaDigitavel")] string LinhaDigitavel
    );

    public record Cobranca(
        [property: JsonPropertyName("codigoSolicitacao")] string CodigoSolicitacao,
        [property: JsonPropertyName("seuNumero")] string SeuNumero,
        [property: JsonPropertyName("dataEmissao")] string DataEmissao,
        [property: JsonPropertyName("dataVencimento")] string DataVencimento,
        [property: JsonPropertyName("valorNominal")] double ValorNominal,
        [property: JsonPropertyName("tipoCobranca")] string TipoCobranca,
        [property: JsonPropertyName("situacao")] string Situacao, // StatusBoletoEnum 
        [property: JsonPropertyName("dataSituacao")] string? DataSituacao,
        [property: JsonPropertyName("valorTotalRecebido")] string? ValorTotalRecebido,
        [property: JsonPropertyName("origemRecebimento")] string? OrigemRecebimento,
        [property: JsonPropertyName("motivoCancelamento")] string? MotivoCancelamento,
        [property: JsonPropertyName("arquivada")] bool? Arquivada,
        [property: JsonPropertyName("descontos")] IReadOnlyList<Desconto>? Descontos,
        [property: JsonPropertyName("multa")] Multa? Multa,
        [property: JsonPropertyName("mora")] Mora? Mora,
        [property: JsonPropertyName("pagador")] PagadorBase? Pagador
    );

    public record Desconto(
        [property: JsonPropertyName("codigo")] string Codigo,
        [property: JsonPropertyName("quantidadeDias")] int QuantidadeDias,
        [property: JsonPropertyName("taxa")] int Taxa
    );

    public record Mora(
        [property: JsonPropertyName("codigo")] string Codigo,
        [property: JsonPropertyName("taxa")] int? Taxa,
        [property: JsonPropertyName("valor")] int? Valor
    );

    public record Multa(
        [property: JsonPropertyName("codigo")] string Codigo,
        [property: JsonPropertyName("taxa")] int? Taxa,
        [property: JsonPropertyName("valor")] int? Valor
    );

  

    public record Pix(
        [property: JsonPropertyName("txid")] string Txid,
        [property: JsonPropertyName("pixCopiaECola")] string PixCopiaECola
    );
}
