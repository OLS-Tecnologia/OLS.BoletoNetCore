using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response
{
    public class BoletosPagadorSicoobResponseDto
    {
        [JsonPropertyName("resultado")]
        public  IReadOnlyList<PagadorBoletosResultado> Resultado { get; set; }
    }

    public class BeneficiarioFinalBoletosSicoob
    {
        [JsonPropertyName("nome")] public string Nome {  get; set; }
    }

    public class PagadorBoletos
    {
        [JsonPropertyName("numeroCpfCnpj")] public string NumeroCpfCnpj { get; set; }
        [JsonPropertyName("nome")] public string Nome { get; set; }
    }

    public class PagadorBoletosResultado
    {
        [JsonPropertyName("numeroCliente")]  public int NumeroCliente { get; set;}
        [JsonPropertyName("codigoModalidade")]  public int CodigoModalidade { get; set;}
        [JsonPropertyName("numeroContaCorrente")]  public int NumeroContaCorrente { get; set;}
        [JsonPropertyName("codigoEspecieDocumento")]  public string CodigoEspecieDocumento { get; set;}
        [JsonPropertyName("dataEmissao")]  public string DataEmissao { get; set;}
        [JsonPropertyName("nossoNumero")]  public int NossoNumero { get; set;}
        [JsonPropertyName("seuNumero")]  public string SeuNumero { get; set;}
        [JsonPropertyName("identificacaoBoletoEmpresa")]  public string IdentificacaoBoletoEmpresa { get; set;}
        [JsonPropertyName("codigoBarras")]  public string CodigoBarras { get; set;}
        [JsonPropertyName("linhaDigitavel")]  public string LinhaDigitavel { get; set;}
        [JsonPropertyName("valor")]  public double Valor { get; set;}
        [JsonPropertyName("dataVencimento")]  public string DataVencimento { get; set;}
        [JsonPropertyName("valorAbatimento")]  public int ValorAbatimento { get; set;}
        [JsonPropertyName("tipoDesconto")]  public int TipoDesconto { get; set;}
        [JsonPropertyName("dataPrimeiroDesconto")]  public string DataPrimeiroDesconto { get; set;}
        [JsonPropertyName("valorPrimeiroDesconto")]  public int ValorPrimeiroDesconto { get; set;}
        [JsonPropertyName("dataSegundoDesconto")]  public string DataSegundoDesconto { get; set;}
        [JsonPropertyName("valorSegundoDesconto")]  public int ValorSegundoDesconto { get; set;}
        [JsonPropertyName("dataTerceiroDesconto")]  public string DataTerceiroDesconto { get; set;}
        [JsonPropertyName("valorTerceiroDesconto")]  public int ValorTerceiroDesconto { get; set;}
        [JsonPropertyName("tipoMulta")]  public int TipoMulta { get; set;}
        [JsonPropertyName("dataMulta")]  public string DataMulta { get; set;}
        [JsonPropertyName("valorMulta")]  public int ValorMulta { get; set;}
        [JsonPropertyName("tipoJurosMora")]  public int TipoJurosMora { get; set;}
        [JsonPropertyName("dataJurosMora")]  public string DataJurosMora { get; set;}
        [JsonPropertyName("valorJurosMora")]  public int ValorJurosMora { get; set;}
        [JsonPropertyName("numeroParcela")]  public int NumeroParcela { get; set;}
        [JsonPropertyName("aceite")]  public bool Aceite { get; set;}
        [JsonPropertyName("codigoNegativacao")]  public int CodigoNegativacao { get; set;}
        [JsonPropertyName("codigoProtesto")]  public int CodigoProtesto { get; set;}
        [JsonPropertyName("quantidadeDiasFloat")]  public int QuantidadeDiasFloat { get; set;}
        [JsonPropertyName("pagador")]  public PagadorBoletos Pagador { get; set;}
        [JsonPropertyName("beneficiarioFinal")]  public BeneficiarioFinalBoletosSicoob BeneficiarioFinal { get; set;}
        [JsonPropertyName("mensagensInstrucao")]  public IReadOnlyList<string> MensagensInstrucao { get; set;}
        [JsonPropertyName("situacaoBoleto")]  public string SituacaoBoleto { get; set;}
        [JsonPropertyName("qrCode")]  public string QrCode { get; set;}
        [JsonPropertyName("numeroContratoCobranca")] public int NumeroContratoCobranc { get; set; }
    }

  
}
