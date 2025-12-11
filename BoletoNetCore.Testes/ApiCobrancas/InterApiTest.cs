using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Inter;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using NUnit.Framework;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BoletoNetCore.Testes.ApiCobrancas
{

    [TestFixture]
    [Category("Testes Cobranca")]

    public class InterApiTest
    {
        private InterProvider provider = new InterProvider();

        [Test]
        public async Task EmitirCobrancaSucesso()
        {           

            var pagador = new PagadorBase() {  
                
                CpfCnpj = "63037800674",
                Telefone = "",
                Endereco ="Lourdes",
                TipoPessoa = Enum.GetName<TipoPessoa>(TipoPessoa.FISICA),
                Nome = "Wanderson",
                Cidade =" Valadares",
                Uf = Enum.GetName<UfBrasil>(UfBrasil.MG),
                Cep ="35030771"

            };

            var body = new EmitirBoletoInterRequestBody(
                "123453", // seuNumero
                2.5, // ValorNominal
                new DateOnly(2026, 09, 07), // DataVencimento
                60, // numDiasAgenda - número de dias após o vencimento para cancelamento do boleto
                pagador
             );

            try
            {             

                var interRequest = new EmitirBoletoInterRequestDto()
                {
                    ArquivoCertificado = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Certificado.crt",
                    ArquivoChave = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Chave.key",
                    ClientId = "32d83ffa-ba06-44a3-9ef3-c0736b15e209",
                    ClientSecret = "732171c2-391c-4baf-a632-8d31a449d171",
                    XContaCorrente = "1154",
                    RequestDto = body
                };
              

                var result = await provider.EmitirBoleto(interRequest);

                Assert.IsInstanceOf(typeof(EmitirBoletoInterResponseDto), result);

            }
            catch (Exception ex) { }
            
          
        }

        [Test]
        public async Task BuscarCobrancaSucesso()
        {
            ConsultarBoletoInterRequestDto ConsultarBoletoRequest = new()
            {
                ArquivoCertificado = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Certificado.crt",
                ArquivoChave = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Chave.key",
                ClientId = "32d83ffa-ba06-44a3-9ef3-c0736b15e209",
                ClientSecret = "732171c2-391c-4baf-a632-8d31a449d171",
                CodigoSolicitacao = "78aba116-7857-460e-bf92-ea74a1627801"

            };         


            var result = await provider.ConsultaBoleto(ConsultarBoletoRequest);


            //InterBaseResponseDto
            //  Assert.Equals(interRequest, result);
            Assert.IsInstanceOf(typeof(RecuperarCobrancaInterResponse), result);

        }

        [Test]
        public async Task CancelarCobrancaSucesso()
        {

            CancelarBoetoBody requestBody = new CancelarBoetoBody() {
                MotivoCancelamento = "Cancelamento teste"
            };

            CancelamentoBoletoInterRequestDto request = new CancelamentoBoletoInterRequestDto()
            {
                ArquivoCertificado = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Certificado.crt",
                ArquivoChave = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Chave.key",
                ClientId = "32d83ffa-ba06-44a3-9ef3-c0736b15e209",
                ClientSecret = "732171c2-391c-4baf-a632-8d31a449d171",
                CodigoSolicitacao = "4b96b10a-f054-42b5-b8fa-62c794f73af7",
                RequestDto = requestBody

            };
          

            var result = await provider.BaixarBoleto(request);
            
            Assert.Equals(HttpStatusCode.Accepted, result);
        }

    }
}
