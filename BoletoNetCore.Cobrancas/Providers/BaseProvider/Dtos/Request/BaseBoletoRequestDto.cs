using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;

namespace BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request
{
    public class BaseProviderGerarBoletoRequestDto
    {

        [JsonProperty("isApiRequest")]
        public bool IsApiRequest { get; set; }

        [JsonProperty("numeroCliente")]
        [Required]
        public int NumeroCliente { get; set; }

        [JsonProperty("codigoModalidade")]
        [Required]
        public int CodigoModalidade { get; set; }

        [JsonProperty("numeroContaCorrente")]
        [Required]
        public int NumeroContaCorrente { get; set; }

        [JsonProperty("codigoEspecieDocumento")]
        [Required]
        public CodigoEspecieDocumentosEnum CodigoEspecieDocumento { get; set; }

        [JsonProperty("dataEmissao")]
        [Required]
        public DateOnly DataEmissao { get; set; }

        [JsonProperty("x-conta-corrente")]
        [Required]
        public string XContaCorrente { get; set; } // header inter

        [JsonProperty("seuNumero")]
        [Required]
        public string SeuNumero { get; set; }


        [JsonProperty("identificacaoEmissaoBoleto")]
        [Required]
        public int IdentificacaoEmissaoBoleto { get; set; }

        [JsonProperty("identificacaoDistribuicaoBoleto")]
        [Required]
        public int IdentificacaoDistribuicaoBoleto { get; set; }

        [JsonProperty("valorNominal")]
        [Required]
        public double ValorNominal { get; set; }
        public Pagador Pagador { get; set; }

        [JsonProperty("client_id")]
        [Required]
        public string ClientId { get; set; } // header sicoob

        [JsonProperty("dataVencimento")]
        [Required]
        public DateOnly DataVencimento { get; set; }

        [JsonProperty("nossoNumero")]
        public int NossoNumero { get; set; }

        [JsonProperty("identificacaoBoletoEmpresa")]
        public string IdentificacaoBoletoEmpresa { get; set; }       
       

        [JsonProperty("beneficiario")]
        public BeneficiarioFinal Beneficiario { get; set; }

        [JsonProperty("gerarPdf")]
        public bool GerarPdf { get; set; } = false;  

        [JsonProperty("codigoProtesto")]
        public int CodigoProtesto { get; set; }

        [JsonProperty("numDiasAgenda")]
        public int NumDiasAgenda { get; set; } // Número de dias corridos após o vencimento para o cancelamento efetivo automático da cobrança. (de 0 até 60), Inter


        [JsonProperty("codigoCadastrarPIX")]
        public ModeloBoletoEnum CodigoCadastrarPIX { get; set; }

        [JsonProperty("numeroContratoCobranca")]
        public int NumeroContratoCobranca { get; set; }
     

        [JsonProperty("aceite")]
        public bool Aceite {  get; set; }

        [JsonProperty("codigoNegativacao")]
        public int CodigoNegativacao { get; set; }

        [JsonProperty("numeroDiasNegativacao")]
        public int NumeroDiasNegativacao { get; set; }       

        [JsonProperty("numeroDiasProtesto")]
        public int NumeroDiasProtesto { get; set; }

        [JsonProperty("numeroParcela")]
        [Required]
        public int NumeroParcela { get; set; }
        

        [JsonProperty("dataLimitePagamento")]
        public DateOnly? DataLimitePagamento { get; set; }

        [JsonProperty("dataPrimeiroDesconto")]
        public DateOnly? DataPrimeiroDesconto { get; set; }

        [JsonProperty("valorPrimeiroDesconto")]
        public double? ValorPrimeiroDesconto { get; set; }

        [JsonProperty("dataSegundoDesconto")]
        public DateOnly? DataSegundoDesconto { get; set; }

        [JsonProperty("valorSegundoDesconto")]
        public double? ValorSegundoDesconto { get; set; }

        [JsonProperty("dataTerceiroDesconto")]
        public DateOnly? DataTerceiroDesconto { get; set; }

        [JsonProperty("valorTerceiroDesconto")]
        public double? ValorTerceiroDesconto { get; set; }

        [JsonProperty("valorAbatimento")]
        public double? ValorAbatimento { get; set; }

        [JsonProperty("mensagensInstrucao")]
        public List<string>? MensagensInstrucao { get; set; }


        [JsonProperty("rateioCreditos")]
        public List<RateioCredito>? RateioCreditos { get; set; }

        [JsonProperty("valorJurosMora")]
        public double? ValorJurosMora { get; set; }

        [JsonProperty("taxaJurosMora")]
        public double? TaxaJurosMora { get; set; }

        [JsonProperty("tipoJurosMora")]
        [Required]
        public MoraCodigosEnum TipoJurosMora { get; set; }

        [JsonProperty("dataJurosMora")]
        public DateOnly? DataJurosMora { get; set; }

        [JsonProperty("valorMulta")]
        public double? ValorMulta { get; set; }

        [JsonProperty("taxaMulta")]
        public double? TaxaMulta { get; set; }

        [JsonProperty("tipoMulta")]
        [Required]
        public MultaCodigosEnum TipoMulta { get; set; }

        [JsonProperty("dataMulta")]
        public DateOnly? DataMulta { get; set; }

        [JsonProperty("valorDesconto")]
        public double? ValorDesconto { get; set; } = 1;

        [JsonProperty("taxaDesconto")]
        public double? TaxaDesconto { get; set; } = 1;

        [JsonProperty("tipoDesconto")]
        [Required]
        public TipoDesconto TipoDesconto { get; set; }

        [JsonProperty("quantidadeDias")]
        [Required]
        public int? QuantidadeDias { get; set; } = 1; // Quantidade de dias antes do vencimento que será aplicado o desconto. Inter
    }

    public class Pagador : BeneficiarioFinal
    {

        public Pagador() {
            NumeroCpfCnpj = base.CpfCnpj;
        }


        [JsonProperty("ddd")]
        public string Ddd { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }

        [JsonProperty("numeroCpfCnpj")] // recebe do campo CpfCnpj da genérica
        [Required]
        public string NumeroCpfCnpj { get; private set; }
    }

    public class BeneficiarioFinal
    {

        public BeneficiarioFinal() {

            NumeroCpfCnpj = this.CpfCnpj; 
        }

        [JsonProperty("cpfCnpj")]
        public string CpfCnpj { get; set; }


        [JsonProperty("numeroCpfCnpj")] // recebe de CpfCnpj
        public string NumeroCpfCnpj { get; private set; }



        [JsonProperty("nome")]
        [Required]
        public string Nome { get; set; }

        [JsonProperty("tipoPessoa")]
        [Required]
        public TipoPessoa TipoPessoa { get; set; }

        [JsonProperty("cep")]
        [Required]
        public string Cep { get; set; }

        [JsonProperty("uf")]
        [Required]
        public UfBrasil Uf { get; set; }

        [JsonProperty("cidade")]
        [Required]
        public string Cidade { get; set; }

        [JsonProperty("endereco")]
        [Required]
        public string Endereco { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }
    }    

    
    public class RateioCredito
    {
        [JsonProperty("numeroBanco")]
        [Required]
        public int NumeroBanco { get; set; }

        [JsonProperty("numeroAgencia")]
        [Required]
        public int NumeroAgencia { get; set; }

        [JsonProperty("numeroContaCorrente")]
        [Required]
        public int NumeroContaCorrente { get; set; }

        [JsonProperty("contaPrincipal")]
        [Required]
        public bool ContaPrincipal { get; set; }

        [JsonProperty("codigoTipoValorRateio")]
        [Required]
        public int CodigoTipoValorRateio { get; set; }

        [JsonProperty("valorRateio")]
        [Required]
        public int ValorRateio { get; set; }

        [JsonProperty("codigoTipoCalculoRateio")]
        [Required]
        public int CodigoTipoCalculoRateio { get; set; }

        [JsonProperty("numeroCpfCnpjTitular")]
        [Required]
        public string NumeroCpfCnpjTitular { get; set; }

        [JsonProperty("nomeTitular")]
        [Required]
        public string NomeTitular { get; set; }

        [JsonProperty("codigoFinalidadeTed")]
        [Required]
        public int CodigoFinalidadeTed { get; set; }

        [JsonProperty("codigoTipoContaDestinoTed")]
        [Required]
        public string CodigoTipoContaDestinoTed { get; set; }

        [JsonProperty("quantidadeDiasFloat")]
        [Required]
        public int QuantidadeDiasFloat { get; set; }

        [JsonProperty("dataFloatCredito")]
        [Required]
        public string DataFloatCredito { get; set; }
    }
}
