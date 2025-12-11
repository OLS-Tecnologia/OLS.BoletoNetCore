using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Interfaces;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request
{
    public  class EmitirBoletoInterRequestDto : InterBaseRequestDto
    {
        public string XContaCorrente { get; set; }// header parameter

        public EmitirBoletoInterRequestBody RequestDto { get; set; }

    }

    public class EmitirBoletoInterRequestBody
    {
        [JsonProperty("seuNumero")]
        [Required]
        [StringLength(15, ErrorMessage = "Tamanho máximo para o campo seuNumero é de 15 caracteres")]
        public string SeuNumero { get; set; }

        [JsonProperty("valorNominal")]
        [Required]
        public double ValorNominal { get; set; }

        [JsonProperty("dataVencimento")]
        [Required]
        public DateOnly DataVencimento { get; set; }

        [JsonProperty("numDiasAgenda")]
        [Required]
        [Range(0, 61, ErrorMessage = "Dias para cancelamento da cobrança após o vencimento deve estar no intervalo entre 0 até 60 dias.")]
        public int NumDiasAgenda { get; set; }

        [JsonProperty("pagador")]
        [Required]
        public Pagador Pagador { get; set; }

        [JsonProperty("desconto")]
        public Desconto? Desconto { get; set; }

        [JsonProperty("multa")]
        public Multa? Multa { get; set; }


        [JsonProperty("mora")]
        public Mora? Mora { get; set; }

        [JsonProperty("mensagem")]
        public string[]? Mensagem { get; set; } = new string[5];


        [JsonProperty("beneficiarioFinal")]
        public BeneficiarioFinal? BeneficiarioFinal { get; set; }

        [JsonProperty("formasRecebimento")]
        public List<string>? FormasRecebimento { get; set; }  //  [ "BOLETO", "PIX" ]

        public EmitirBoletoInterRequestBody() { }
        public EmitirBoletoInterRequestBody(string seuNumero, double valorNominal, DateOnly dataVencimento, int numDiasAgenda,
            Pagador pagador, Desconto? desconto = null, Multa? multa = null, Mora? mora = null, string[]? mensagem = null,
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



            if (mora is not null)
            {

                if (mora.Codigo.Equals(MoraCodigosEnum.ISENTO))
                {
                    mora = null;

                }
                else
                {

                    if (mora.Codigo.Equals(MoraCodigosEnum.VALORDIA))
                    {

                        if (mora.Valor is null)
                        {
                            ListErrosValidacao.Add("Necessário informar o valor de juros mora por dia.");
                        }

                    }
                    else if (mora.Codigo.Equals(MoraCodigosEnum.TAXAMENSAL))
                    {

                        if (mora.Taxa is null)
                        {
                            ListErrosValidacao.Add("Necessário informar a taxa mensal de juros mora.");
                        }

                    }

                }

            }

            if (multa is not null)
            {

                if (multa.Codigo.Equals(MultaCodigosEnum.ISENTO))
                {
                    multa = null;

                }
                else
                {

                    if (multa.Codigo.Equals(MultaCodigosEnum.VALORFIXO))
                    {

                        if (multa.Valor is null)
                        {
                            ListErrosValidacao.Add("Necessário informar campo valorMulta.");
                        }

                    }
                    else if (multa.Codigo.Equals(MultaCodigosEnum.PERCENTUAL))
                    {

                        if (multa.Taxa is null)
                        {
                            ListErrosValidacao.Add("Necessário informar o campo taxaMulta.");
                        }

                    }

                }

            }


            if (desconto is not null)
            {

                if (desconto.Codigo.Equals(TipoDesconto.SEMDESCONTO))
                {
                    desconto = null;

                }
                else
                {

                    if (desconto.Codigo.Equals(TipoDesconto.VALORFIXODATAINFORMADA))
                    {

                        if (desconto.Valor is null)
                        {
                            ListErrosValidacao.Add("Necessário informar o campo valorDesconto.");
                        }

                    }
                    else if (desconto.Codigo.Equals(TipoDesconto.PERCENTUALDATAINFORMADA))
                    {

                        if (desconto.Taxa is null)
                        {
                            ListErrosValidacao.Add("Necessário informar o campo taxaDesconto.");
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
        [JsonProperty("valor")]
        public double? Valor { get; set; }

        [JsonProperty("taxa")]
        public double? Taxa { get; set; }

        [JsonProperty("codigo")]
        [Required]
        public MoraCodigosEnum Codigo { get; set; }   


    }

    public class Multa
    {
        [JsonProperty("valor")]
        public double? Valor { get; set; }

        [JsonProperty("taxa")]
        public double? Taxa { get; set; }

        [JsonProperty("codigo")]
        [Required]
        public MultaCodigosEnum Codigo { get; set; }

      
    }

    public class Desconto
    {
        [JsonProperty("valor")]
        public double? Valor { get; set; } = 1;

        [JsonProperty("taxa")]
        public double? Taxa { get; set; } = 1;

        [JsonProperty("codigo")]
        [Required]
        public TipoDesconto Codigo { get; set; }

        [JsonProperty("quantidadeDias")]
        [Required]
        public int? QuantidadeDias { get; set; } = 1; // Quantidade de dias antes do vencimento que será aplicado o desconto.


    }

}
