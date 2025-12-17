using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Inter.Entities;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request
{
    public  class EmitirBoletoInterRequestDto : InterBaseRequestDto, RequestBase
    {     
        public string XContaCorrente { get; set; }// header parameter

        public EmitirBoletoInterRequestBody RequestDto { get; set; }


        public bool IsValid()
        {
            DateOnly DataAtual = DateOnly.FromDateTime(new DateTime());

            OLS.LibCore.Validate.ValidationResult ListErrosValidacao = new();


            if (RequestDto.DataVencimento < DataAtual)
            {
                ListErrosValidacao.AddMensagem("Data de vencimento da cobranca deve ser data futura.");
            }



            if (!Enum.IsDefined(typeof(TipoPessoa), RequestDto.Pagador.TipoPessoa))
            {
                string options = string.Join(", ", Enum.GetValues<TipoPessoa>());

                ListErrosValidacao.AddMensagem($"Valor Inválido para tipo Pessoa. Valores aceitos: {options}");

            }

            if (!Enum.IsDefined(typeof(UfBrasil), RequestDto.Pagador.Uf))
            {
                string options = string.Join(", ", Enum.GetValues<UfBrasil>());

                ListErrosValidacao.AddMensagem($"Valor Inválido para UF do pagador. Valores aceitos: {options}");

            }


            if (RequestDto.Mensagem is not null)
            {

                if (RequestDto.Mensagem.Length > 5)
                {

                    ListErrosValidacao.AddMensagem("São permitidas apenas 5 mensagens de instrução.");

                }
                else
                {
                    foreach (string msg in RequestDto.Mensagem)
                    {
                        if (msg.Length > 78)
                        {
                            ListErrosValidacao.AddMensagem("As mensagens de instrução devem ter no máximo 78 caracteres.");
                        }
                    }
                }
            }

            if (RequestDto.Mora is not null)
            {
                if (RequestDto.Mora.Codigo.Equals(MoraCodigosEnum.VALORDIA))
                {
                    if (RequestDto.Mora.Valor is null)
                    {
                        ListErrosValidacao.AddMensagem("Necessário informar o valor de juros mora por dia.");
                    }
                }
                else if (RequestDto.Mora.Codigo.Equals(MoraCodigosEnum.TAXAMENSAL))
                {
                    if (RequestDto.Mora.Taxa is null)
                    {
                        ListErrosValidacao.AddMensagem("Necessário informar a taxa mensal de juros mora.");
                    }
                }
            }

            if (RequestDto.Multa is not null)
            {

                if (RequestDto.Multa.Codigo.Equals(MultaCodigosEnum.VALORFIXO))
                {

                    if (RequestDto.Multa.Valor is null)
                    {
                        ListErrosValidacao.AddMensagem("Necessário informar campo valorMulta.");
                    }

                }
                else if (RequestDto.Multa.Codigo.Equals(MultaCodigosEnum.PERCENTUAL))
                {

                    if (RequestDto.Multa.Taxa is null)
                    {
                        ListErrosValidacao.AddMensagem("Necessário informar o campo taxaMulta.");
                    }

                }
                else
                {
                    ListErrosValidacao.AddMensagem("Tipo de multa inválido. Valores esperados: VALORFIXO, PERCENTUAL");
                }

            }

            if (RequestDto.Desconto is not null)
            {
                if (RequestDto.Desconto.Codigo.Equals(TipoDesconto.VALORFIXODATAINFORMADA))
                {

                    if (RequestDto.Desconto.Valor is null)
                    {
                        ListErrosValidacao.AddMensagem("Necessário informar o campo valorDesconto.");
                    }

                }
                else if (RequestDto.Desconto.Codigo.Equals(TipoDesconto.PERCENTUALDATAINFORMADA))
                {

                    if (RequestDto.Desconto.Taxa is null)
                    {
                        ListErrosValidacao.AddMensagem("Necessário informar o campo taxaDesconto.");
                    }

                }
                else
                {
                    ListErrosValidacao.AddMensagem("Tipo de desconto inválido. Valores esperados: PERCENTUALDATAINFORMADA, VALORFIXODATAINFORMADA ");
                }
            }

            if (!ListErrosValidacao.IsValid)
            {
                Console.WriteLine(" Erros na validação do IncluirBoletoSicoobRequestDto");
                throw new Exception(ListErrosValidacao.Message);
            }


            return ListErrosValidacao.IsValid;
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
       
    }

}
