using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Factory;


namespace BoletoNetCore.Cobrancas.Services
{
    public class EmissaoBoletoService
    {

        public async Task<BaseProviderGerarBoletoResponseDto> EmitirBoleto(BaseProviderGerarBoletoRequestDto request, ProviderTypeEnum typeProvider)
        {
            try
            {
                IBaseProviderSevice provider = ProviderFactory.GetProvider(typeProvider);               

                var result = await provider.EmitirBoleto(request);
                

                return result;  // validation result              

            }
            catch (Exception ex) {
                // Exibir a mensagem no form de erros
                return null;
            
            }

        }

        
    }
}
