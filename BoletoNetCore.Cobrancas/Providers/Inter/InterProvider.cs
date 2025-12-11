using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Inter.Utills;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace BoletoNetCore.Cobrancas.Providers.Inter
{
    public class InterProvider : IProviderBoleto<InterBaseRequestDto, InterBaseResponseDto>
    {
        public  bool SuportaCnab { get; set; } = false;
        public bool SuportaApi { get; set; } = true;

        public string ApiUrl { get; set; } = "https://cdpj-sandbox.partners.uatinter.co";

        internal static TokenAcesso TokenRequest { get; set; } = new TokenAcesso(); // gerado com permissão de leitura
        private static SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

     

        public  async Task<InterBaseResponseDto>  EmitirBoleto(InterBaseRequestDto request)
        {
            try
            {
                string permissoes = "boleto-cobranca.write boleto-cobranca.read";

                HttpClient client = new HttpClient();
                string bearerToken = "";

                X509Certificate cert = obterCert(request.ArquivoCertificado, request.ArquivoChave);
               
                bearerToken = await obterBearerToken(ApiUrl, request.ClientId, request.ClientSecret, permissoes, client, cert);


                //Criar uma cobrança
                var retorno = await CriarCobranca(ApiUrl, request,  client, cert, bearerToken);

                //ConsultarBoletoInterRequestDto ConsultarBoletoRequest = new()
                //{
                //    ArquivoCertificado = request.ArquivoCertificado,
                //    ArquivoChave = request.ArquivoChave,
                //    ClientId = request.ClientId,
                //    ClientSecret = request.ClientSecret,
                //    CodigoSolicitacao = retorno?.CodigoSolicitacao
                //};

                //// Buscar informações do boleto
                //var detalhesBoleto = await ConsultaBoleto(ConsultarBoletoRequest);


                return retorno;

            }
            catch (Exception ex)
            {
                // logar erro

                Console.WriteLine("Error: " + ex);
                // throw;
                return null;
            }

        }


        public async  Task<HttpStatusCode> BaixarBoleto(CancelamentoBoletoInterRequestDto req)
        {
            //try
            //{
            //    string permissoes = "boleto-cobranca.write boleto-cobranca.read";

            //    HttpClient client = new HttpClient();
            //    string bearerToken = ""; 

            //    X509Certificate cert = obterCert(req.ArquivoCertificado, req.ArquivoChave);


            //    bearerToken = await obterBearerToken(ApiUrl, req.ClientId, req.ClientSecret, permissoes, client, cert);

            //    var clientHandlerOauth = new HttpClientHandler();
            //    clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
            //    clientHandlerOauth.ClientCertificates.Add(cert);

            //    String uriCancelar = ApiUrl + "/cobranca/v3/cobrancas" + "/" + req.CodigoSolicitacao + "/cancelar";

            //    using (client = new HttpClient(clientHandlerOauth))
            //    {
            //        client.DefaultRequestHeaders.Accept.Clear();
            //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{bearerToken}");
            //        client.DefaultRequestHeaders.Add("x-conta-corrente", $"{req.XContaCorrente}");                   

            //        var payload = JsonSerializer.Serialize(req.RequestDto);

            //        var content = new StringContent(payload, Encoding.UTF8, "application/json");

            //        HttpResponseMessage response_detalhe = client.PostAsync(uriCancelar, content).GetAwaiter().GetResult();
            //        String resultado = "";
            //        if (response_detalhe.IsSuccessStatusCode)
            //        { 
            //            resultado = response_detalhe.Content.ReadAsStringAsync().Result;
            //        }
            //        else
            //        {
            //            throw new Exception("Status/Erro: " + response_detalhe.StatusCode + "/" + response_detalhe.ReasonPhrase);
            //        }

            //        client.Dispose();

            //        return HttpStatusCode.Accepted;
            //    }

            //}
            //catch (Exception ex) {
            //    Console.WriteLine(" Error: " + ex);
            //    return HttpStatusCode.BadRequest;

            //}
            throw new NotImplementedException();

        }



        //public async Task<IResponseDto> ConsultaBoleto(RequestBse interReq)
        //{

        //    ConsultarBoletoInterRequestDto req = (ConsultarBoletoInterRequestDto)interReq;


        //    HttpClient client = new HttpClient();
        //    string bearerToken = "";     


        //    X509Certificate2 cert = X509Certificate2.CreateFromPem(req.ArquivoCertificado, req.ArquivoChave);
        //    string permissoes = "boleto-cobranca.read";

        //    bearerToken = await obterBearerToken(ApiUrl, req.ClientId, req.ClientSecret, permissoes, client, cert);

        //    string URI_Detalhe_boleto = $"{ApiUrl}/cobranca/v2/boletos/{req.CodigoSolicitacao}";

        //    var clientHandler = new HttpClientHandler();
        //    clientHandler.ClientCertificates.Add(cert);
        //    clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;

        //    using (client = new HttpClient(clientHandler))
        //    {
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{bearerToken}");
        //        client.DefaultRequestHeaders.Add("x-conta-corrente", $"{req.XContaCorrente}");


        //        HttpResponseMessage recuperarCobranca =  client.GetAsync(URI_Detalhe_boleto).GetAwaiter().GetResult();

        //        string resultRecuperarCobranca = "";
        //        if (recuperarCobranca.IsSuccessStatusCode)
        //        {
        //            resultRecuperarCobranca = recuperarCobranca.Content.ReadAsStringAsync().Result;

        //            return JsonSerializer.Deserialize<RecuperarCobrancaInterResponse>(resultRecuperarCobranca);

        //        }
        //        else
        //        {
        //            Console.WriteLine("Error, received status code {0}: {1}", recuperarCobranca?.StatusCode, recuperarCobranca?.ReasonPhrase);

        //            throw new Exception($" Erro ao tentar bucar dados da cobrança com id {req.CodigoSolicitacao}.");
        //        }

        //    }          


        //}

        // ----------------------------------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------------------------

        private async Task<EmitirBoletoInterResponseDto> CriarCobranca(string urlInter, InterBaseRequestDto request, HttpClient client, X509Certificate cert, string? bearerToken)
        {

            //var handler = new SocketsHttpHandler
            //{
            //    SslOptions = new SslClientAuthenticationOptions
            //    {
            //        ClientCertificates = new X509CertificateCollection { cert },
            //        EnabledSslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13
            //    }
            //};


            var clientHandlerOauth = new HttpClientHandler();
            clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
            clientHandlerOauth.ClientCertificates.Add(cert);

            string uriEmitir = urlInter + "/cobranca/v3/cobrancas";

            using (client = new HttpClient(clientHandlerOauth))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{bearerToken}");
                client.DefaultRequestHeaders.Add("x-conta-corrente", $"{((EmitirBoletoInterRequestDto)request).XContaCorrente}");
               
                var payload = JsonSerializer.Serialize(request);
              
                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                HttpResponseMessage response_detalhe = await client.PostAsync(uriEmitir, content);
                string resultado = "";

                if (response_detalhe.IsSuccessStatusCode)
                {
                    resultado = await response_detalhe.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Status/Erro: " + response_detalhe.StatusCode + "/" + response_detalhe.ReasonPhrase);
                }
                

                return  JsonSerializer.Deserialize<EmitirBoletoInterResponseDto>(resultado);             

            }
        }



        private async Task<AtualizarBoletoInterResponseDto> AtualizarBoleto(AtualizarboletoInterRequestDto req)
        {
            string permissoes = "boleto-cobranca.write";

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

                HttpResponseMessage response_detalhe = client.PostAsync(uriEditar, content).GetAwaiter().GetResult();
                string resultado = "";
                if (response_detalhe.IsSuccessStatusCode)
                {
                    resultado = response_detalhe.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new Exception("Status/Erro: " + response_detalhe.StatusCode + "/" + response_detalhe.ReasonPhrase);
                }

                client.Dispose();

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




        // TODO: Metodos serão apagados
        public Task<HttpStatusCode> BaixarBoleto(InterBaseRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<InterBaseResponseDto> AlterarDataDeVencimentoBoleto(InterBaseRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<InterBaseResponseDto> ConsultaBoleto(InterBaseRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<InterBaseResponseDto> AlterarValorBoleto(InterBaseRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
  


}
