using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request
{
    public class IncluirBoletoSicoobResquetDto
    {
        public IncluirBoletoSicoobResquetDto(string clienteId, IncluirBoletoSicoobRequestBody boleto)
        {
            ClienteId = clienteId;
            Boleto = boleto;
        }

        [property: JsonProperty("client_id")]
        [Required]
        string ClienteId { get; set; }

        [property: JsonProperty("boleto")]
        [Required]
        IncluirBoletoSicoobRequestBody Boleto { get; set; }
    }   
   


    public class IncluirBoletoSicoobRequestBody
    {
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

        [JsonProperty("nossoNumero")]
        public int NossoNumero { get; set; }

        [JsonProperty("seuNumero")]
        [Required]
        public string SeuNumero { get; set; }

        [JsonProperty("identificacaoBoletoEmpresa")]
        public string IdentificacaoBoletoEmpresa { get; set; }

        [JsonProperty("identificacaoEmissaoBoleto")]
        [Required]
        public int IdentificacaoEmissaoBoleto { get; set; }

        [JsonProperty("identificacaoDistribuicaoBoleto")]
        [Required]
        public int IdentificacaoDistribuicaoBoleto { get; set; }

        [JsonProperty("valor")]
        [Required]
        public double Valor { get; set; }

        [JsonProperty("dataVencimento")]
        [Required]
        public DateOnly DataVencimento { get; set; }

        [JsonProperty("dataLimitePagamento")]
        public DateOnly? DataLimitePagamento { get; set; }

        [JsonProperty("valorAbatimento")]
        public double? ValorAbatimento { get; set; }

        [JsonProperty("tipoDesconto")]
        [Required]
        public int TipoDesconto { get; set; }

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
        public double?  ValorTerceiroDesconto { get; set; }

       
        [JsonProperty("tipoMulta")]
        [Required]
        public int TipoMulta { get; set; }
      
        [JsonProperty("dataMulta")]
        public DateOnly? DataMulta { get; set; }

        [JsonProperty("valorMulta")]
        public double? ValorMulta { get; set; }

       
        [JsonProperty("tipoJurosMora")]
        [Required]
        public int TipoJurosMora { get; set; }

       
        [JsonProperty("dataJurosMora")]
        public DateOnly? DataJurosMora { get; set; }

        [JsonProperty("valorJurosMora")]
        public double? ValorJurosMora { get; set; }

        [JsonProperty("numeroParcela")]
        [Required]
        public int NumeroParcela { get; set; }

        [JsonProperty("aceite")]
        public bool Aceite { get; set; }

        [JsonProperty("codigoNegativacao")]
        public int CodigoNegativacao { get; set; }

        [JsonProperty("numeroDiasNegativacao")]
        public int NumeroDiasNegativacao { get; set; }

       
        [JsonProperty("codigoProtesto")]
        public int CodigoProtesto { get; set; }

        [JsonProperty("numeroDiasProtesto")]
        public int NumeroDiasProtesto { get; set; }

        [JsonProperty("pagador")]
        [Required]
        public Pagador Pagador { get; set; }

        [JsonProperty("beneficiarioFinal")]
        public BeneficiarioFinal BeneficiarioFinal { get; set; }

        [JsonProperty("mensagensInstrucao")]
        public List<string>? MensagensInstrucao { get; set; }

        [JsonProperty("gerarPdf")]
        public bool GerarPdf { get; set; }

        [JsonProperty("rateioCreditos")]
        public List<RateioCredito>? RateioCreditos { get; set; }

        [JsonProperty("codigoCadastrarPIX")]
        public int CodigoCadastrarPIX { get; set; }

        [JsonProperty("numeroContratoCobranca")]
        public int NumeroContratoCobranca { get; set; }

        public IncluirBoletoSicoobRequestBody(int numeroCliente, int codigoModalidade, int numeroContaCorrente, CodigoEspecieDocumentosEnum codigoEspecieDocumento, DateOnly dataEmissao, 
            int nossoNumero, string seuNumero, string identificacaoBoletoEmpresa, int identificacaoEmissaoBoleto, int identificacaoDistribuicaoBoleto, double valor,
            DateOnly dataVencimento, int tipoDesconto, int tipoMulta, int tipoJurosMora, int numeroParcela, bool aceite, int codigoNegativacao,
            int numeroDiasNegativacao, int codigoProtesto, int numeroDiasProtesto, Pagador pagador, BeneficiarioFinal beneficiarioFinal, bool gerarPdf,
            int codigoCadastrarPIX, int numeroContratoCobranca, DateOnly? dataMulta = null, double? valorMulta = null, DateOnly? dataLimitePagamento = null, DateOnly? dataJurosMora = null, 
            DateOnly? dataPrimeiroDesconto = null, double? valorPrimeiroDesconto = null,
            DateOnly? dataSegundoDesconto = null, double? valorSegundoDesconto =null, DateOnly? dataTerceiroDesconto = null, double? valorTerceiroDesconto = null,
            double? valorAbatimento = null, List<string>? mensagensInstrucao = null, List<RateioCredito>? rateioCreditos = null, double? valorJurosMora = null)
        {
            List<string> listErros = [];
            DateOnly DataAtual = new();

            bool isValidCodigo = CodigoProtestoOptions.ContainsKey(CodigoProtesto);

            if (!isValidCodigo)
            {
                listErros.Add("Valor inválido para CodigoProtesto. Valores esperados para o banco Sicoob: 1 - Protestar Dias Corridos, 2 - Protestar Dias Úteis, 3 - Não Protestar ");                  

            }
            if (dataVencimento < DataAtual)
            {
                listErros.Add("Data de vencimento da cobranca deve ser data futura.");
            }


            if (!CodigoNegativacaoOptions.ContainsKey(CodigoNegativacao))
            {
                listErros.Add(" Valor inválido para CodigoNegativacao. Valores esperados para o banco Sicoob: 2 - Negativar dias uteis, 3 - Não negativar");
            }        



            if (!TipoJurosMoraOptions.ContainsKey(tipoJurosMora))
            {
                listErros.Add("Valor inválido para tipoJurosMora. Valores esperados para o banco Sicoob: 1 - Valor por dia, 2 - Taxa Mensal, 3 - Isento");

            }         

               

            if (!TipoMultaOptions.ContainsKey(tipoMulta))
            {
                listErros.Add("Valor inválido para tipoMulta. Valores esperados para o banco Sicoob: 0 - Isento, 1 - Valor Fixo, 2 - Percentual");
            }



            if (!ModalidadeBoletoOptions.ContainsKey(codigoModalidade))
            {
                listErros.Add("Valor inválido para codigoModalidadeBoleto. Valor esperado para o banco Sicoob: 1 - SIMPLES COM REGISTRO");
            }

            if (!Enum.TryParse<CodigoEspecieDocumentosEnum>(codigoEspecieDocumento.ToString(), ignoreCase: true, out var c))
            {
                listErros.Add(@$"Valor inválido para codigoEspecieDocumento. Valores esperados para o banco Sicoob: {string.Join(", ", Enum.GetNames(typeof(CodigoEspecieDocumentosEnum)))}");

            }
            //TipoDesconto
            if (!Enum.IsDefined(typeof(TipoDesconto), tipoDesconto))
            {
                listErros.Add($" Valor inválido para tipoDesconto. Valores esperados para o banco Sicoob: {string.Join(", ", Enum.GetValues(typeof(TipoDesconto)))}");
            }

            if (!CodigoCadastrarPixOptions.ContainsKey(CodigoCadastrarPIX))
            {
                listErros.Add($" Valor inválido paraVPix. Valores esperados para o banco Sicoob: {CodigoCadastrarPixOptions.ToList()}");
            }

            // Valor da multa. Deve ser preenchido caso o campo dataMulta seja preenchido.
            if (dataMulta is not null)
            {
                if(valorMulta is null)
                {
                    listErros.Add("Campo data multa foi preenchido, o valor da multa deve ser informado.");
                }

            }

            // Data Juros mora - Deve ser maior que a data de vencimento do boleto e menor ou igual que data limite de pagamento.
            if(dataJurosMora is not null)
            {
                if(dataJurosMora < dataVencimento)
                {
                    listErros.Add("Campo DataJurosMora deve ser maior que a data de vencimento do boleto.");

                }

                if(dataLimitePagamento is not null)
                {
                    if (dataJurosMora > dataLimitePagamento)
                    {
                        listErros.Add("Campo DataJurosMora não pode ser maior que a data limite de pagamento do boleto.");
                    }
                }
                // validar valor juros mora

                if(valorJurosMora is null)
                {
                    listErros.Add("Campo DataJurosMora foi preenchido, obrigatório informar o valor juros mora");

                }


            }


            if(dataPrimeiroDesconto is not null)
            {
                if (valorPrimeiroDesconto is null)
                {
                    listErros.Add("Obrigatório informar valor do primeiro desconto.");

                }
            }

            if (dataSegundoDesconto is not null)
            {
                if (valorSegundoDesconto is null)
                {
                    listErros.Add("Obrigatório informar valor do segundo desconto.");

                }
            }
            
            if (dataTerceiroDesconto is not null)
            {
                if (valorTerceiroDesconto is null)
                {
                    listErros.Add("Obrigatório informar valor do terceiro desconto.");

                }
            }

            if(mensagensInstrucao is not null)
            {

                if (mensagensInstrucao.Count > 5) {

                    listErros.Add("São permitidas apenas 5 mensagens de instrução.");

                }
                else
                {
                    foreach (string msg in mensagensInstrucao)
                    {
                        if (msg.Length > 40)
                        {
                            listErros.Add("As mensagens de instrução devem ter no máximo 40 caracteres.");
                        }
                    }

                }
                  

            }

            if (listErros.Count > 0)
            {
                Console.WriteLine(" Erros na validação do IncluirBoletoSicoobRequestDto"); // logger
              // TODO:  throw new ValidationResult(listErros);
            }



            NumeroCliente = numeroCliente;
            CodigoModalidade = codigoModalidade;
            NumeroContaCorrente = numeroContaCorrente;
            CodigoEspecieDocumento = codigoEspecieDocumento;
            DataEmissao = dataEmissao;
            NossoNumero = nossoNumero;
            SeuNumero = seuNumero;
            IdentificacaoBoletoEmpresa = identificacaoBoletoEmpresa;
            IdentificacaoEmissaoBoleto = identificacaoEmissaoBoleto;
            IdentificacaoDistribuicaoBoleto = identificacaoDistribuicaoBoleto;
            Valor = valor;
            DataVencimento = dataVencimento;
            DataLimitePagamento = dataLimitePagamento;
            ValorAbatimento = valorAbatimento;
            TipoDesconto = tipoDesconto;
            DataPrimeiroDesconto = dataPrimeiroDesconto;
            ValorPrimeiroDesconto = valorPrimeiroDesconto;
            DataSegundoDesconto = dataSegundoDesconto;
            ValorSegundoDesconto = valorSegundoDesconto;
            DataTerceiroDesconto = dataTerceiroDesconto;
            ValorTerceiroDesconto = valorTerceiroDesconto;
            TipoMulta = tipoMulta;
            DataMulta = dataMulta;
            ValorMulta = valorMulta;
            TipoJurosMora = tipoJurosMora;
            DataJurosMora = dataJurosMora;
            ValorJurosMora = valorJurosMora;
            NumeroParcela = numeroParcela;
            Aceite = aceite;
            CodigoNegativacao = codigoNegativacao;
            NumeroDiasNegativacao = numeroDiasNegativacao;
            CodigoProtesto = codigoProtesto;
            NumeroDiasProtesto = numeroDiasProtesto;
            Pagador = pagador;
            BeneficiarioFinal = beneficiarioFinal;
            MensagensInstrucao = mensagensInstrucao;
            GerarPdf = gerarPdf;
            RateioCreditos = rateioCreditos;
            CodigoCadastrarPIX = codigoCadastrarPIX;
            NumeroContratoCobranca = numeroContratoCobranca;
        }
       

        Dictionary<int, string> CodigoProtestoOptions = new() { { 1, "Protestar Dias Corridos" }, { 2, "Protestar Dias Úteis" }, {3, "Não Protestar" } };
        Dictionary<int, string> CodigoNegativacaoOptions = new() { { 2, "Negativar Dias Úteis" }, {3, "Não Negativar" } };
        Dictionary<int, string> TipoJurosMoraOptions = new() { {1, "Valor por dia" }, { 2, "Taxa Mensal" }, {3, "Isento" } };
        Dictionary<int, string> TipoMultaOptions = new() { {0, "Isento" }, { 1, "Valor Fixo" }, {2, "Percentual" } };
        Dictionary<int, string> ModalidadeBoletoOptions = new() { { 1, "SIMPLES COM REGISTRO" } };        
        Dictionary<int, string> CodigoCadastrarPixOptions = new() { { 0, "Padrão" }, {1 , "Com Pix" }, { 2, "Sem Pix"} };
            

    }

}
