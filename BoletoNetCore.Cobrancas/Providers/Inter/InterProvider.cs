using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Interfaces;
using BoletoNetCore.Cobrancas.Providers.Factory;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Inter
{
    public class InterProvider : IProviderBoleto<InterBaseRequestDto, InterBaseResponseDto>
    {
        public  bool SuportaCnab { get; set; } = false;
        public bool SuportaApi { get; set; } = true;

        public string ApiUrl { get; set; } = "https://cdpj-sandbox.partners.uatinter.com";

        public static TokenPropsExpire TokenRequest { get; set; } = new TokenPropsExpire(); // gerado com permissão de leitura


     

        public  async Task<InterBaseResponseDto>  EmitirBoleto(InterBaseRequestDto request)
        {
            try
            {

                string permissoes = "boleto-cobranca.write boleto-cobranca.read";

                HttpClient client = new HttpClient();
                string? bearerToken;

                X509Certificate cert = obterCert(request.ArquivoCertificado, request.ArquivoChave);

                TimeSpan validade = TimeSpan.FromMinutes(TokenRequest.expires_in);

                //Obtendo bearer token
                if (DateTime.UtcNow > TokenRequest.CreatedAt.Add(validade)) // adicionar comparação aqui
                {
                    bearerToken = TokenRequest.access_token;
                }
                else
                {

                    TokenModel tokenModel = await obterBearerToken(ApiUrl, request.ClientId, request.ClientSecret, permissoes, client, cert);

                    TokenRequest.access_token = tokenModel?.access_token;
                    TokenRequest.CreatedAt = DateTime.Now;
                    TokenRequest.expires_in = tokenModel.expires_in;

                }

                //Criar uma cobrança
                var retorno = CriarCobranca(ApiUrl, request, out client, cert, TokenRequest.access_token);


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
                throw;
            }

        }




        public async  Task<HttpStatusCode> BaixarBoleto(CancelamentoBoletoInterRequestDto req)
        {
            //try
            //{
            //    string permissoes = "boleto-cobranca.write boleto-cobranca.read";

            //    HttpClient client = new HttpClient();


            //    X509Certificate cert = obterCert(req.ArquivoCertificado, req.ArquivoChave);

            //    TimeSpan validade = TimeSpan.FromMinutes(TokenRequest.expires_in);

            //    //Obtendo bearer token
            //    if (DateTime.UtcNow > TokenRequest?.CreatedAt.Add(validade))
            //    {
            //        TokenModel tokenModel = await obterBearerToken(ApiUrl, req.ClientId, req.ClientSecret, permissoes, client, cert);

            //        TokenRequest.access_token = tokenModel?.access_token;
            //        TokenRequest.CreatedAt = DateTime.Now;
            //        TokenRequest.expires_in = tokenModel.expires_in;
            //    }                      


            //    var clientHandlerOauth = new HttpClientHandler();
            //    clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
            //    clientHandlerOauth.ClientCertificates.Add(cert);

            //    String uriCancelar = ApiUrl + "/cobranca/v3/cobrancas" + "/" + req.CodigoSolicitacao + "/cancelar";

            //    using (client = new HttpClient(clientHandlerOauth))
            //    {
            //        client.DefaultRequestHeaders.Accept.Clear();
            //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{TokenRequest.access_token}");
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
        //    String bearerToken = "";

        //    TimeSpan validade = TimeSpan.FromMinutes(TokenRequest?.expires_in ?? 0);
            

        //    X509Certificate2 cert = X509Certificate2.CreateFromPem(req.ArquivoCertificado, req.ArquivoChave);
        //    string permissoes = "boleto-cobranca.read";

        //    if (DateTime.UtcNow > TokenRequest?.CreatedAt.Add(validade))
        //    {
        //        TokenModel tokenModel = await obterBearerToken(ApiUrl, req.ClientId, req.ClientSecret, permissoes, client, cert);

        //        TokenRequest.access_token = tokenModel?.access_token;
        //        TokenRequest.CreatedAt = DateTime.Now;
        //        TokenRequest.expires_in = tokenModel.expires_in;

        //        bearerToken = tokenModel?.access_token;

        //        client.Dispose();
        //    }

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
      
        private EmitirBoletoInterResponseDto CriarCobranca(string urlInter, InterBaseRequestDto request, out HttpClient client, X509Certificate cert, string? bearerToken)
        {
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

                HttpResponseMessage response_detalhe = client.PostAsync(uriEmitir, content).GetAwaiter().GetResult();
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

                return  JsonSerializer.Deserialize<EmitirBoletoInterResponseDto>(resultado);              

            }
        }



        private async Task<AtualizarBoletoInterResponseDto> AtualizarBoleto(AtualizarboletoInterRequestDto req)
        {
            string permissoes = "boleto-cobranca.write";

            HttpClient client = new HttpClient();
       

            X509Certificate cert = obterCert(req.ArquivoCertificado, req.ArquivoChave);

            TimeSpan validade = TimeSpan.FromMinutes(TokenRequest.expires_in);

            //Obtendo bearer token
            if (DateTime.UtcNow > TokenRequest.CreatedAt.Add(validade))
            {
                TokenModel tokenModel = await obterBearerToken(ApiUrl, req.ClientId, req.ClientSecret, permissoes, client, cert);

                TokenRequest.access_token = tokenModel?.access_token;
                TokenRequest.CreatedAt = DateTime.Now;
                TokenRequest.expires_in = tokenModel.expires_in;
            }

            ///

            var clientHandlerOauth = new HttpClientHandler();
            clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
            clientHandlerOauth.ClientCertificates.Add(cert);

            String uriEditar = ApiUrl + "/cobranca/v3/cobrancas" + "/" + req.CodigoSolicitacao;

            using (client = new HttpClient(clientHandlerOauth))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{TokenRequest.access_token}");
                client.DefaultRequestHeaders.Add("x-conta-corrente", $"{req.XContaCorrente}");

                // Serialize class into JSON
                var payload = JsonSerializer.Serialize(req.RequestDto) ;

                // Wrap our JSON inside a StringContent object
                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                HttpResponseMessage response_detalhe = client.PostAsync(uriEditar, content).GetAwaiter().GetResult();
                String resultado = "";
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

        private async Task<TokenModel> obterBearerToken(string urlInter, string clientId, string clientSecret, string permissoes, HttpClient client, X509Certificate cert)
        {
            var clientHandlerOauth = new HttpClientHandler();
            clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
            clientHandlerOauth.ClientCertificates.Add(cert);

            String URI_Token = urlInter + "/oauth/v2/token";

            var data = new[]
            {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("scope", permissoes),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
             };

            using (client = new HttpClient(clientHandlerOauth))
            {
                var response = await  client.PostAsync(URI_Token, new FormUrlEncodedContent(data));

                String jsonStr = response.Content.ReadAsStringAsync().Result;

                TokenModel? tokenModel = JsonSerializer.Deserialize<TokenModel>(jsonStr);

                client.Dispose();

                return tokenModel;
            }
        }




        private static X509Certificate obterCert(String certPem, String keyPem)
        {

            string certificado = File.ReadAllText(certPem);
            string chave = File.ReadAllText(keyPem);

            X509Certificate2 cert = X509Certificate2.CreateFromPem(certPem, keyPem);

            return cert;
        }

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

    /// <summary>
    /// Token retornado pelo inter 
    /// </summary>
    public class TokenModel
    {
     
        public string? access_token { get; set; }
        public string? token_type { get; set; }
        public int expires_in { get; set; }
        public string? scope { get; set; }
    }

    public class TokenPropsExpire : TokenModel
    {
        public DateTime CreatedAt { get; set; }
    }
}
