using BoletoNetCore.Cobrancas.Factory;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Testes.ApiCobrancas.Makers.Inter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BoletoNetCore.Testes.ApiCobrancas.Sucesso
{

    [TestFixture]
    [Category("Testes Cobranca")]
    [NonParallelizable]

    public class InterApiTest
    {   

        public string ArquivoCertificado = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Certificado.crt";
        public string ArquivoChave = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Chave.key";
        public string ClientId = "32d83ffa-ba06-44a3-9ef3-c0736b15e209";
        public string ClientSecret = "732171c2-391c-4baf-a632-8d31a449d171";
        private string UrlSandBox = "https://cdpj-sandbox.partners.uatinter.co";
        private string CodigoCobranca { get; set; } = string.Empty ;

        [Test, Order(1)]
        public async Task EmitirCobrancaSucesso()
        {

            var provider = ProviderFactory.EmitirBoleto<EmitirBoletoInterRequestDto>(Bancos.BancoInter, UrlSandBox);

            var pagador = PagadorInterMaker.MakePagador();

            var interRequest = EmitirCobrancaMaker.MakeCobrancaInter(pagador, ClientSecret, ArquivoCertificado, ArquivoChave, ClientId);

            var listRquest = new List<EmitirBoletoInterRequestDto>() { interRequest };              

            var result = await provider.EmitirBoleto(listRquest);

            Assert.AreEqual(result.IsValid, true);

            Assert.IsInstanceOf(typeof(RecuperarCobrancaInterResponse), ((List<RecuperarCobrancaInterResponse>)result.Object).FirstOrDefault());

            CodigoCobranca = ((List<RecuperarCobrancaInterResponse>)result.Object).FirstOrDefault()?.Cobranca.CodigoSolicitacao;

        }

        [Test, Order(2)]
        public async Task BuscarCobrancaSucesso()
        {

            var provider = ProviderFactory.ConsultarBoleto<ConsultarBoletoInterRequestDto>(Bancos.BancoInter, UrlSandBox);

            string cobrancaAtual = CodigoCobranca ?? "ec683c5e-5c71-4ff2-8213-5fc39d8a35f4";

            var ConsultarBoletoRequest = new ConsultarBoletoInterRequestDto()
            {
                CodigoSolicitacao = cobrancaAtual,
                ClientId= ClientId,
                ClientSecret = ClientSecret,
                ArquivoChave = ArquivoChave,
                ArquivoCertificado= ArquivoCertificado,
                XContaCorrente = ""               
            };         


            var result = await provider.ConsultaBoleto(ConsultarBoletoRequest);

           
            Assert.IsInstanceOf(typeof(RecuperarCobrancaInterResponse), result.Object);

        }       

        [Test, Order(3)]
        public async Task AtualizarValorBoletoSucesso()
        {
            var provider = ProviderFactory.AtualizarBoleto<AtualizarboletoInterRequestDto>(Bancos.BancoInter, UrlSandBox);

            double novoValor = 5.5;
            string cobrancaAtual = CodigoCobranca ?? "ec683c5e-5c71-4ff2-8213-5fc39d8a35f4";

            AtualizarBoletoBody body = new AtualizarBoletoBody(novoValor);         

            var request = new AtualizarboletoInterRequestDto()
            {
                RequestDto = body,
                CodigoSolicitacao= cobrancaAtual,
                XContaCorrente = "123conta",
                ArquivoCertificado = ArquivoCertificado,
                ArquivoChave= ArquivoChave,
                ClientSecret = ClientSecret,
                ClientId = ClientId              
            };

            var response = await provider.AlterarDataDeVencimentoBoleto(request);


            Assert.IsInstanceOf(typeof(AtualizarBoletoInterResponseDto), response.Object);

        }

        [Test, Order(4)]
        public async Task AtualizarDataVencimentoBoletoSucesso()
        {

            var provider = ProviderFactory.AtualizarBoleto<AtualizarboletoInterRequestDto>(Bancos.BancoInter, UrlSandBox);

            string cobrancaAtual = CodigoCobranca ?? "ec683c5e-5c71-4ff2-8213-5fc39d8a35f4";
            DateOnly dataVencimento = DateOnly.FromDateTime(new DateTime(2026,12,30));

            AtualizarBoletoBody body = new AtualizarBoletoBody(dataVencimento);

            var request = new AtualizarboletoInterRequestDto()
            {
                RequestDto = body,
                CodigoSolicitacao = cobrancaAtual,
                XContaCorrente = "123conta",
                ArquivoCertificado = ArquivoCertificado,
                ArquivoChave = ArquivoChave,
                ClientSecret = ClientSecret,
                ClientId = ClientId
            };


            var response = await provider.AlterarDataDeVencimentoBoleto(request);

            Assert.IsInstanceOf(typeof(AtualizarBoletoInterResponseDto), response.Object);

        }

        [Test, Order(5)]
        public async Task CancelarCobrancaSucesso()
        {
            var provider = ProviderFactory.BaixarBoleto<CancelamentoBoletoInterRequestDto>(Bancos.BancoInter, UrlSandBox);

            string cobrancaAtual = CodigoCobranca ?? "ec683c5e-5c71-4ff2-8213-5fc39d8a35f4";

            CancelarBoetoBody requestBody = new("Cancelamento teste");

            CancelamentoBoletoInterRequestDto request = new()
            {
                CodigoSolicitacao = cobrancaAtual,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                ArquivoChave= ArquivoChave,
                ArquivoCertificado= ArquivoCertificado,
                XContaCorrente = "",
                RequestDto = requestBody              
            };


            var result = await provider.BaixarBoleto(request);

            Assert.AreEqual(HttpStatusCode.Accepted, result.Object);
        }

        [Test, Order(6)]
        public async Task EmitirLoteCobrancasSucesso()
        {

            var provider = ProviderFactory.EmitirBoleto<EmitirBoletoInterRequestDto>(Bancos.BancoInter, UrlSandBox);

            var pagador = PagadorInterMaker.MakePagador();

            string seuNumero = Random.Shared.Next(100000, 1000000).ToString();           

            var interRequest = EmitirCobrancaMaker.MakeCobrancaInter(pagador, ClientSecret, ArquivoCertificado, ArquivoChave, ClientId);
           

            var request2 = EmitirCobrancaMaker.MakeCobrancaInter(pagador, ClientSecret, ArquivoCertificado, ArquivoChave, ClientId);

            request2.ClientId = "3333333333";


            var listRquest = new List<EmitirBoletoInterRequestDto>() { interRequest, request2 };

            var result = await provider.EmitirBoleto(listRquest);

            Assert.AreEqual(result.IsValid, true);

            Assert.IsInstanceOf(typeof(RecuperarCobrancaInterResponse), ((List<RecuperarCobrancaInterResponse>)result.Object).FirstOrDefault());

            CodigoCobranca = ((List<RecuperarCobrancaInterResponse>)result.Object).FirstOrDefault()?.Cobranca.CodigoSolicitacao;

        }
    }
}
