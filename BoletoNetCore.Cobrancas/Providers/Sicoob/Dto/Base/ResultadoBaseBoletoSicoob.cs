using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Base
{
    public class ResultadoBaseBoletoSicoob()
    {

         [JsonPropertyName("numeroCliente")]
         public int NumeroCliente { get; set; }

         [JsonPropertyName("codigoModalidade")] public int CodigoModalidade {  get; set; }


         [ JsonPropertyName("numeroContaCorrente")] public int? NumeroContaCorrente { get; set; }


         [ JsonPropertyName("codigoEspecieDocumento")] public string CodigoEspecieDocumento { get; set; }


         [JsonPropertyName("dataEmissao")] public string DataEmissao { get; set; }

        [JsonPropertyName("nossoNumero")]
        public int NossoNumero { get; set; }

        [JsonPropertyName("seuNumero")]
        public string SeuNumero { get; set; }

        [JsonPropertyName("identificacaoBoletoEmpresa")]
        public string IdentificacaoBoletoEmpresa { get; set; }

        [JsonPropertyName("codigoBarras")]
        public string CodigoBarras { get; set; }

        [JsonPropertyName("linhaDigitavel")]
        public string LinhaDigitavel { get; set; }

        [JsonPropertyName("identificacaoEmissaoBoleto")]
        public int IdentificacaoEmissaoBoleto { get; set; }

        [JsonPropertyName("identificacaoDistribuicaoBoleto")]
        public int IdentificacaoDistribuicaoBoleto { get; set; }


        [JsonPropertyName("valor")]
        public double Valor { get; set; }

        [JsonPropertyName("dataVencimento")]
        public string DataVencimento { get; set; }

        [JsonPropertyName("dataLimitePagamento")]
        public string DataLimitePagamento { get; set; }

        [JsonPropertyName("valorAbatimento")]
        public double ValorAbatimento { get; set; }

        [JsonPropertyName("tipoDesconto")]
        public int TipoDesconto { get; set; }

        [JsonPropertyName("dataPrimeiroDesconto")]
        public string? DataPrimeiroDesconto { get; set; }

        [JsonPropertyName("valorPrimeiroDesconto")]
        public double? ValorPrimeiroDesconto { get; set; }

        [JsonPropertyName("dataSegundoDesconto")]
        public string? DataSegundoDesconto { get; set; }

        [JsonPropertyName("valorSegundoDesconto")]
        public double? ValorSegundoDesconto { get; set; }

        [JsonPropertyName("dataTerceiroDesconto")]
        public string? DataTerceiroDesconto { get; set; }

        [JsonPropertyName("valorTerceiroDesconto")]
        public double? ValorTerceiroDesconto { get; set; }

        [JsonPropertyName("tipoMulta")]
        public int TipoMulta { get; set; }

        [JsonPropertyName("dataMulta")]
        public string? DataMulta { get; set; }

        [JsonPropertyName("valorMulta")]
        public double ValorMulta { get; set; }

        [JsonPropertyName("tipoJurosMora")]
        public int TipoJurosMora { get; set; }

        [JsonPropertyName("dataJurosMora")]
        public string? DataJurosMora { get; set; }

        [JsonPropertyName("valorJurosMora")]
        public double ValorJurosMora { get; set; }

        [JsonPropertyName("numeroParcela")]
        public int NumeroParcela { get; set; }

        [JsonPropertyName("aceite")]
        public bool Aceite { get; set; }


        [JsonPropertyName("numeroDiasNegativacao")]
        public int? NumeroDiasNegativacao { get; set; }
        

        [JsonPropertyName("numeroDiasProtesto")]
        public int? NumeroDiasProtesto { get; set; }

        [JsonPropertyName("quantidadeDiasFloat")]
        public int QuantidadeDiasFloat { get; set; }       

        [JsonPropertyName("mensagensInstrucao")]
        public IReadOnlyList<string>? MensagensInstrucao { get; set; }
        

        [JsonPropertyName("qrCode")]
        public string? QrCode { get; set; }

        [JsonPropertyName("numeroContratoCobranca")]
        public int? NumeroContratoCobranca { get; set; }

    };

    
}
