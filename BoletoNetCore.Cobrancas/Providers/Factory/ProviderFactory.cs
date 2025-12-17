
using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.Inter;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Sicoob;

namespace BoletoNetCore.Cobrancas.Providers.Factory
{
    public class ProviderFactory 
    {     
        public static IProviderBoleto GetProvider(Bancos provider, string providerApiUrl, string? tokenSandBox= null)
        {
            switch (provider)
            {
                case Bancos.BancoInter:
                    return new InterProvider(providerApiUrl);
                case Bancos.Sicoob:
                    return new SicoobProvider(providerApiUrl, tokenSandBox);
                default: throw new ArgumentException("Provedor não disponível");
            }

        }
    }
}
