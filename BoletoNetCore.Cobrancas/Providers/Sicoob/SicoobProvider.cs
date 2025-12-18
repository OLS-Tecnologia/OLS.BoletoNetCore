using BoletoNetCore.Cobrancas.Providers.BaseProvider;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response;
using OLS.LibCore.Validate;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob
{
    public class SicoobProvider : 
        IProviderEmitirBoleto<EmitirBoletoSicoobResquetDto>,
        IProviderBaixarBoleto<BaixarBoletoSicoobRequestDto>,
        IProviderConsultaBoleto<ConsultarBoletoRequestDto>,
        IProviderATualizarBoleto<EditarBoletoSicoobRequestDto>        
    {

        public bool SuportaCnab { get; set; } = false;
        public bool SuportaApi { get; set; } = true;
        public string ApiUrl { get; set; } = string.Empty ; 
        private  string? Token { get; set; } = string.Empty;

        private string ApiUrlGerarToken = "https://auth.sicoob.com.br/auth/realms/cooperado/protocol/openid-connect/token";

        private static SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        private ValidationResult _validationResult;

        public SicoobProvider(string apiUrl, string? token= null)
        {
            ApiUrl = apiUrl;
            Token = token;

            _validationResult = new ValidationResult();
        }

        public async Task<ValidationResult> EmitirBoleto(List<EmitirBoletoSicoobResquetDto> requests)
        {

            _validationResult.Object = new List<IncluirBoletoSicoobResponseDto>();
          
           
            if(requests.Count == 0)
            {                    
                _validationResult.AddMensagem("Nenhuma requisição informada", tipo: ValidationMessageType.Aviso);
                return _validationResult;           

            }

            foreach (var request in requests)
            {               
                try
                {
                    request.IsValid();

                    await ObterToken(request.ClienteId);

                    string uriEmitir = ApiUrl + "/boletos";
                      

                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{Token}");
                        client.DefaultRequestHeaders.Add("client_id", $"{request.ClienteId}");

                        var payload = JsonSerializer.Serialize(request.Boleto);

                        var content = new StringContent(payload, Encoding.UTF8, "application/json");

                        HttpResponseMessage resposta = await client.PostAsync(uriEmitir, content);                       

                        if (!resposta.IsSuccessStatusCode)                           
                        {
                            string respostaDetalhes = await resposta.Content.ReadAsStringAsync();
                            _validationResult.AddMensagem($"Erro ao gerar o boleto: {resposta.StatusCode} - {respostaDetalhes}", request.Boleto.SeuNumero);
                            return _validationResult;
                        }


                        var respostaData = await  resposta.Content.ReadFromJsonAsync<IncluirBoletoSicoobResponseDto>() ;                       

                        ((List<IncluirBoletoSicoobResponseDto>)_validationResult.Object).Add(respostaData);

                        _validationResult.AddMensagem("Boleto gerado com sucesso", request.Boleto.SeuNumero, tipo: ValidationMessageType.Sucesso);

                        if (requests.Count > 1)
                            await Task.Delay(TimeSpan.FromMilliseconds(500));
                    }

                }catch(Exception ex)
                {
                    _validationResult.AddMensagem($"Erro na validação da requisição: {ex.Message}", request.Boleto.SeuNumero);   
                    return _validationResult;
                }
            }

            return _validationResult;
          
        }

        public async Task<ValidationResult> AlterarDataDeVencimentoBoleto(EditarBoletoSicoobRequestDto request)
        {

            ValidationResult _validateResult = new();

            try
            {

                request.IsValid();

                string uriEmitir = ApiUrl + $"/boletos/{request.NossoNumero}";
             

                await ObterToken(request.ClientId);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{Token}");
                    client.DefaultRequestHeaders.Add("client_id", $"{request.ClientId}");

                    var payload = JsonSerializer.Serialize(request.Boleto);

                    var content = new StringContent(payload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response_detalhe = await client.PatchAsync(uriEmitir, content);
                  
                    if (!response_detalhe.IsSuccessStatusCode)
                    {
                        _validateResult.AddMensagem($"Erro: {response_detalhe.StatusCode} - {response_detalhe.ReasonPhrase}");
                        return _validateResult;
                    }

                    _validateResult.Object = response_detalhe.StatusCode;                   

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

                request.IsValid();

                string uriEmitir = ApiUrl + $"/boletos/{request.NossoNumero}";                

                await ObterToken(request.ClientId);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{Token}");
                    client.DefaultRequestHeaders.Add("client_id", $"{request.ClientId}");

                    var payload = JsonSerializer.Serialize(request.Boleto);

                    var content = new StringContent(payload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response_detalhe = await client.PatchAsync(uriEmitir, content);          

                    if (!response_detalhe.IsSuccessStatusCode)                   
                    {                       
                        _validateResult.AddMensagem($"Erro: {response_detalhe.StatusCode} - {response_detalhe.ReasonPhrase}");
                        return _validateResult;
                    }                 

                    _validateResult.Object = response_detalhe.StatusCode;

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
                request.IsValid();


                string uriEmitir = ApiUrl + $"/boletos/{request.NossoNumero}/baixar";               

                 await ObterToken(request.ClientId);

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

                request.IsValid();

                await ObterToken(request.ClienteId);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + $"{Token}");
                    client.DefaultRequestHeaders.Add("client_id", $"{request.ClienteId}");

                    string uriEmitir = ApiUrl + $"/boletos?numeroCliente={request.Body.NumeroCliente}&codigoModalidade={request.Body.CodigoModalidade}&nossoNumero={request.Body.NossoNumero}&linhaDigitavel={request.Body.LinhaDigitavel}&codigoBarras={request.Body.CodigoBarras}&numeroContratoCobranca={request.Body.NumeroContratoCobranca}";

                    HttpResponseMessage response_detalhe = await client.GetAsync(uriEmitir);

                    string resultado = await response_detalhe.Content.ReadAsStringAsync();

                    if (!response_detalhe.IsSuccessStatusCode)                  
                    {
                        _validateResult.AddMensagem($"Erro: {response_detalhe.StatusCode}: {resultado}");
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

                    string response_detalhes = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Erro na obtenção do token: {response.StatusCode}: {response_detalhes}");
                    }

                  
                    Token = response_detalhes;                   
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
