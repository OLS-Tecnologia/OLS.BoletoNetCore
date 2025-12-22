using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Base;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Enums;
using Microsoft.Extensions.Options;
using OLS.LibCore.Validate;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request
{
    public class EmitirBoletoSicoobResquetDto : RequestBase
    {
        [JsonPropertyName("client_id")]
        [Required]
        public string ClienteId { get; set; }

        [JsonPropertyName("boleto")]
        [Required]
        public IncluirBoletoSicoobRequestBody Boleto { get; set; }

        public bool IsValid()
        {

            OLS.LibCore.Validate.ValidationResult listErros = new();

            DateOnly DataAtual = DateOnly.FromDateTime(DateTime.Today);

            // Campos obrigatórios

            if(Boleto.NumeroCliente == 0)
            {
                listErros.AddMensagem("Necessário informar o campo número cliente.");

            }

            if(Boleto.NumeroContaCorrente == 0)
            {
                listErros.AddMensagem("Necessário informar o campo número conta corrente.");

            }

            if(Boleto.SeuNumero == string.Empty)
            {
                listErros.AddMensagem("Necessário informar o campo seu número.");

            }

            if(Boleto.IdentificacaoEmissaoBoleto == 0)
            {
                listErros.AddMensagem("Necessário informar o campo codigo de identificação emissão do boleto.  Valores esperados: 1- Banco emite; 2 - Cliente emite");

            }
            if(Boleto.IdentificacaoDistribuicaoBoleto == 0)
            {
                listErros.AddMensagem("Necessário informar o campo codigo de distribuição emissão do boleto. Valores esperados: 1- Banco Distribui; 2 - Cliente distribui");

            }


            if (Boleto.DataVencimento < DataAtual)
            {
                listErros.AddMensagem("Data de vencimento da cobranca não pode ser menor que a data atual.");
            }


            if (!Enum.IsDefined(typeof(TipoMultaSicoob), Boleto.TipoMulta))
            {
                string options = string.Join(", ", Enum.GetValues<TipoMultaSicoob>());

                listErros.AddMensagem($"Valor Inválido para tipo de multa do boleto. Valores aceitos: {options}");

            }

            if (!Enum.IsDefined(typeof(TipoJurosMoraSicoob), Boleto.TipoJurosMora))
            {
                string options = string.Join(", ", Enum.GetValues<TipoJurosMoraSicoob>());

                listErros.AddMensagem($"Valor Inválido para TipoJurosMora do boleto. Valores aceitos: {options}");

            }
            if (!Enum.IsDefined(typeof(TipoDesconto), Boleto.TipoDesconto))
            {
                string options = string.Join(", ", Enum.GetValues<TipoDesconto>());

                listErros.AddMensagem($"Valor Inválido para Tipo desconto do boleto. Valores aceitos: {options}");

            }




            if (Boleto.CodigoNegativacao is not null & Boleto.CodigoNegativacao == (int)CodigoNegativacaoBoletoSicoob.NEGATIVAR_DIAS_UTEIS)
            {
                if (Boleto.NumeroDiasNegativacao is null)
                {
                    listErros.AddMensagem("Necessário informar o número de  dias para negativação do boleto. ");

                }

                if (!Enum.IsDefined(typeof(CodigoNegativacaoBoletoSicoob), Boleto.CodigoNegativacao))
                {
                    string options = string.Join(", ", Enum.GetValues<CodigoNegativacaoBoletoSicoob>());

                    listErros.AddMensagem($"Valor Inválido para codigo de negativação do boleto. Valores aceitos: {options}");

                }

            }

            if (Boleto.CodigoProtesto is not null & Boleto.CodigoProtesto == (int)TipoProtestoSicoob.PROTESTAR_DIAS_CORRIDOS)
            {
                if (Boleto.NumeroDiasProtesto is null)
                {
                    listErros.AddMensagem("Necessário informar o número de dias para protesto do boleto. ");

                }

                if (!Enum.IsDefined(typeof(TipoProtestoSicoob), Boleto.CodigoProtesto))
                {
                    string options = string.Join(", ", Enum.GetValues<TipoProtestoSicoob>());

                    listErros.AddMensagem($"Valor Inválido para tipo Protesto do boleto. Valores aceitos: {options}");

                }
            }


            // Valor da multa. Deve ser preenchido caso o campo dataMulta seja preenchido.
            if (Boleto.DataMulta is not null)
            {
                if (Boleto.ValorMulta is null)
                {
                    listErros.AddMensagem("Campo data multa foi preenchido, o valor da multa deve ser informado.");
                }

            }

            // Data Juros mora - Deve ser maior que a data de vencimento do boleto e menor ou igual que data limite de pagamento.
            if (Boleto.DataJurosMora is not null)
            {
                if (Boleto.DataJurosMora < Boleto.DataVencimento)
                {
                    listErros.AddMensagem("Campo DataJurosMora deve ser maior que a data de vencimento do boleto.");

                }

                if (Boleto.DataLimitePagamento is not null)
                {
                    if (Boleto.DataJurosMora > Boleto.DataLimitePagamento)
                    {
                        listErros.AddMensagem("Campo DataJurosMora não pode ser maior que a data limite de pagamento do boleto.");
                    }
                }
                // validar valor juros mora               
                if (Boleto.ValorJurosMora is null)
                {
                    listErros.AddMensagem("Campo DataJurosMora foi preenchido, obrigatório informar o valor juros mora");

                }

            }


            if (Boleto.DataPrimeiroDesconto is not null)
            {
                if (Boleto.ValorPrimeiroDesconto is null)
                {
                    listErros.AddMensagem("Obrigatório informar valor do primeiro desconto.");

                }
            }

            if (Boleto.DataSegundoDesconto is not null)
            {
                if (Boleto.ValorSegundoDesconto is null)
                {
                    listErros.AddMensagem("Obrigatório informar valor do segundo desconto.");

                }
            }

            if (Boleto.DataTerceiroDesconto is not null)
            {
                if (Boleto.ValorTerceiroDesconto is null)
                {
                    listErros.AddMensagem("Obrigatório informar valor do terceiro desconto.");

                }
            }

            if (Boleto.MensagensInstrucao is not null)
            {

                if (Boleto.MensagensInstrucao.Count > 5)
                {

                    listErros.AddMensagem("São permitidas apenas 5 mensagens de instrução.");

                }
                else
                {
                    foreach (string msg in Boleto.MensagensInstrucao)
                    {
                        if (msg.Length > 40)
                        {
                            listErros.AddMensagem("As mensagens de instrução devem ter no máximo 40 caracteres.");
                        }
                    }

                }

            }

            if (!Enum.IsDefined(typeof(ModalidadeBoletoSicoob), Boleto.CodigoModalidade))
            {
                string options = string.Join(", ", Enum.GetValues<ModalidadeBoletoSicoob>());

                listErros.AddMensagem($"Valor Inválido para modalidade do boleto. Valores aceitos: {options}");

            }          

           


            if (!listErros.IsValid)
            {
                Console.WriteLine(" Erros na validação do IncluirBoletoSicoobRequestDto");
                throw new Exception(listErros.Message);
            }

            return listErros.IsValid;
        }
    }  


    public class IncluirBoletoSicoobRequestBody
    {
        [JsonPropertyName("numeroCliente")]
        [Required]
        public int NumeroCliente { get; set; }

        [JsonPropertyName("codigoModalidade")]
        [Required]
        public int CodigoModalidade { get; set; } 

        [JsonPropertyName("numeroContaCorrente")]
        [Required]
        public int NumeroContaCorrente { get; set; }

        [JsonPropertyName("codigoEspecieDocumento")]
        [Required]
        public string CodigoEspecieDocumento { get; set; }

        [JsonPropertyName("dataEmissao")]
        [Required]
        public DateOnly DataEmissao { get; set; }

        [JsonPropertyName("nossoNumero")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? NossoNumero { get; set; }

        [JsonPropertyName("seuNumero")]
        [Required]
        public string SeuNumero { get; set; }
       

        [JsonPropertyName("identificacaoEmissaoBoleto")]
        [Required]
        public int IdentificacaoEmissaoBoleto { get; set; }

        [JsonPropertyName("identificacaoDistribuicaoBoleto")]
        [Required]
        public int IdentificacaoDistribuicaoBoleto { get; set; }

        [JsonPropertyName("valor")]
        [Required]
        public double Valor { get; set; }

        [JsonPropertyName("dataVencimento")]
        [Required]
        public DateOnly DataVencimento { get; set; }

        [JsonPropertyName("identificacaoBoletoEmpresa")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? IdentificacaoBoletoEmpresa { get; set; }

        [JsonPropertyName("dataLimitePagamento")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateOnly? DataLimitePagamento { get; set; }

        [JsonPropertyName("valorAbatimento")]
          [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? ValorAbatimento { get; set; }

        [JsonPropertyName("tipoDesconto")]
        [Required]
        public int TipoDesconto { get; set; }

        [JsonPropertyName("dataPrimeiroDesconto")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateOnly? DataPrimeiroDesconto { get; set; }

        [JsonPropertyName("valorPrimeiroDesconto")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? ValorPrimeiroDesconto { get; set; }

        [JsonPropertyName("dataSegundoDesconto")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateOnly? DataSegundoDesconto { get; set; }

        [JsonPropertyName("valorSegundoDesconto")]
         [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? ValorSegundoDesconto { get; set; }

        [JsonPropertyName("dataTerceiroDesconto")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateOnly? DataTerceiroDesconto { get; set; }

        [JsonPropertyName("valorTerceiroDesconto")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double?  ValorTerceiroDesconto { get; set; }

       
        [JsonPropertyName("tipoMulta")]
        [Required]
        public int TipoMulta { get; set; }
      
        [JsonPropertyName("dataMulta")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateOnly? DataMulta { get; set; }

        [JsonPropertyName("valorMulta")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? ValorMulta { get; set; }

       
        [JsonPropertyName("tipoJurosMora")]
        [Required]
        public int TipoJurosMora { get; set; }

       
        [JsonPropertyName("dataJurosMora")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateOnly? DataJurosMora { get; set; }

        [JsonPropertyName("valorJurosMora")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? ValorJurosMora { get; set; }

        [JsonPropertyName("numeroParcela")]
        [Required]
        public int NumeroParcela { get; set; }

        [JsonPropertyName("aceite")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Aceite { get; set; }

        [JsonPropertyName("codigoNegativacao")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CodigoNegativacao { get; set; }

        [JsonPropertyName("numeroDiasNegativacao")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? NumeroDiasNegativacao { get; set; }

       
        [JsonPropertyName("codigoProtesto")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CodigoProtesto { get; set; }

        [JsonPropertyName("numeroDiasProtesto")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? NumeroDiasProtesto { get; set; }

        [JsonPropertyName("pagador")]
        [Required]
        public PagadorSicoob Pagador { get; set; }

        [JsonPropertyName("beneficiarioFinal")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BeneficiarioFinalSicoob? BeneficiarioFinal { get; set; }

        [JsonPropertyName("mensagensInstrucao")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? MensagensInstrucao { get; set; }

        [JsonPropertyName("gerarPdf")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? GerarPdf { get; set; }

        [JsonPropertyName("rateioCreditos")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<RateioCredito>? RateioCreditos { get; set; }

        [JsonPropertyName("codigoCadastrarPIX")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CodigoCadastrarPIX { get; set; }

        [JsonPropertyName("numeroContratoCobranca")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? NumeroContratoCobranca { get; set; }        

       

    }

}
