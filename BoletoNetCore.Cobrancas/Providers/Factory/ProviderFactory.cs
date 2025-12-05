
using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Inter;
using BoletoNetCore.Cobrancas.Providers.Sicoob;


namespace BoletoNetCore.Cobrancas.Providers.Factory
{
    public  class ProviderFactory
    {    

        public static IBaseProviderSevice GetProvider(ProviderTypeEnum providerType)
        {

            switch (providerType) {

                case ProviderTypeEnum.SICOOB:
                    return new SicoobProvider();
                
                case ProviderTypeEnum.INTER:
                    return new InterProvider();
                   
                default: throw new ArgumentException("Tipo de provedor inválido. Provedores aceitos: INTER, SICOOB.");
                    
            }

        }

    }
}
