using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Mappers
{
    public class EmitirBoletoSicoobRequestMapper
    {

        public static IncluirBoletoSicoobResquetDto ToSicoob(BaseProviderGerarBoletoRequestDto request)
        {
            try
            {

                var body = new IncluirBoletoSicoobRequestBody(request.NumeroCliente, request.CodigoModalidade, request.NumeroContaCorrente, request.CodigoEspecieDocumento,
                    request.DataEmissao, request.NossoNumero, request.SeuNumero, request.IdentificacaoBoletoEmpresa, request.IdentificacaoEmissaoBoleto,
                    request.IdentificacaoDistribuicaoBoleto, request.ValorNominal, request.DataVencimento, (int)request.TipoDesconto, (int)request.TipoMulta, 
                    (int)request.TipoJurosMora,request.NumeroParcela, request.Aceite, request.CodigoNegativacao, request.NumeroDiasNegativacao, request.CodigoProtesto,
                    request.NumeroDiasProtesto,  request.Pagador, request.Beneficiario, request.GerarPdf, (int)request.CodigoCadastrarPIX, 
                    request.NumeroContratoCobranca, request.DataMulta, request.ValorMulta, request.DataLimitePagamento, request.DataJurosMora, request.DataPrimeiroDesconto,
                    request.ValorPrimeiroDesconto, request.DataSegundoDesconto, request.ValorSegundoDesconto, request.DataTerceiroDesconto, request.ValorTerceiroDesconto,
                    request.ValorAbatimento, request.MensagensInstrucao, request.RateioCreditos, request.ValorJurosMora)
                {
                  
                    Aceite = request.Aceite,
                    BeneficiarioFinal = { CpfCnpj = request.Beneficiario.NumeroCpfCnpj, Nome = request.Beneficiario.Nome },
                    CodigoCadastrarPIX = (int)request.CodigoCadastrarPIX,
                   
                    CodigoNegativacao = request.CodigoNegativacao,
                    CodigoProtesto = request.CodigoProtesto,
                   
                    GerarPdf = request.GerarPdf,
                    
                   
                    NumeroContratoCobranca = request.NumeroContratoCobranca,
                    NumeroDiasNegativacao = request.NumeroDiasNegativacao, 
                    NumeroDiasProtesto = request.NumeroDiasProtesto,
                    Pagador = request.Pagador,
                  
                    
                    TipoJurosMora = (int)request.TipoJurosMora, 
                    
                    DataJurosMora = request.DataJurosMora, 
                    DataLimitePagamento = request.DataLimitePagamento, 
                    DataMulta = request.DataMulta, 

                    DataPrimeiroDesconto = request.DataPrimeiroDesconto,
                    ValorPrimeiroDesconto = request.ValorPrimeiroDesconto,

                    DataSegundoDesconto = request.DataSegundoDesconto,
                    ValorSegundoDesconto = request.ValorSegundoDesconto,

                    DataTerceiroDesconto = request.DataTerceiroDesconto,
                    ValorTerceiroDesconto = request.ValorTerceiroDesconto, 

                
                    MensagensInstrucao = request.MensagensInstrucao,
                    NumeroParcela = request.NumeroParcela, 
                    RateioCreditos = request.RateioCreditos, 
                  
                    ValorAbatimento = request.ValorAbatimento,
                    ValorJurosMora = request.ValorJurosMora,
                    ValorMulta = request.ValorMulta         
                }; 

                return new IncluirBoletoSicoobResquetDto(request.ClientId, body);

            }
            catch (Exception ex) {

                Console.WriteLine(" Erro no mapper Sicoob: " + ex.Message);
                throw ;
                
            }

        }
    }
}
