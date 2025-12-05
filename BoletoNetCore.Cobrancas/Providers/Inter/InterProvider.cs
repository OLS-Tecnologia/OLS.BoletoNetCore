using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Inter.Mappers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Inter
{
    internal class InterProvider : IBaseProviderSevice
    {
        public bool SuportaCnab { get; set; } = false;
        public bool SuportaApi { get; set; } = true;
       

        public async Task<BaseProviderGerarBoletoResponseDto> EmitirBoleto(BaseProviderGerarBoletoRequestDto request)
        {

            try
            {
                //  Mapear a requisição genérica para o Inter
                var requestInter = EmitirBoletoInterRequestMapper.ToInter(request);

                //TODO: Pegar resultado da requisição e bater na rota Recuperar cobrança para trazer os dados
                EmitirBoletoInterResponseDto response = null;

                string codigo = response?.CodigoSolicitacao;

                // TODO: Mapear a resposta da requisição para a genérica


                await Task.CompletedTask;
                return null;

            }
            catch (Exception ex) {
                // logar erro
                throw;
            }
        }

        public void AlterarDataVencimentoBoleto()
        {

            Console.WriteLine(SuportaApi);
            throw new NotImplementedException();
        }

        public void AlterarValorBoleto()
        {
            throw new NotImplementedException();
        }

        public void BaixarBoleto()
        {
            throw new NotImplementedException();
        }

        public void CancelarBoleto()
        {
            throw new NotImplementedException();
        }

        public void ConsultaBaixaBoleto()
        {
            throw new NotImplementedException();
        }
    }
}
