using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Base;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Testes.ApiCobrancas.Makers.Sicoob
{
    public  class CobrancaSicoobMaker
    {
        public static EmitirBoletoSicoobResquetDto MakeCobranca(PagadorSicoob pagador, string ClientId)
        {
            var beneficiario = new BeneficiarioFinalSicoob()
            {
                Nome = "Paulo",
                NumeroCpfCnpj = "43417424267"
            };

            var boletoBody = new IncluirBoletoSicoobRequestBody()
            {
                SeuNumero = "3243546",
                CodigoModalidade = 1,
                BeneficiarioFinal = beneficiario,
                Pagador = pagador,
                TipoDesconto = (int)TipoDesconto.SEMDESCONTO,
                TipoJurosMora = (int)TipoJurosMoraSicoob.ISENTO,
                TipoMulta = (int)TipoMultaSicoob.ISENTO,
                CodigoEspecieDocumento = Enum.GetName(CodigoEspecieDocumentosEnum.DM),
                DataEmissao = DateOnly.FromDateTime(DateTime.Today),
                DataVencimento = new DateOnly(2025, 12, 30),
                NumeroCliente = 25546454,
                NumeroParcela = 1,
                IdentificacaoDistribuicaoBoleto = 1,
                IdentificacaoEmissaoBoleto = 1,
                Valor = 500,
                NumeroContaCorrente = 12344

            };


            return new EmitirBoletoSicoobResquetDto()
            {
                ClienteId = ClientId,
                Boleto = boletoBody
            };
        }
    }
}
