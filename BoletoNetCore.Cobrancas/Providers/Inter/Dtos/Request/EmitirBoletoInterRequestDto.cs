using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Inter.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request
{
    public  class EmitirBoletoInterRequestDto : InterBaseRequestDto, RequestBase
    {
      
        public string XContaCorrente { get; set; }// header parameter

        public EmitirBoletoInterRequestBody RequestDto { get; set; }

        public EmitirBoletoInterRequestDto(string xContaCorrente, EmitirBoletoInterRequestBody requestDto, string clientId, string clientSecret, string arquivoCertificado, string arquivoChave) 
            : base(clientId, clientSecret, arquivoCertificado, arquivoChave)
        {
            XContaCorrente = xContaCorrente;
            RequestDto = requestDto;
        }


    }

    public class EmitirBoletoInterRequestBody
    {
        [JsonPropertyName("seuNumero")]
        [Required]
        [StringLength(15, ErrorMessage = "Tamanho máximo para o campo seuNumero é de 15 caracteres")]
        public string SeuNumero { get; set; }

        [JsonPropertyName("valorNominal")]
        [Required]
        public double ValorNominal { get; set; }

        [JsonPropertyName("dataVencimento")]
        [Required]
        public DateOnly DataVencimento { get; set; }

        [JsonPropertyName("numDiasAgenda")]
        [Required]
        [Range(0, 61, ErrorMessage = "Dias para cancelamento da cobrança após o vencimento deve estar no intervalo entre 0 até 60 dias.")]
        public int NumDiasAgenda { get; set; }

        [JsonPropertyName("pagador")]
        [Required]
        public PagadorInter Pagador { get; set; }

        [JsonPropertyName("desconto")]
        public Desconto? Desconto { get; set; }

        [JsonPropertyName("multa")]
        public Multa? Multa { get; set; }


        [JsonPropertyName("mora")]
        public Mora? Mora { get; set; }

        [JsonPropertyName("mensagem")]
        public string[]? Mensagem { get; set; } = new string[5];


        [JsonPropertyName("beneficiarioFinal")]
        public BeneficiarioFinal? BeneficiarioFinal { get; set; }

        [JsonPropertyName("formasRecebimento")]
        public List<string>? FormasRecebimento { get; set; }  //  [ "BOLETO", "PIX" ]
      

        public EmitirBoletoInterRequestBody (string seuNumero, double valorNominal, DateOnly dataVencimento, int numDiasAgenda,
            PagadorInter pagador, Desconto? desconto = null, Multa? multa = null, Mora? mora = null, string[]? mensagem = null,
            BeneficiarioFinal? beneficiarioFinal = null, ModeloBoletoEnum? modeloBoleto = null)
        {
            DateOnly DataAtual = new();

            List<string> ListErrosValidacao = new List<string>();


            if (dataVencimento < DataAtual)
            {
                ListErrosValidacao.Add("Data de vencimento da cobranca deve ser data futura.");
            }

            if (mensagem is not null)
            {

                if (mensagem.Length > 5)
                {

                    ListErrosValidacao.Add("São permitidas apenas 5 mensagens de instrução.");

                }
                else
                {
                    foreach (string msg in mensagem)
                    {
                        if (msg.Length > 78)
                        {
                            ListErrosValidacao.Add("As mensagens de instrução devem ter no máximo 78 caracteres.");
                        }
                    }

                }


            }

                    

            List<string> formasRecebimento = new List<string>();

            if (modeloBoleto is not null)
            {
                if (modeloBoleto.Equals(ModeloBoletoEnum.COM_PIX))
                {
                    formasRecebimento.Add("BOLETO");
                    formasRecebimento.Add("PIX");
                }
                else formasRecebimento.Add("BOLETO");


            }
            else formasRecebimento.Add("BOLETO");


         
            SeuNumero = seuNumero;
            ValorNominal = valorNominal;
            DataVencimento = dataVencimento;
            NumDiasAgenda = numDiasAgenda;
            Pagador = pagador;
            Desconto = desconto;
            Multa = multa;
            Mora = mora;
            Mensagem = mensagem;
            BeneficiarioFinal = beneficiarioFinal;
            FormasRecebimento = formasRecebimento;
        }
    }

   
    public class Mora
    {
        [JsonPropertyName("valor")]
        public double? Valor { get; set; }

        [JsonPropertyName("taxa")]
        public double? Taxa { get; set; }

        [JsonPropertyName("codigo")]
        [Required]
        public MoraCodigosEnum Codigo { get; set; }

        public Mora(double? valor, double? taxa, MoraCodigosEnum codigo)
        {

            List<string> ListErrosValidacaoMora = new List<string>();

            if (codigo.Equals(MoraCodigosEnum.VALORDIA))
            {

                if (valor is null)
                {
                    ListErrosValidacaoMora.Add("Necessário informar o valor de juros mora por dia.");
                }

            }
            else if (codigo.Equals(MoraCodigosEnum.TAXAMENSAL))
            {

                if (taxa is null)
                {
                    ListErrosValidacaoMora.Add("Necessário informar a taxa mensal de juros mora.");
                }

            }

            if (ListErrosValidacaoMora.Count > 0) {

                return;
            }

            Valor = valor;
            Taxa = taxa;
            Codigo = codigo;
        }
    }

    public class Multa
    {
        [JsonPropertyName("valor")]
        public double? Valor { get; set; }

        [JsonPropertyName("taxa")]
        public double? Taxa { get; set; }

        [JsonPropertyName("codigo")]
        [Required]
        public string Codigo { get; set; }

        public Multa(MultaCodigosEnum codigo, double? valor, double? taxa)
        {
            List<string> ListErrosValidacaoMulta = new List<string>();


            if (codigo.Equals(MultaCodigosEnum.VALORFIXO))
            {

                if (valor is null)
                {
                    ListErrosValidacaoMulta.Add("Necessário informar campo valorMulta.");
                }

            }
            else if (codigo.Equals(MultaCodigosEnum.PERCENTUAL))
            {

                if (taxa is null)
                {
                   ListErrosValidacaoMulta.Add("Necessário informar o campo taxaMulta.");
                }

            }
            else
            {
                ListErrosValidacaoMulta.Add("Tipo de multa inválido. Valores esperados: VALORFIXO, PERCENTUAL");
            }


            if (ListErrosValidacaoMulta.Count > 0)
            {

                return;
            }

            Valor = valor;
            Taxa = taxa;
            Codigo = Enum.GetName< MultaCodigosEnum>(codigo) ?? "";
        }
    }

    public class Desconto
    {
        [JsonPropertyName("valor")]
        public double? Valor { get; set; } = 1;

        [JsonPropertyName("taxa")]
        public double? Taxa { get; set; } = 1;

        [JsonPropertyName("codigo")]
        [Required]
        public string Codigo { get; set; } // TipoDesconto

        /// <summary>
        ///     Quantidade de dias antes do vencimento que será aplicado o desconto.
        /// </summary>
        [JsonPropertyName("quantidadeDias")]
        [Required]       
        public int QuantidadeDias { get; set; } = 1; 

        public Desconto(TipoDesconto codigo, int quantidadeDias, double? valor, double? taxa)
        {          
            List<string> ListErrosValidacaoDesconto = new List<string>();

            if (codigo.Equals(TipoDesconto.VALORFIXODATAINFORMADA))
            {

                if (valor is null)
                {
                        ListErrosValidacaoDesconto.Add("Necessário informar o campo valorDesconto.");
                }

            }
            else if (codigo.Equals(TipoDesconto.PERCENTUALDATAINFORMADA))
            {

                if (taxa is null)
                {
                ListErrosValidacaoDesconto.Add("Necessário informar o campo taxaDesconto.");
                }

            }
            else
            {
                ListErrosValidacaoDesconto.Add("Tipo de desconto inválido. Valores esperados: PERCENTUALDATAINFORMADA, VALORFIXODATAINFORMADA ");
            }

            


            if (ListErrosValidacaoDesconto.Count> 0)
            {

                return;
                // retornar validation result com a lista de erros

            }

            Valor = valor;
            Taxa = taxa;
            Codigo = Enum.GetName<TipoDesconto>(codigo) ?? "";
            QuantidadeDias = quantidadeDias;
        }
    }

}
