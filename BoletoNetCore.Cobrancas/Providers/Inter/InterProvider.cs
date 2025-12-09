using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Factory;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Inter.Mappers;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Inter
{
    public class InterProvider : IProviderBoleto
    {
        public bool SuportaCnab { get; set; } = false;
        public bool SuportaApi { get; set; } = true;

        public string ApiUrl { get; set; } = "https://cdpj.partners.bancointer.com.br";

        public static TokenPropsExpire TokenRequestPost { get; set; } = new TokenPropsExpire(); // Gerado com permissão de escrita
        public static TokenPropsExpire TokenRequestGet { get; set; } = new TokenPropsExpire(); // gerado com permissão de leitura


        public  async  Task<RecuperarCobrancaInterResponse> EmitirBoleto(EmitirBoletoInterRequestDto request, string? clientId, string? clientSecret, string? ArquivoCertificado, 
            string? ArquivoChave)
        {
            try   
            {             
                //string nomeArquivoCertificado = "<nome arquivo certificado>.crt";
                //string nomeArquivoChave = "<nome arquivo chave privada>.key";

                //string clientId = "<clientId de sua aplicação>";
                //string clientSecret = "<clientSecret de sua aplicação>";
       
                string permissoes = "boleto-cobranca.write";

                HttpClient client;
                string? bearerToken;

                X509Certificate cert = obterCert(ArquivoCertificado, ArquivoChave);

                TimeSpan validade = TimeSpan.FromMinutes(TokenRequestPost.expires_in);

                //Obtendo bearer token
                if (DateTime.UtcNow > TokenRequestPost.CreatedAt.Add(validade)) // adicionar comparação aqui
                {
                    bearerToken = TokenRequestPost.access_token;
                }
                else
                {
                   
                    TokenModel tokenModel = obterBearerToken(ApiUrl, clientId, clientSecret, permissoes, out client, cert);

                    TokenRequestPost.access_token = tokenModel?.access_token;
                    TokenRequestPost.CreatedAt = DateTime.Now;
                    TokenRequestPost.expires_in = tokenModel.expires_in;

                }                              

                //Criar uma cobrança
                var retorno = CriarCobranca(ApiUrl, request, out client, cert, TokenRequestPost.access_token);

                // Buscar informações do boleto
                var detalhesBoleto = await ConsultaBoleto(retorno?.CodigoSolicitacao, ArquivoCertificado, ArquivoChave, clientId, clientSecret);


                return detalhesBoleto;

            }
            catch (Exception ex) {
                // logar erro

                Console.WriteLine("Error: " + ex);
                throw;
            }
        }

        public  void AlterarDataVencimentoBoleto()
        {

            Console.WriteLine(SuportaApi);
            throw new NotImplementedException();
        }

        public void AlterarValorBoleto()
        {
            throw new NotImplementedException();
        }

      

        public HttpStatusCode BaixarBoleto( String contaCorrente, String codigoSolicitacao, string ArquivoCertificado,
            string ArquivoChave, CancelamentoBoletoInterDto motivoCancelamento, String? bearerToken)
        {
            try
            {


                HttpClient client;
                X509Certificate cert = obterCert(ArquivoCertificado, ArquivoChave);

                var clientHandlerOauth = new HttpClientHandler();
                clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
                clientHandlerOauth.ClientCertificates.Add(cert);

                String uriCancelar = ApiUrl + "/cobranca/v3/cobrancas" + "/" + codigoSolicitacao + "/cancelar";

                using (client = new HttpClient(clientHandlerOauth))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);
                    client.DefaultRequestHeaders.Add("x-conta-corrente", contaCorrente);                   

                    var payload = JsonSerializer.Serialize(motivoCancelamento);

                    var content = new StringContent(payload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response_detalhe = client.PostAsync(uriCancelar, content).GetAwaiter().GetResult();
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

                    return HttpStatusCode.Accepted;
                }

            }
            catch (Exception ex) {
                Console.WriteLine(" Error: " + ex);
                return HttpStatusCode.BadRequest;
            
            }


        }

        public async Task<RecuperarCobrancaInterResponse> ConsultaBoleto(string codigoCobranca, string certPem, string eccPem, string clientId, string clientSecret)
        {
           
            HttpClient client = new HttpClient();
            String bearerToken = "";

            TimeSpan validade = TimeSpan.FromMinutes(TokenRequestGet?.expires_in ?? 0);

            //String certPem = File.ReadAllText("Inter_API_Certificado.crt");
            //String eccPem = File.ReadAllText("Inter_API_Chave.key");

            X509Certificate2 cert = X509Certificate2.CreateFromPem(certPem, eccPem);
            string permissoes = "boleto-cobranca.read";

            if (DateTime.UtcNow > TokenRequestGet?.CreatedAt.Add(validade))
            {
                TokenModel tokenModel = obterBearerToken(ApiUrl, clientId, clientSecret, permissoes, out client, cert);

                TokenRequestGet.access_token = tokenModel?.access_token;
                TokenRequestGet.CreatedAt = DateTime.Now;
                TokenRequestGet.expires_in = tokenModel.expires_in;

                bearerToken = tokenModel?.access_token;

                client.Dispose();
            }

            String URI_Detalhe_boleto = $"{ApiUrl}/cobranca/v2/boletos/{codigoCobranca}";


            var clientHandler = new HttpClientHandler();
            clientHandler.ClientCertificates.Add(cert);
            clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;

            using (client = new HttpClient(clientHandler))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);
                client.DefaultRequestHeaders.Add("x-conta-corrente", "<conta corrente selecionada>");
                
            
                HttpResponseMessage recuperarCobranca = client.GetAsync(URI_Detalhe_boleto).GetAwaiter().GetResult();

                string resultRecuperarCobranca = "";
                if (recuperarCobranca.IsSuccessStatusCode)
                {
                    resultRecuperarCobranca = recuperarCobranca.Content.ReadAsStringAsync().Result;

                    return JsonSerializer.Deserialize<RecuperarCobrancaInterResponse>(resultRecuperarCobranca);

                }
                else
                {
                    Console.WriteLine("Error, received status code {0}: {1}", recuperarCobranca?.StatusCode, recuperarCobranca?.ReasonPhrase);

                    throw new Exception(" Erro ao tentar bucar dados da cobrança.");
                }

            }          


        }

        private static TokenModel obterBearerToken(String urlInter, String clientId, String clientSecret, String permissoes, out HttpClient client, X509Certificate cert)
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
                var response = client.PostAsync(URI_Token, new FormUrlEncodedContent(data)).GetAwaiter().GetResult();

                String jsonStr = response.Content.ReadAsStringAsync().Result;

                TokenModel? tokenModel = JsonSerializer.Deserialize<TokenModel>(jsonStr);           

                client.Dispose();

                return tokenModel;
            }
        }

        private static X509Certificate obterCert(String certPem, String keyPem)
        {
            //String certPem = File.ReadAllText(nomeArquivoCertificado);
            //String keyPem = File.ReadAllText(nomeArquivoChave);

            X509Certificate2 cert = X509Certificate2.CreateFromPem(certPem, keyPem);

            return cert;
        }

        private EmitirBoletoInterResponseDto CriarCobranca(string urlInter, EmitirBoletoInterRequestDto request, out HttpClient client, X509Certificate cert, string? bearerToken)
        {
            var clientHandlerOauth = new HttpClientHandler();
            clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
            clientHandlerOauth.ClientCertificates.Add(cert);

            string uriEmitir = urlInter + "/cobranca/v3/cobrancas";

            using (client = new HttpClient(clientHandlerOauth))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);
                client.DefaultRequestHeaders.Add("x-conta-corrente", request.XContaCorrente);
               
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

        //public override CancelamentoBoletoInterDto ImpostoDeRenda<EmitirBoletoInterRequestDto, CancelamentoBoletoInterDto>(EmitirBoletoInterRequestDto req)
        //{
        //    var teste = new CancelamentoBoletoInterDto();
        //    return teste;
        //}
    }
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
