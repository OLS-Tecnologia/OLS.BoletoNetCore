using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Interfaces;
using BoletoNetCore.Cobrancas.Providers.Factory;
using BoletoNetCore.Cobrancas.Providers.Inter;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;


namespace BoletoNetCore.Cobrancas.Services
{
    public class EmissaoBoletoService
    {

        // Arquivo teste temporário
        public async Task<IResponseDto> EmitirBoletoService(IRequestDto request) 
        {
            try
            {
                var provider = ProviderFactory.GetProvider(Bancos.BancoInter);

                EmitirBoletoInterRequestDto interRequest = null;
                EmitirBoletoInterResponseDto interResponse = null;

                var result = await provider.EmitirBoleto(interRequest);               

                if (result is not null)
                {
                    Console.WriteLine(result);  // validation result   
                }


                return result;
            }
            catch (Exception ex) {
                // Exibir a mensagem no form de erros
                throw;
            
            }

        }

        
    }
}
