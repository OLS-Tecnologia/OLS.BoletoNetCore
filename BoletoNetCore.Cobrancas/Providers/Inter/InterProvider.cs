using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Inter.Utills;
using OLS.LibCore.Validate;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace BoletoNetCore.Cobrancas.Providers.Inter
{
    public class InterProvider :
    IProviderEmitirBoleto<EmitirBoletoInterRequestDto>,
    IProviderBaixarBoleto<CancelamentoBoletoInterRequestDto>,
    IProviderConsultaBoleto<ConsultarBoletoInterRequestDto>,
    IProviderAlterarVencimento<AtualizarboletoInterRequestDto>,
     IProviderAlterarValorBoleto<AtualizarboletoInterRequestDto>,
    // <TEmitir, TBaixar, TAlterarVenc, TAlterarValor, TConsultar>
    IProviderBoleto//<EmitirBoletoInterRequestDto, CancelamentoBoletoInterRequestDto, AtualizarboletoInterRequestDto, AtualizarboletoInterRequestDto, ConsultarBoletoInterRequestDto>
    {
        public bool SuportaCnab { get; set; } = false;
        public bool SuportaApi { get; set; } = true;

        public string ApiUrl { get; set; } = string.Empty;

        internal static TokenAcesso TokenRequest { get; set; } = new TokenAcesso();

        private static SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        public InterProvider(string apiUrl)
        {
            ApiUrl = apiUrl;
        }      


        public  async Task<ValidationResult>  EmitirBoleto(EmitirBoletoInterRequestDto request)
        {
            ValidationResult _validateResult = new();
            try
            {
                request.IsValid();
                
                string permissoes = "boleto-cobranca.write boleto-cobranca.read";

                HttpClient client = new HttpClient();
                string bearerToken = "";

                X509Certificate cert = obterCert(request.ArquivoCertificado, request.ArquivoChave);
               
                bearerToken = await obterBearerToken(ApiUrl, request.ClientId, request.ClientSecret, permissoes, client, cert);


                //Criar uma cobrança
                var retorno = await CriarCobranca(ApiUrl, request,  client, cert, bearerToken);

                var ConsultarBoletoRequest = new ConsultarBoletoInterRequestDto()
                {
                   CodigoSolicitacao = retorno?.CodigoSolicitacao,
                   ClientSecret = request.ClientSecret,
                   XContaCorrente = request.XContaCorrente,
                   ArquivoCertificado = request.ArquivoCertificado,
                   ArquivoChave = request.ArquivoChave,
                   ClientId = request.ClientId 

                };  
                

                // Buscar informações do boleto
                var detalhesBoleto = await ConsultaBoleto(ConsultarBoletoRequest);
                
                return detalhesBoleto;

            }
            catch (Exception ex)
            {
               

                _validateResult.AddMensagem(ex.Message);
                return _validateResult;               
            }

        }


        public async  Task<ValidationResult> BaixarBoleto(CancelamentoBoletoInterRequestDto req)
        {
            ValidationResult _validateResult = new ValidationResult();

            try
            {              
                
                string permissoes = "boleto-cobranca.write boleto-cobranca.read";

                HttpClient client = new HttpClient();
                string bearerToken = "";

                X509Certificate cert = obterCert(req.ArquivoCertificado, req.ArquivoChave);


                bearerToken = await obterBearerToken(ApiUrl, req.ClientId, req.ClientSecret, permissoes, client, cert);

                var clientHandlerOauth = new HttpClientHandler();
                clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
                clientHandlerOauth.ClientCertificates.Add(cert);

                string uriCancelar = ApiUrl + "/cobranca/v3/cobrancas" + "/" + req.CodigoSolicitacao + "/cancelar";

                using (client = new HttpClient(clientHandlerOauth))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{bearerToken}");
                    client.DefaultRequestHeaders.Add("x-conta-corrente", $"{req.XContaCorrente}");

                    var payload = JsonSerializer.Serialize(req.RequestDto);

                    var content = new StringContent(payload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response_detalhe = await client.PostAsync(uriCancelar, content);

                    _validateResult.Object = response_detalhe.StatusCode;

                    if (!response_detalhe.IsSuccessStatusCode)
                    {
                        _validateResult.AddMensagem("Erro ao dar baixa no boleto.");
                       
                        return _validateResult ;
                    }                     

                    return _validateResult;
                }

            }
            catch (Exception ex)
            {          
                _validateResult.AddMensagem(ex.Message);
                return _validateResult;
            }
           

        }

        public async Task<ValidationResult> ConsultaBoleto(ConsultarBoletoInterRequestDto req)
        {
            ValidationResult _validateResult = new();
            try
            {
                HttpClient client = new HttpClient();
                string bearerToken = "";

                X509Certificate cert = obterCert(req.ArquivoCertificado, req.ArquivoChave);

                string permissoes = "boleto-cobranca.read";

                bearerToken = await obterBearerToken(ApiUrl, req.ClientId, req.ClientSecret, permissoes, client, cert);

                string URI_Detalhe_boleto = $"{ApiUrl}/cobranca/v3/cobrancas/{req.CodigoSolicitacao}";

                var clientHandler = new HttpClientHandler();
                clientHandler.ClientCertificates.Add(cert);
                clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;

                using (client = new HttpClient(clientHandler))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{bearerToken}");
                    client.DefaultRequestHeaders.Add("x-conta-corrente", $"{req.XContaCorrente}");


                    HttpResponseMessage recuperarCobranca = await client.GetAsync(URI_Detalhe_boleto);

                    string resultRecuperarCobranca = "";
                    if (recuperarCobranca.IsSuccessStatusCode)
                    {
                        resultRecuperarCobranca = await recuperarCobranca.Content.ReadAsStringAsync();

                        var response =  JsonSerializer.Deserialize<RecuperarCobrancaInterResponse>(resultRecuperarCobranca);
                        _validateResult.Object = response;

                        return _validateResult;

                    }
                    else
                    {                       
                        _validateResult.AddMensagem($" Erro ao tentar bucar dados da cobrança com id {req.CodigoSolicitacao}.");
                        _validateResult.AddMensagem($"Código Http: {recuperarCobranca?.StatusCode} : {recuperarCobranca?.ReasonPhrase}");

                        return _validateResult;
                    }

                }

            }
            catch (Exception ex) {

                _validateResult.AddMensagem(ex.Message);
                return _validateResult;
                
            }
        }

        /// <summary>
        /// Após editar uma cobrança, seu valor pode levar até 30 minutos para ser atualizado.
        /// Atualiza a data de vencimento do boleto e o valor na mesma requisição
        /// </summary>    
        /// <returns></returns>
        public async Task<ValidationResult> AlterarDataDeVencimentoBoleto(AtualizarboletoInterRequestDto request)
        {
            ValidationResult _validateResult = new();
            try
            {
                var result = await AtualizarBoleto(request);
                _validateResult.Object = result;

                return _validateResult;

            }
            catch (Exception ex)
            {
                _validateResult.AddMensagem(ex.Message);

                return _validateResult;

            }
        }

        /// <summary>
        /// Após editar uma cobrança, seu valor pode levar até 30 minutos para ser atualizado.
        /// Atualiza a data de vencimento do boleto e o valor na mesma requisição
        /// </summary>    
        /// <returns></returns>
        public async Task<ValidationResult> AlterarValorBoleto(AtualizarboletoInterRequestDto request)
        {
            ValidationResult _validateResult = new();
            try
            {
                var result = await AtualizarBoleto(request);
                _validateResult.Object = result;

                return _validateResult;

            }
            catch (Exception ex) {
                _validateResult.AddMensagem(ex.Message);

                return _validateResult;          
            
            }
        }
        

        // ----------------------------------------------------------------------------------------------------------------

        private async Task<EmitirBoletoInterResponseDto> CriarCobranca(string urlInter, EmitirBoletoInterRequestDto request, HttpClient client, X509Certificate cert, string? bearerToken)
        {           

            var clientHandlerOauth = new HttpClientHandler();
            clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
            clientHandlerOauth.ClientCertificates.Add(cert);

            string uriEmitir = urlInter + "/cobranca/v3/cobrancas";

            using (client = new HttpClient(clientHandlerOauth))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{bearerToken}");
                client.DefaultRequestHeaders.Add("x-conta-corrente", $"{request.XContaCorrente}");
               
                var payload = JsonSerializer.Serialize(request.RequestDto);
              
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

                    throw new Exception("Status/Erro: " + response_detalhe.StatusCode + "/" + response_detalhe.ReasonPhrase);
                }
                

                return  JsonSerializer.Deserialize<EmitirBoletoInterResponseDto>(resultado);             

            }
        }

        private async Task<AtualizarBoletoInterResponseDto> AtualizarBoleto(AtualizarboletoInterRequestDto req)
        {
            string permissoes = "boleto-cobranca.write boleto-cobranca.read";

            HttpClient client = new HttpClient();       

            X509Certificate cert = obterCert(req.ArquivoCertificado, req.ArquivoChave);

            string bearerToken = "";           
           
            bearerToken = await obterBearerToken(ApiUrl, req.ClientId, req.ClientSecret, permissoes, client, cert);


            var clientHandlerOauth = new HttpClientHandler();
            clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
            clientHandlerOauth.ClientCertificates.Add(cert);

            string uriEditar = ApiUrl + "/cobranca/v3/cobrancas" + "/" + req.CodigoSolicitacao;

            using (client = new HttpClient(clientHandlerOauth))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{bearerToken}");
                client.DefaultRequestHeaders.Add("x-conta-corrente", $"{req.XContaCorrente}");

                // Serialize class into JSON
                var payload = JsonSerializer.Serialize(req.RequestDto) ;

                // Wrap our JSON inside a StringContent object
                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                HttpResponseMessage response_detalhe = await client.PatchAsync(uriEditar, content);
                string resultado = "";
                if (response_detalhe.IsSuccessStatusCode)
                {
                    resultado = await response_detalhe.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Status/Erro: " + response_detalhe.StatusCode + "/" + response_detalhe.ReasonPhrase);
                }
                

                return JsonSerializer.Deserialize<AtualizarBoletoInterResponseDto>(resultado);
            }

         }

        private async Task<string> obterBearerToken(string urlInter, string clientId, string clientSecret, string permissoes, HttpClient client, X509Certificate cert)
        {                 

            //Obtendo bearer token
            if (TokenRequest.access_token is not null && DateTime.UtcNow < TokenRequest.ExpiredAt)                       
               return  TokenRequest.access_token;

            await _lock.WaitAsync();

            try
            {
                //double check
                if (TokenRequest.access_token is not null && DateTime.UtcNow < TokenRequest.ExpiredAt)
                    return TokenRequest.access_token;

                var clientHandlerOauth = new HttpClientHandler();
                clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
                clientHandlerOauth.ClientCertificates.Add(cert);

                string URI_Token = urlInter + "/oauth/v2/token";

                var data = new[]
                {
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("scope", permissoes),
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };

                using (client = new HttpClient(clientHandlerOauth))
                {
                    var response = await client.PostAsync(URI_Token, new FormUrlEncodedContent(data));

                    string jsonStr = await response.Content.ReadAsStringAsync();

                    TokenModel? tokenModel = JsonSerializer.Deserialize<TokenModel>(jsonStr);

                    client.Dispose();


                    DateTime currentDate = DateTime.UtcNow;

                    TokenRequest.access_token = tokenModel?.access_token ?? "";
                    TokenRequest.CreatedAt = currentDate;                   
                    TokenRequest.ExpiredAt = currentDate.AddSeconds(tokenModel?.expires_in ?? 0);

                    return TokenRequest.access_token;
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

        private static X509Certificate obterCert(String certPem, String keyPem)
        {
            string certificado = File.ReadAllText(certPem);
            string chave = File.ReadAllText(keyPem);

            X509Certificate2 cert = X509Certificate2.CreateFromPem(certificado, chave);

            // Exporta para PFX (com a chave privada)
            byte[] pfxBytes = cert.Export(X509ContentType.Pkcs12);
         
            return new X509Certificate2(pfxBytes, (string)null,
                X509KeyStorageFlags.Exportable |
                X509KeyStorageFlags.MachineKeySet |
                X509KeyStorageFlags.PersistKeySet);
         
        }
               
    } 


}
