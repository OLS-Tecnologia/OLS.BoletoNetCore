using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Inter.Utills;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response;
using OLS.LibCore.Validate;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace BoletoNetCore.Cobrancas.Providers.Inter
{
    public class InterProvider :
    IProviderEmitirBoleto<EmitirBoletoInterRequestDto>,
    IProviderBaixarBoleto<CancelamentoBoletoInterRequestDto>,
    IProviderConsultaBoleto<ConsultarBoletoInterRequestDto>,
    IProviderATualizarBoleto<AtualizarboletoInterRequestDto>
    {
        public bool SuportaCnab { get; set; } = false;
        public bool SuportaApi { get; set; } = true;

        public string ApiUrl { get; set; } = string.Empty;

        internal static TokenAcesso TokenRequest { get; set; } = new TokenAcesso();

        private static SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        private static X509Certificate CertInter;

        private ValidationResult _validationResult;

        public InterProvider(string apiUrl)
        {
            ApiUrl = apiUrl;
            _validationResult = new ValidationResult();
        }      


        public  async Task<ValidationResult>  EmitirBoleto(List<EmitirBoletoInterRequestDto> requests)
        {        

            _validationResult.Object = new List<RecuperarCobrancaInterResponse>();


            if (requests.Count == 0)
            {
                _validationResult.AddMensagem("Nenhuma requisição informada", tipo: ValidationMessageType.Aviso);
                return _validationResult;

            }

            foreach (var request in requests) {

                try
                {
                    request.IsValid();

                    string permissoes = "boleto-cobranca.write boleto-cobranca.read";

                    HttpClient client = new HttpClient();
                    string bearerToken = "";

                    X509Certificate cert = await ObterCert(request.ArquivoCertificado, request.ArquivoChave);

                    bearerToken = await obterBearerToken(ApiUrl, request.ClientId, request.ClientSecret, permissoes, client, cert);


                    var clientHandlerOauth = new HttpClientHandler();
                    clientHandlerOauth.ClientCertificateOptions = ClientCertificateOption.Manual;
                    clientHandlerOauth.ClientCertificates.Add(cert);

                    string uriEmitir = ApiUrl + "/cobranca/v3/cobrancas";

                    using (client = new HttpClient(clientHandlerOauth))
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{bearerToken}");
                        client.DefaultRequestHeaders.Add("x-conta-corrente", $"{request.XContaCorrente}");

                        var payload = JsonSerializer.Serialize(request.RequestDto);

                        var content = new StringContent(payload, Encoding.UTF8, "application/json");

                        HttpResponseMessage resposta = await client.PostAsync(uriEmitir, content);                       

                        if (!resposta.IsSuccessStatusCode)
                        {
                            string detalhesErro = await  resposta.Content.ReadAsStringAsync();
                            _validationResult.AddMensagem($"Erro ao gerar Boleto: {resposta.StatusCode}: {detalhesErro}", request.RequestDto.SeuNumero);
                            return _validationResult;
                        }

                        var respostaData = await resposta.Content.ReadFromJsonAsync<EmitirBoletoInterResponseDto>();


                        string URI_Detalhe_boleto = $"{ApiUrl}/cobranca/v3/cobrancas/{respostaData?.CodigoSolicitacao}";

                        HttpResponseMessage recuperarCobranca = await client.GetAsync(URI_Detalhe_boleto);

                      

                        if (!recuperarCobranca.IsSuccessStatusCode)                      
                        {
                            var detalhesErro = recuperarCobranca.Content.ReadAsStringAsync();

                            _validationResult.AddMensagem($" O Boleto foi gerado mas ocorreu um erro ao buscar detalhes do boleto.", request.RequestDto.SeuNumero);
                            _validationResult.AddMensagem($"Erro:  {recuperarCobranca?.StatusCode} : {detalhesErro}");

                            return _validationResult;
                        }

                        var resultRecuperarCobranca = await recuperarCobranca.Content.ReadFromJsonAsync<RecuperarCobrancaInterResponse>();


                        ((List<RecuperarCobrancaInterResponse>)_validationResult.Object).Add(resultRecuperarCobranca);

                        _validationResult.AddMensagem("Boleto gerado com sucesso", request.RequestDto.SeuNumero, tipo: ValidationMessageType.Sucesso);

                        if (requests.Count > 1)
                            await Task.Delay(TimeSpan.FromMilliseconds(500));

                    }

                }
                catch (Exception ex)
                {                    
                    _validationResult.AddMensagem($"Erro na validação da requisição: {ex.Message}", request.RequestDto.SeuNumero);
                    return _validationResult;
                }

            }
            return _validationResult;

        }


        public async  Task<ValidationResult> BaixarBoleto(CancelamentoBoletoInterRequestDto req)
        {
            ValidationResult _validateResult = new ValidationResult();

            try
            {

                req.IsValid();
                
                string permissoes = "boleto-cobranca.write boleto-cobranca.read";

                HttpClient client = new HttpClient();
                string bearerToken = "";

                X509Certificate cert = await ObterCert(req.ArquivoCertificado, req.ArquivoChave);


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
                req.IsValid();

                HttpClient client = new HttpClient();
                string bearerToken = "";

                X509Certificate cert = await ObterCert(req.ArquivoCertificado, req.ArquivoChave);

                string permissoes = "boleto-cobranca.read boleto-cobranca.write";

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

                    string resultRecuperarCobranca = await recuperarCobranca.Content.ReadAsStringAsync(); 

                    if (recuperarCobranca.IsSuccessStatusCode)
                    {
                        var response =  JsonSerializer.Deserialize<RecuperarCobrancaInterResponse>(resultRecuperarCobranca);
                        _validateResult.Object = response;

                        return _validateResult;

                    }
                    else
                    {                       
                        _validateResult.AddMensagem($" Erro ao tentar bucar dados da cobrança com id {req.CodigoSolicitacao}.");
                        _validateResult.AddMensagem($"Código Http: {recuperarCobranca?.StatusCode} : {resultRecuperarCobranca}");

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

                request.IsValid();

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
                request.IsValid();

                var result = await AtualizarBoleto(request);
                _validateResult.Object = result;

                return _validateResult;

            }
            catch (Exception ex) {
                _validateResult.AddMensagem(ex.Message);

                return _validateResult;          
            
            }
        }
            
             

        private async Task<AtualizarBoletoInterResponseDto> AtualizarBoleto(AtualizarboletoInterRequestDto req)
        {
            string permissoes = "boleto-cobranca.write boleto-cobranca.read";

            HttpClient client = new HttpClient();       

            X509Certificate cert = await ObterCert(req.ArquivoCertificado, req.ArquivoChave);

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

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Erro: {response.StatusCode}: {response.ReasonPhrase}");

                    }

                    string jsonStr = await response.Content.ReadAsStringAsync();

                    TokenModel? tokenModel = JsonSerializer.Deserialize<TokenModel>(jsonStr);

                    client.Dispose();

                    DateTime currentDate = DateTime.UtcNow;

                    var tempoExpiracao = tokenModel?.expires_in is not null ? tokenModel.expires_in - 15 : 0;

                    TokenRequest.access_token = tokenModel?.access_token ?? "";
                    TokenRequest.CreatedAt = currentDate;                   
                    TokenRequest.ExpiredAt = currentDate.AddSeconds(tempoExpiracao);

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

        private async Task<X509Certificate> ObterCert(String certPem, String keyPem)
        {
            try
            {
                if (CertInter is not null)
                {
                    return CertInter;

                }


                string certificado = File.ReadAllText(certPem);
                string chave = File.ReadAllText(keyPem);


                X509Certificate2 cert = X509Certificate2.CreateFromPem(certificado, chave);

                // Exporta para PFX (com a chave privada)
                byte[] pfxBytes = cert.Export(X509ContentType.Pkcs12);

                var newCert =
                     new X509Certificate2(pfxBytes, (string)null,
                        X509KeyStorageFlags.Exportable |
                        X509KeyStorageFlags.MachineKeySet |
                        X509KeyStorageFlags.PersistKeySet);

                CertInter = newCert;

                return newCert;
            }
            catch (Exception ex) {
                throw;

            } 

        }
               
    } 


}
