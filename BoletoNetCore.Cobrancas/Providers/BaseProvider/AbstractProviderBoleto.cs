using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Interfaces;
using Org.BouncyCastle.Ocsp;
using System.Net;


namespace BoletoNetCore.Cobrancas.Providers.BaseProvider
{  
    public interface IProviderBoleto<TReq, TResp> 
    {

        Task<TResp> EmitirBoleto(TReq request);
        Task<HttpStatusCode> BaixarBoleto(TReq request);
        Task<TResp> AlterarDataDeVencimentoBoleto(TReq request);
        Task<TResp> ConsultaBoleto(TReq request);
        Task<TResp> AlterarValorBoleto(TReq request);
        // Task<ResponseBase> EmitirBoleto(RequestBase request);
        // Task<HttpStatusCode> BaixarBoleto(RequestBase request);
        //Task<ResponseBase> AlterarDataDeVencimentoBoleto(RequestBase request);
        // Task<ResponseBase> ConsultaBoleto(RequestBase request);
        // Task<ResponseBase> AlterarValorBoleto(RequestBase request);
    }
}
