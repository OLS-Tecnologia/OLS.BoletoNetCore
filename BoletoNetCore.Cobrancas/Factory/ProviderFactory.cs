using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter;
using BoletoNetCore.Cobrancas.Providers.Sicoob;

namespace BoletoNetCore.Cobrancas.Factory
{
    public static class ProviderFactory
    {
        public static IProviderEmitirBoleto<TReq> EmitirBoleto<TReq>(
           Bancos banco,
           string apiUrl,
           string? tokenSandbox = null) where TReq : RequestBase
        {
            return banco switch
            {
                Bancos.BancoInter =>
                    (IProviderEmitirBoleto<TReq>)new InterProvider(apiUrl),

                Bancos.Sicoob =>
                    (IProviderEmitirBoleto<TReq>)new SicoobProvider(apiUrl, tokenSandbox),

                _ => throw new ArgumentException("Banco não suportado")
            };
        }


        public static IProviderBaixarBoleto<TReq> BaixarBoleto<TReq>(
           Bancos banco,
           string apiUrl,
           string? tokenSandbox = null)  where TReq : RequestBase
        {
            return banco switch
            {
                Bancos.BancoInter =>
                    (IProviderBaixarBoleto<TReq>)new InterProvider(apiUrl),

                Bancos.Sicoob =>
                    (IProviderBaixarBoleto<TReq>)new SicoobProvider(apiUrl, tokenSandbox),

                _ => throw new ArgumentException("Banco não suportado")
            };
        }

        public static IProviderATualizarBoleto<TReq> AtualizarBoleto<TReq>(
           Bancos banco,
           string apiUrl,
           string? tokenSandbox = null)  where TReq : RequestBase
        {
            return banco switch
            {
                Bancos.BancoInter =>
                    (IProviderATualizarBoleto<TReq>)new InterProvider(apiUrl),

                Bancos.Sicoob =>
                    (IProviderATualizarBoleto<TReq>)new SicoobProvider(apiUrl, tokenSandbox),

                _ => throw new ArgumentException("Banco não suportado")
            };
        }

        public static IProviderConsultaBoleto<TReq> ConsultarBoleto<TReq>(
           Bancos banco,
           string apiUrl,
           string? tokenSandbox = null)  where TReq : RequestBase
        {
            return banco switch
            {
                Bancos.BancoInter =>
                    (IProviderConsultaBoleto<TReq>)new InterProvider(apiUrl),

                Bancos.Sicoob =>
                    (IProviderConsultaBoleto<TReq>)new SicoobProvider(apiUrl, tokenSandbox),

                _ => throw new ArgumentException("Banco não suportado")
            };
        }



        public static TProvider GetProvider<TProvider>(
            Bancos banco,
            Func<string, string?, object> creator,
            string apiUrl,
            string? tokenSandbox = null)
            where TProvider : class
        {
            return banco switch
            {
                Bancos.BancoInter => (TProvider)creator(apiUrl, null),
                Bancos.Sicoob => (TProvider)creator(apiUrl, tokenSandbox),
                _ => throw new ArgumentException("Banco não suportado")
            };
        }

        /*
         
           switch (provider)
            {
                case Bancos.BancoInter:
                    return new InterProvider(providerApiUrl);
                case Bancos.Sicoob:
                    return new SicoobProvider(providerApiUrl, tokenSandBox);
                default: throw new ArgumentException("Provedor não disponível");
            }

         */


    }


}
