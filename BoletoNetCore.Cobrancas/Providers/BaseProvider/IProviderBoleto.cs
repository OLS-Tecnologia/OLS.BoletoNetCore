using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using OLS.LibCore.Validate;
using Org.BouncyCastle.Ocsp;
using System.Net;


namespace BoletoNetCore.Cobrancas.Providers.BaseProvider
{     

    public interface IProviderEmitirBoleto<TReq>
    where TReq : RequestBase
    {
        Task<ValidationResult> EmitirBoleto(List<TReq> request);
    }

    public interface IProviderBaixarBoleto<TReq>
    where TReq : RequestBase
    {
        Task<ValidationResult> BaixarBoleto(TReq request);
    }
   

    public interface IProviderATualizarBoleto<TReq>
    where TReq : RequestBase

    {
        Task<ValidationResult> AlterarValorBoleto(TReq request);
        Task<ValidationResult> AlterarDataDeVencimentoBoleto(TReq request);
    }

    public interface IProviderConsultaBoleto<TReq>
     where TReq : RequestBase
    {
        Task<ValidationResult> ConsultaBoleto(TReq request);
    }


}


