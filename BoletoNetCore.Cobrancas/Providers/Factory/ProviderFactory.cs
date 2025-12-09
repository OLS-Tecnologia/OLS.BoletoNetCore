
using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Interfaces;
using BoletoNetCore.Cobrancas.Providers.Inter;
using BoletoNetCore.Cobrancas.Providers.Sicoob;
using Microsoft.Extensions.DependencyInjection;

namespace BoletoNetCore.Cobrancas.Providers.Factory
{
    public class ProviderFactory 
    {     
        public static IProviderBoleto GetProvider(Bancos provider)
        {
            switch (provider)
            {
                case Bancos.BancoInter:
                    return new InterProvider();
                case Bancos.Sicoob:
                    return new SicoobProvider();
                default: throw new ArgumentException("Provedor não disponível");
            }

        }
    }
}
