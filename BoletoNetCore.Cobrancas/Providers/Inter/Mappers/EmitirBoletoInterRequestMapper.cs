using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Mappers
{
    public class EmitirBoletoInterRequestMapper
    {
        public static EmitirBoletoInterRequestDto ToInter(BaseProviderGerarBoletoRequestDto request)
        {
            try
            {
                var desconto = new Desconto() { Codigo = request.TipoDesconto, QuantidadeDias = request.QuantidadeDias, Taxa= request.TaxaDesconto, Valor = request.ValorDesconto};
                var multa = new Multa() { Codigo = request.TipoMulta, Taxa= request.TaxaMulta, Valor = request.ValorMulta};
                var mora = new Mora() { Codigo = request.TipoJurosMora, Taxa= request.TaxaJurosMora, Valor = request.ValorJurosMora};

               return new EmitirBoletoInterRequestDto(request.XContaCorrente, request.SeuNumero, request.ValorNominal, request.DataVencimento, request.NumDiasAgenda,
                   request.Pagador, desconto, multa, mora, request.MensagensInstrucao?.ToArray(), request.Beneficiario, request.CodigoCadastrarPIX);

            }
            catch (Exception ex) {

                Console.WriteLine(" Erro no mapper INTER: " + ex.Message);
                throw ;
            }
        }
    }
}
