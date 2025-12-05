using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;


namespace BoletoNetCore.Cobrancas.Providers.BaseProvider
{
    public interface IBaseProviderSevice
    {
        bool SuportaApi { get;  set; }
        bool SuportaCnab { get; set; }       


       public Task<BaseProviderGerarBoletoResponseDto>  EmitirBoleto(BaseProviderGerarBoletoRequestDto request);

       public void CancelarBoleto();
       
       public  void BaixarBoleto();
       
       public  void AlterarDataVencimentoBoleto();
       public void AlterarValorBoleto();
       
       public void ConsultaBaixaBoleto();

    }
}
