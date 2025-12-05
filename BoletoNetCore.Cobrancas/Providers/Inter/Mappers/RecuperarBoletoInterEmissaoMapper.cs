using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Mappers
{
    public class RecuperarBoletoInterEmissaoMapper
    {

        public static BaseProviderGerarBoletoResponseDto ToBaseProvider(RecuperarCobrancaInterResponse response, int numeroClient)
        {

            try
            {
                return new BaseProviderGerarBoletoResponseDto()
                {
                    NumeroCliente = numeroClient,
                    CodigoModalidade = response. ,
                    NumeroContaCorrente = response. ,
                    CodigoEspecieDocumento = response. ,
                    DataEmissao = response.Cobranca.DataEmissao ,
                    NossoNumero = response.Boleto.NossoNumero,
                    SeuNumero = response.Cobranca.SeuNumero ,
                    IdentificacaoBoletoEmpresa = response. ,
                    CodigoBarras = response.Boleto.CodigoBarras ,
                    LinhaDigitavel = response.Boleto.LinhaDigitavel ,
                    IdentificacaoEmissaoBoleto = response. ,
                    IdentificacaoDistribuicaoBoleto = response. ,
                    Valor = response.Cobranca.ValorNominal ,
                    DataVencimento = response.Cobranca.DataVencimento ,
                    DataLimitePagamento = response. ,
                    ValorAbatimento = response. ,
                    TipoDesconto = response. ,
                    DataPrimeiroDesconto = response. ,
                    ValorPrimeiroDesconto = response. ,
                    DataSegundoDesconto = response. ,
                    ValorSegundoDesconto = response. ,
                    DataTerceiroDesconto = response. ,
                    ValorTerceiroDesconto = response. ,
                    TipoMulta = response.Cobranca.Multa.Codigo , //TODO mapear o tipo aqui
                    DataMulta = response. ,
                    ValorMulta = response.Cobranca.Multa.Valor,
                    TipoJurosMora = response.Cobranca.Mora.Codigo, //TODO Mapear aqui
                    DataJurosMora = response. ,
                    ValorJurosMora = response. ,
                    NumeroParcela = response. ,
                    Aceite = response. ,
                    CodigoNegativacao = response. ,
                    NumeroDiasNegativacao = response. ,
                    CodigoProtesto = response. ,
                    NumeroDiasProtesto = response. ,
                    QuantidadeDiasFloat = response. ,
                    Pagador = response. ,
                    BeneficiarioFinal = response. ,
                    MensagensInstrucao = response. ,
                    RateioCreditos = response. ,
                    PdfBoleto = response. ,
                    QrCode = response.Pix?.PixCopiaECola ,
                    NumeroContratoCobranca = response.Cobranca.CodigoSolicitacao ,
                    DescricaoRejeicaoPix = response. ,
                    StatusBoleto = response. ,

                };

            }
            catch (Exception ex) {
                // logar erro

                throw;
            }

        }
    }
}
