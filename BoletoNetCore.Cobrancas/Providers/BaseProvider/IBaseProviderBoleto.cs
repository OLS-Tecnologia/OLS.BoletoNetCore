using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Interfaces;
using Org.BouncyCastle.Ocsp;


namespace BoletoNetCore.Cobrancas.Providers.BaseProvider
{  
    public interface IProviderBoleto
    {        
         Task<IResponseDto> EmitirBoleto(IRequestDto request, BaseProviderEntity? entity);
         Task<IResponseDto> BaixarBoleto(IRequestDto request, BaseProviderEntity? entity);
         Task<IResponseDto> AlterarDataDeVencimento(IRequestDto request, BaseProviderEntity? entity);
         Task<IResponseDto> ConsultaBoleto(IRequestDto request, BaseProviderEntity? entity);
         Task<IResponseDto> AlterarValorBoleto(IRequestDto request, BaseProviderEntity? entity);
    }
}
