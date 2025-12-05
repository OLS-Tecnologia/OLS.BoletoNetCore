using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using Newtonsoft.Json;


namespace BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response
{
    public class BaseProviderGerarBoletoResponseDto
    {
        [JsonProperty("numeroCliente")] 
           public  int NumeroCliente {get; set;}
        [JsonProperty("codigoModalidade")] 
           public  int CodigoModalidade {get; set;}
        [JsonProperty("numeroContaCorrente")] 
           public  int NumeroContaCorrente {get; set;}
        [JsonProperty("codigoEspecieDocumento")] 
           public  string CodigoEspecieDocumento {get; set;}
        [JsonProperty("dataEmissao")] 
           public  string DataEmissao {get; set;}
        [JsonProperty("nossoNumero")] 
           public  string NossoNumero { get; set; }
        [JsonProperty("seuNumero")] 
           public  string SeuNumero {get; set;}
        [JsonProperty("identificacaoBoletoEmpresa")] 
           public  string IdentificacaoBoletoEmpresa {get; set;}
        [JsonProperty("codigoBarras")] 
           public  string CodigoBarras {get; set;}
        [JsonProperty("linhaDigitavel")] 
           public  string LinhaDigitavel {get; set;}
        [JsonProperty("identificacaoEmissaoBoleto")] 
           public  int IdentificacaoEmissaoBoleto {get; set;}
        [JsonProperty("identificacaoDistribuicaoBoleto")] 
           public  int IdentificacaoDistribuicaoBoleto {get; set;}
        [JsonProperty("valor")] 
           public  double Valor {get; set;}
        [JsonProperty("dataVencimento")] 
           public  string DataVencimento {get; set;}
        [JsonProperty("dataLimitePagamento")] 
           public  string DataLimitePagamento {get; set;}
        [JsonProperty("valorAbatimento")] 
           public  int ValorAbatimento {get; set;}
        [JsonProperty("tipoDesconto")] 
           public  int TipoDesconto {get; set;}
        [JsonProperty("dataPrimeiroDesconto")] 
           public  string DataPrimeiroDesconto {get; set;}
        [JsonProperty("valorPrimeiroDesconto")] 
           public  int ValorPrimeiroDesconto {get; set;}
        [JsonProperty("dataSegundoDesconto")] 
           public  string DataSegundoDesconto {get; set;}
        [JsonProperty("valorSegundoDesconto")] 
           public  int ValorSegundoDesconto {get; set;}
        [JsonProperty("dataTerceiroDesconto")] 
           public  string DataTerceiroDesconto {get; set;}
        [JsonProperty("valorTerceiroDesconto")] 
           public  int ValorTerceiroDesconto {get; set;}
        [JsonProperty("tipoMulta")] 
           public  int TipoMulta {get; set;}
        [JsonProperty("dataMulta")] 
           public  string DataMulta {get; set;}
        [JsonProperty("valorMulta")] 
           public  int ValorMulta {get; set;}
        [JsonProperty("tipoJurosMora")] 
           public  int TipoJurosMora {get; set;}
        [JsonProperty("dataJurosMora")] 
           public  string DataJurosMora {get; set;}
        [JsonProperty("valorJurosMora")] 
           public  int ValorJurosMora {get; set;}
        [JsonProperty("numeroParcela")] 
           public  int NumeroParcela {get; set;}
        [JsonProperty("aceite")] 
           public  bool Aceite {get; set;}
        [JsonProperty("codigoNegativacao")] 
           public  int CodigoNegativacao { get; set; }
        [JsonProperty("numeroDiasNegativacao")] 
           public  int NumeroDiasNegativacao {get; set;}
        [JsonProperty("codigoProtesto")] 
           public  int CodigoProtesto {get; set;}
        [JsonProperty("numeroDiasProtesto")] 
           public  int NumeroDiasProtesto {get; set;}
        [JsonProperty("quantidadeDiasFloat")] 
           public  int QuantidadeDiasFloat {get; set;}
        [JsonProperty("pagador")] 
           public  Pagador Pagador {get; set;}
        [JsonProperty("beneficiarioFinal")] 
           public  Beneficiario? BeneficiarioFinal {get; set;}
        [JsonProperty("mensagensInstrucao")] 
           public  IReadOnlyList<string>? MensagensInstrucao {get; set;}
        [JsonProperty("rateioCreditos")] 
           public  IReadOnlyList<RateioCredito>? RateioCreditos {get; set;}
        [JsonProperty("pdfBoleto")] 
           public  string? PdfBoleto {get; set;}
        [JsonProperty("qrCode")] 
           public  string? QrCode {get; set;}
        [JsonProperty("numeroContratoCobranca")] 
           public  int? NumeroContratoCobranca {get; set;}
        [JsonProperty("descricaoRejeicaoPix")]
           public string? DescricaoRejeicaoPix { get; set; }

        [JsonProperty("situacaoBoleto")]
        public StatusBoletoEnum StatusBoleto {get; set;}
     };

    public class Beneficiario
    {     

        [JsonProperty("cpfCnpj")]
        public string? CpfCnpj { get; set; }

        [JsonProperty("nome")]        
        public string? Nome { get; set; }

    }



}
