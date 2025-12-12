using BoletoNetCore.Cobrancas.Providers.BaseProvider.Entities;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Inter;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Inter.Entities;
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
        private InterProvider provider = new InterProvider(); // TODO: Usar a factory aqui

        public string ArquivoCertificado = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Certificado.crt";
        public string ArquivoChave = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Chave.key";
        public string ClientId = "32d83ffa-ba06-44a3-9ef3-c0736b15e209";
        public string ClientSecret = "732171c2-391c-4baf-a632-8d31a449d171";

        [Test]
        public async Task EmitirCobrancaSucesso()
        {
            var pagador = new PagadorInter("63037800674", TipoPessoa.FISICA, "Wanderson", "35030446", UfBrasil.MG, "Governador Valadares",
                "Pedro Lessa", "23", "33", "3366555", "Casa", "teste@gmail.com");               
               

            var body = new EmitirBoletoInterRequestBody(
                "123457", // seuNumero
                2.5, // ValorNominal
                new DateOnly(2026, 09, 07), // DataVencimento
                60, // numDiasAgenda - número de dias após o vencimento para cancelamento do boleto
                pagador
             );

            try
            {

                var interRequest = new EmitirBoletoInterRequestDto("1154", body, ClientId, ClientSecret, ArquivoCertificado, ArquivoChave);               
              

                var result = await provider.EmitirBoleto(interRequest);

                Assert.IsInstanceOf(typeof(RecuperarCobrancaInterResponse), result.Object);

            }
            catch (Exception ex) { }
            
          
        }

        [Test]
        public async Task BuscarCobrancaSucesso()
        {
            var ConsultarBoletoRequest = new ConsultarBoletoInterRequestDto(
                "84fd78ac-55b8-454d-a4e1-d4357fc48f80", 
                "",
                ClientId,
                ClientSecret,
                ArquivoCertificado,
                ArquivoChave
             );         


            var result = await provider.ConsultaBoleto(ConsultarBoletoRequest);

           
            Assert.IsInstanceOf(typeof(RecuperarCobrancaInterResponse), result.Object);

        }

        //[Test]
        //public async Task CancelarCobrancaSucesso()
        //{

        //    CancelarBoetoBody requestBody = new("Cancelamento teste");

        //    CancelamentoBoletoInterRequestDto request = new(
        //        "e85ef5d3-3161-4c06-bc1e-de5237e99592",
        //        requestBody,
        //        ClientId,
        //        ClientSecret,
        //        ArquivoCertificado,
        //        ArquivoChave                
        //      );
           

        //    var result = await provider.BaixarBoleto(request);
            
        //    Assert.AreEqual(HttpStatusCode.Accepted, result);
        //}

        [Test]
        public async Task AtualizarValorBoletoSucesso()
        {
            double novoValor = 5.5;

            AtualizarBoletoBody body = new AtualizarBoletoBody(novoValor);
         

            var request = new AtualizarboletoInterRequestDto(
               "84fd78ac-55b8-454d-a4e1-d4357fc48f80",
                "1234Conta",
                body,
                ClientId,
                ClientSecret,
                ArquivoCertificado,
                ArquivoChave
             );

            var response = await provider.AlterarDataDeVencimentoBoleto(request);


            Assert.IsInstanceOf(typeof(AtualizarBoletoInterResponseDto), response.Object);

        }

        [Test]
        public async Task AtualizarDataVencimentoBoletoSucesso()
        {
            DateOnly dataVencimento = new DateOnly();

            AtualizarBoletoBody body = new AtualizarBoletoBody(dataVencimento);


            var request = new AtualizarboletoInterRequestDto(
               "84fd78ac-55b8-454d-a4e1-d4357fc48f80",
                "1234Conta",
                body,
                ClientId,
                ClientSecret,
                ArquivoCertificado,
                ArquivoChave
             );

            var response = await provider.AlterarDataDeVencimentoBoleto(request);


            Assert.IsInstanceOf(typeof(AtualizarBoletoInterResponseDto), response.Object);

        }

    }
}
