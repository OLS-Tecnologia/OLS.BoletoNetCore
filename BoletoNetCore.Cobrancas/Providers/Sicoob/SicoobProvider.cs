using Microsoft.Extensions.Configuration;
using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Mappers;
using static Org.BouncyCastle.Math.EC.ECCurve;


namespace BoletoNetCore.Cobrancas.Providers.Sicoob
{
    public class SicoobProvider : IBaseProviderSevice
    {
  
        public  bool SuportaCnab { get; set; } = false;
        public bool SuportaApi { get; set; } = true;

       

        public async Task<BaseProviderGerarBoletoResponseDto> EmitirBoleto(BaseProviderGerarBoletoRequestDto request)
        {

            try
            {
                
                // Mapear a requisição genérica para o Sicoob
                var sicoobRequest = EmitirBoletoSicoobRequestMapper.ToSicoob(request);

                // enviar requisição a api sicoob
                IncluirBoletoSicoobResponseDto response = null;


                if (response is null) throw new Exception(); //TODO: Mapear o erro


                //  Mapear a resposta do Sicoob para a genérica
                var convertedResponse = EmitirBoletoSicoobResponseMapper.ToBaseProvider(response);

                await Task.CompletedTask;

                return convertedResponse;

            }
            catch (Exception ex) {

                //Todo: logar o erro
                throw;
            }
          
        }

        public  void AlterarDataVencimentoBoleto()
        {

            Console.WriteLine(SuportaApi);
            throw new NotImplementedException();
        }

        public  void AlterarValorBoleto()
        {
            throw new NotImplementedException();
        }

        public  void BaixarBoleto()
        {
            throw new NotImplementedException();
        }

        public  void CancelarBoleto()
        {
            throw new NotImplementedException();
        }

        public  void ConsultaBaixaBoleto()
        {
            throw new NotImplementedException();
        }
    }
}
