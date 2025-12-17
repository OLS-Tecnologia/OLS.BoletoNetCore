using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response;
using OLS.LibCore.Validate;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob
{
    public class SicoobProvider : IProviderBoleto,
        IProviderEmitirBoleto<EmitirBoletoSicoobResquetDto>,
        IProviderBaixarBoleto<BaixarBoletoSicoobRequestDto>,
        IProviderConsultaBoleto<ConsultarBoletoRequestDto>,
        IProviderAlterarVencimento<EditarBoletoSicoobRequestDto>,
        IProviderAlterarValorBoleto<EditarBoletoSicoobRequestDto>
    {

        public bool SuportaCnab { get; set; } = false;
        public bool SuportaApi { get; set; } = true;
        public string ApiUrl { get; set; } = string.Empty ; 
        private  string? Token { get; set; } = string.Empty;

        private string ApiUrlGerarToken = "https://auth.sicoob.com.br/auth/realms/cooperado/protocol/openid-connect/token";

        private static SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        public SicoobProvider(string apiUrl, string? token= null)
        {
            ApiUrl = apiUrl;
            Token = token;
        }

        public async Task<ValidationResult> EmitirBoleto(EmitirBoletoSicoobResquetDto request)
        {

            ValidationResult _validateResult = new();

            try
            {
                string uriEmitir = ApiUrl + "/boletos";

                string ArquivoCertificado = @"C:\CERTS\private.PEM";
                string ArquivoChave= @"C:\CERTS\public.PEM";

                //TODO: Verificar a geração do certificado
               // var cert = obterCert(ArquivoCertificado, ArquivoChave) ;

                //var clientHandlerOauth = new HttpClientHandler();
                //clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
                //clientHandlerOauth.ClientCertificates.Add(cert);


               //  await ObterToken(request.ClienteId);               

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{Token}");
                    client.DefaultRequestHeaders.Add("client_id", $"{request.ClienteId}");

                    var payload = JsonSerializer.Serialize(request.Boleto);

                    var content = new StringContent(payload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response_detalhe = await client.PostAsync(uriEmitir, content);
                     string resultado = "";

                    if (response_detalhe.IsSuccessStatusCode)
                    {
                        resultado = await response_detalhe.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        var teste = await response_detalhe.Content.ReadAsStringAsync();
                        _validateResult.AddMensagem($"Status/Erro: {response_detalhe.StatusCode} - {response_detalhe.ReasonPhrase}");
                        return _validateResult;
                    }                   

                    _validateResult.Object = JsonSerializer.Deserialize<EmitirBoletoInterResponseDto>(resultado); 

                    return _validateResult;
                }

               
            }
            catch (Exception ex)
            {
                _validateResult.AddMensagem(ex.Message);

                return _validateResult;
                
            }

        }
        public async Task<ValidationResult> AlterarDataDeVencimentoBoleto(EditarBoletoSicoobRequestDto request)
        {

            ValidationResult _validateResult = new();

            try
            {
                
                string uriEmitir = ApiUrl + $"/boletos/{request.NossoNumero}";

                //TODO: Verificar a geração do certificado
            //    var cert = obterCert("caminho chave");// new X509Certificate2(uriEmitir);

             //   await ObterToken(request.ClientId, cert);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{Token}");
                    client.DefaultRequestHeaders.Add("client_id", $"{request.ClientId}");

                    var payload = JsonSerializer.Serialize(request.Boleto);

                    var content = new StringContent(payload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response_detalhe = await client.PatchAsync(uriEmitir, content);
                    HttpStatusCode resultado;

                    if (response_detalhe.IsSuccessStatusCode)
                    {
                        resultado = response_detalhe.StatusCode;
                    }
                    else
                    {
                        _validateResult.AddMensagem($"Status/Erro: {response_detalhe.StatusCode} - {response_detalhe.ReasonPhrase}");
                        return _validateResult;
                    }                 

                    _validateResult.Object = resultado;

                    return _validateResult;

                }




            }
            catch (Exception ex)
            {
                _validateResult.AddMensagem(ex.Message);

                return _validateResult;

            }
        }

        public async  Task<ValidationResult> AlterarValorBoleto(EditarBoletoSicoobRequestDto request)
        {

            ValidationResult _validateResult = new();

            try
            {
                string uriEmitir = ApiUrl + $"/boletos/{request.NossoNumero}";

                //TODO: Verificar a geração do certificado
                //var cert = obterCert("caminho chave");// new X509Certificate2(uriEmitir);

                //await ObterToken(request.ClientId, cert);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{Token}");
                    client.DefaultRequestHeaders.Add("client_id", $"{request.ClientId}");

                    var payload = JsonSerializer.Serialize(request.Boleto);

                    var content = new StringContent(payload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response_detalhe = await client.PatchAsync(uriEmitir, content);
                    HttpStatusCode resultado;

                    if (response_detalhe.IsSuccessStatusCode)
                    {
                        resultado =  response_detalhe.StatusCode;
                    }
                    else
                    {                       
                        _validateResult.AddMensagem($"Status/Erro: {response_detalhe.StatusCode} - {response_detalhe.ReasonPhrase}");
                        return _validateResult;
                    }                 

                    _validateResult.Object = resultado;

                    return _validateResult;

                }



            }
            catch (Exception ex)
            {
                _validateResult.AddMensagem(ex.Message);

                return _validateResult;

            }
        }

        public async Task<ValidationResult> BaixarBoleto(BaixarBoletoSicoobRequestDto request)
        {
            ValidationResult _validateResult = new ValidationResult();
            try
            {
               
                string uriEmitir = ApiUrl + $"/boletos/{request.NossoNumero}/baixar";

                //TODO: Verificar a geração do certificado
                //var cert = obterCert("caminho chave");// new X509Certificate2(uriEmitir);

                //await ObterToken(request.ClientId, cert);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{Token}");
                    client.DefaultRequestHeaders.Add("client_id", $"{request.ClientId}");

                    var payload = JsonSerializer.Serialize(request.Boleto);

                    var content = new StringContent(payload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response_detalhe = await client.PostAsync(uriEmitir, content);
                    HttpStatusCode resultado;

                    if (response_detalhe.IsSuccessStatusCode)
                    {
                        resultado = response_detalhe.StatusCode;
                    }
                    else
                    {
                        _validateResult.AddMensagem($"Status/Erro: {response_detalhe.StatusCode} - {response_detalhe.ReasonPhrase}");
                        return _validateResult;
                    }                  

                    _validateResult.Object = resultado;

                    return _validateResult;

                }
                
            }
            catch (Exception ex) {
                _validateResult.AddMensagem(ex.Message);
                return _validateResult;
            
            }
        }

        public async Task<ValidationResult> ConsultaBoleto(ConsultarBoletoRequestDto request)
        {
            ValidationResult _validateResult = new();

            try
            {
                await ObterToken(request.ClienteId);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{Token}");
                    client.DefaultRequestHeaders.Add("client_id", $"{request.ClienteId}");

                    string uriEmitir = ApiUrl + $"/boletos?numeroCliente={request.Body.NumeroCliente}&codigoModalidade={request.Body.CodigoModalidade}&nossoNumero={request.Body.NossoNumero}&linhaDigitavel={request.Body.LinhaDigitavel}&codigoBarras={request.Body.CodigoBarras}&numeroContratoCobranca={request.Body.NumeroContratoCobranca}";

                    HttpResponseMessage response_detalhe = await client.GetAsync(uriEmitir);
                    string resultado = "";

                    if (response_detalhe.IsSuccessStatusCode)
                    {
                        resultado = await response_detalhe.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        _validateResult.AddMensagem($"Status/Erro: {response_detalhe.StatusCode} - {response_detalhe.ReasonPhrase}");
                        return _validateResult;
                    }

                    _validateResult.Object = JsonSerializer.Deserialize<ConsultarBoletoResponseDto>(resultado);

                    return _validateResult;

                }

            }
            catch (Exception ex)
            {
                _validateResult.AddMensagem(ex.Message);

                return _validateResult;

            }
        }      


        private async Task ObterToken(string clientId)
        {

            if (Token is not null)
                return;

            await _lock.WaitAsync();          

            string permissoes = "cob.write cob.read cobv.write cobv.read";
            try
            {          
            
                var data = new[]
                {
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("scope", permissoes),
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(ApiUrlGerarToken, new FormUrlEncodedContent(data));

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Erro na obtenção do token: {response.StatusCode}: {response.ReasonPhrase}");
                    }

                    string jsonStr = await response.Content.ReadAsStringAsync();
                    Token = jsonStr;                   
                }             

            }
            catch (Exception ex) {

                throw;
            }
            finally
            {
                _lock.Release();
            }

        }      
       
    }
}
