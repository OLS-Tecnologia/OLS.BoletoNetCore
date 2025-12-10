using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response
{
    public class RecuperarCobrancaInterResponse : InterBaseResponseDto
    {

        [property: JsonProperty("cobranca")]
        public Cobranca Cobranca { get; set; }

        [property: JsonProperty("boleto")]
        Boleto Boleto { get; set; }

        [property: JsonProperty("pix")] 
        public Pix? Pix { get; set; }



    };

    public record Boleto(
        [property: JsonProperty("nossoNumero")] string NossoNumero,
        [property: JsonProperty("codigoBarras")] string CodigoBarras,
        [property: JsonProperty("linhaDigitavel")] string LinhaDigitavel
    );

    public record Cobranca(
        [property: JsonProperty("codigoSolicitacao")] string CodigoSolicitacao,
        [property: JsonProperty("seuNumero")] string SeuNumero,
        [property: JsonProperty("dataEmissao")] string DataEmissao,
        [property: JsonProperty("dataVencimento")] string DataVencimento,
        [property: JsonProperty("valorNominal")] double ValorNominal,
        [property: JsonProperty("tipoCobranca")] string TipoCobranca,
        [property: JsonProperty("situacao")] StatusBoletoEnum Situacao,
        [property: JsonProperty("dataSituacao")] string DataSituacao,
        [property: JsonProperty("valorTotalRecebido")] string ValorTotalRecebido,
        [property: JsonProperty("origemRecebimento")] string OrigemRecebimento,
        [property: JsonProperty("arquivada")] bool Arquivada,
        [property: JsonProperty("descontos")] IReadOnlyList<Desconto> Descontos,
        [property: JsonProperty("multa")] Multa Multa,
        [property: JsonProperty("mora")] Mora Mora,
        [property: JsonProperty("pagador")] Pagador Pagador
    );

    public record Desconto(
        [property: JsonProperty("codigo")] string Codigo,
        [property: JsonProperty("quantidadeDias")] int QuantidadeDias,
        [property: JsonProperty("taxa")] int Taxa
    );

    public record Mora(
        [property: JsonProperty("codigo")] string Codigo,
        [property: JsonProperty("taxa")] int Taxa
    );

    public record Multa(
        [property: JsonProperty("codigo")] string Codigo,
        [property: JsonProperty("valor")] int Valor
    );

    //public record Pagador(
    //    [property: JsonProperty("email")] string Email,
    //    [property: JsonProperty("ddd")] string Ddd,
    //    [property: JsonProperty("telefone")] string Telefone,
    //    [property: JsonProperty("numero")] string Numero,
    //    [property: JsonProperty("complemento")] string Complemento,
    //    [property: JsonProperty("cpfCnpj")] string CpfCnpj,
    //    [property: JsonProperty("tipoPessoa")] string TipoPessoa,
    //    [property: JsonProperty("nome")] string Nome,
    //    [property: JsonProperty("endereco")] string Endereco,
    //    [property: JsonProperty("bairro")] string Bairro,
    //    [property: JsonProperty("cidade")] string Cidade,
    //    [property: JsonProperty("uf")] string Uf,
    //    [property: JsonProperty("cep")] string Cep
    //);

    public record Pix(
        [property: JsonProperty("txid")] string Txid,
        [property: JsonProperty("pixCopiaECola")] string PixCopiaECola
    );
}
