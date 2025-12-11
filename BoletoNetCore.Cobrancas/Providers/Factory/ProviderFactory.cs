
using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.Inter;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;

namespace BoletoNetCore.Cobrancas.Providers.Factory
{
    public class ProviderFactory 
    {     
        public static IProviderBoleto<InterBaseRequestDto, InterBaseResponseDto> GetProvider(Bancos provider)
        {
            switch (provider)
            {
                case Bancos.BancoInter:
                    return new InterProvider();
                //case Bancos.Sicoob:
                //    return new SicoobProvider();
                default: throw new ArgumentException("Provedor não disponível");
            }

        }
    }
}
