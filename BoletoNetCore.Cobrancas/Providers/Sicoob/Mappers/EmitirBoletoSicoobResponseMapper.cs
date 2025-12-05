using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Mappers
{
    public class EmitirBoletoSicoobResponseMapper
    {
        public static BaseProviderGerarBoletoResponseDto  ToBaseProvider(IncluirBoletoSicoobResponseDto response)
        {

            try
            {
                return new BaseProviderGerarBoletoResponseDto()
                {
                    NumeroCliente = response.Resultado.NumeroCliente,
                    CodigoModalidade = response.Resultado.CodigoModalidade,
                    NumeroContaCorrente = response.Resultado.NumeroContaCorrente,
                    CodigoEspecieDocumento = response.Resultado.CodigoEspecieDocumento,
                    DataEmissao = response.Resultado.DataEmissao,
                    NossoNumero = response.Resultado.NossoNumero,
                    SeuNumero = response.Resultado.SeuNumero,
                    IdentificacaoBoletoEmpresa = response.Resultado.IdentificacaoBoletoEmpresa,
                    CodigoBarras = response.Resultado.CodigoBarras,
                    LinhaDigitavel = response.Resultado.LinhaDigitavel,
                    IdentificacaoEmissaoBoleto = response.Resultado.IdentificacaoEmissaoBoleto,
                    IdentificacaoDistribuicaoBoleto = response.Resultado.IdentificacaoDistribuicaoBoleto,
                    Valor = response.Resultado.Valor,
                    DataVencimento = response.Resultado.DataVencimento,
                    DataLimitePagamento = response.Resultado.DataLimitePagamento,
                    ValorAbatimento = response.Resultado.ValorAbatimento,
                    TipoDesconto = response.Resultado.TipoDesconto,
                    DataPrimeiroDesconto = response.Resultado.DataPrimeiroDesconto,
                    ValorPrimeiroDesconto = response.Resultado.ValorPrimeiroDesconto,
                    DataSegundoDesconto = response.Resultado.DataSegundoDesconto,
                    ValorSegundoDesconto = response.Resultado.ValorSegundoDesconto,
                    DataTerceiroDesconto = response.Resultado.DataTerceiroDesconto,
                    ValorTerceiroDesconto = response.Resultado.ValorTerceiroDesconto,
                    TipoMulta = response.Resultado.TipoMulta,
                    DataMulta = response.Resultado.DataMulta,
                    ValorMulta = response.Resultado.ValorMulta,
                    TipoJurosMora = response.Resultado.TipoJurosMora,
                    DataJurosMora = response.Resultado.DataJurosMora,
                    ValorJurosMora = response.Resultado.ValorJurosMora,
                    NumeroParcela = response.Resultado.NumeroParcela,
                    Aceite = response.Resultado.Aceite,
                    CodigoNegativacao = response.Resultado.CodigoNegativacao,
                    NumeroDiasNegativacao = response.Resultado.NumeroDiasNegativacao,
                    CodigoProtesto = response.Resultado.CodigoProtesto,
                    NumeroDiasProtesto = response.Resultado.NumeroDiasProtesto,
                    QuantidadeDiasFloat = response.Resultado.QuantidadeDiasFloat,
                    Pagador = response.Resultado.Pagador,
                    BeneficiarioFinal = new Beneficiario()
                    {
                        CpfCnpj =  response?.Resultado?.BeneficiarioFinal?.NumeroCpfCnpj,
                        Nome =  response?.Resultado.BeneficiarioFinal.Nome,
                    },
                    MensagensInstrucao = response?.Resultado.MensagensInstrucao,
                    RateioCreditos = response?.Resultado.RateioCreditos,
                    PdfBoleto = response?.Resultado.PdfBoleto,
                    QrCode = response?.Resultado.QrCode,
                    NumeroContratoCobranca = response?.Resultado.NumeroContratoCobranca,
                    DescricaoRejeicaoPix = response?.Resultado.DescricaoRejeicaoPix,
                    StatusBoleto = StatusBoletoEnum.A_RECEBER

                };


               
            }
            catch (Exception ex) {
                // logar erro no mapper 

                throw;
            
            }

        }
    }
}
