using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Testes.ApiCobrancas.Makers.Inter
{
    public class EmitirCobrancaMaker
    {

        public static EmitirBoletoInterRequestDto MakeCobrancaInter(PagadorInter pagador, string ClientSecret, string ArquivoCertificado, string ArquivoChave, string ClientId)
        {
            string seuNumero = Random.Shared.Next(100000, 1000000).ToString();

            var body = new EmitirBoletoInterRequestBody()
            {
                SeuNumero = seuNumero,
                ValorNominal = 2.5,
                DataVencimento = new DateOnly(2026, 09, 07),
                NumDiasAgenda = 60,
                Pagador = pagador,
            };


            var interRequest = new EmitirBoletoInterRequestDto()
            {
                RequestDto = body,
                ClientSecret = ClientSecret,
                ArquivoCertificado = ArquivoCertificado,
                ArquivoChave = ArquivoChave,
                ClientId = ClientId,
                XContaCorrente = "1234"

            };

            return interRequest;
        }
    }
}
