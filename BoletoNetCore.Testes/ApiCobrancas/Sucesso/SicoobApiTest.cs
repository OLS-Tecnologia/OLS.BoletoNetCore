using BoletoNetCore.Cobrancas.Factory;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response;
using BoletoNetCore.Testes.ApiCobrancas.Makers.Sicoob;
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
    public class SicoobApiTest
    {    
                    
        private string ClientId = "9b5e603e428cc477a2841e2683c92d21";
        private string UrlSandBox = "https://sandbox.sicoob.com.br/sicoob/sandbox/cobranca-bancaria/v3";
      

        [Test]
        public async Task EmitirCobrancaSucesso()
        {

            var provider = ProviderFactory.EmitirBoleto<EmitirBoletoSicoobResquetDto>(Bancos.Sicoob , UrlSandBox, "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

            var pagador = PagadorSicoobMaker.MakePagador();

            var emitirCobrancaRequest = CobrancaSicoobMaker.MakeCobranca(pagador, ClientId);


            var listRequest = new List<EmitirBoletoSicoobResquetDto>() { emitirCobrancaRequest };

            var response = await provider.EmitirBoleto(listRequest);

            Assert.AreEqual(response.IsValid, true);

            Assert.IsInstanceOf(typeof(IncluirBoletoSicoobResponseDto), ((List<IncluirBoletoSicoobResponseDto>)response.Object).FirstOrDefault());

        }


        [Test]
        public async Task BuscarCobrancaSucesso()
        {
            var provider = ProviderFactory.ConsultarBoleto<ConsultarBoletoRequestDto>(Bancos.Sicoob, UrlSandBox, "1301865f-c6bc-38f3-9f49-666dbcfc59c3");
            var body = new ConsultarBoletoSicoobRequestBody()
            {
                CodigoModalidade = 1, //  ModalidadeBoletoSicoob.SIMPLES_COM_REGISTRO
                NossoNumero = 1,
            };

            var request = new ConsultarBoletoRequestDto()
            {
                Body = body,
                ClienteId  = ClientId
            };

            var response = await  provider.ConsultaBoleto(request);

            Assert.AreEqual(response.IsValid, true);

            Assert.IsInstanceOf(typeof(ConsultarBoletoResponseDto), response.Object);

        }

        [Test]
        public async Task CancelarCobrancaSucesso()
        {
            var provider = ProviderFactory.BaixarBoleto<BaixarBoletoSicoobRequestDto>(Bancos.Sicoob, UrlSandBox, "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

            var boleto = new BaixarBoletoRequestBody()
            {
                NumeroCliente= 12,
                CodigoModalidade = 1 // ModalidadeBoletoSicoob.SIMPLES_COM_REGISTRO
            };

            var request = new BaixarBoletoSicoobRequestDto()
            {
                NossoNumero= "3456565",
                ClientId= ClientId,
                Boleto = boleto
            };

            var response = await provider.BaixarBoleto(request);

            Assert.AreEqual(response.IsValid, true);

            Assert.AreEqual(response.Object, HttpStatusCode.NoContent);
        }

        [Test]
        public async Task AtualizarValorBoletoSucesso()
        {
            var provider = ProviderFactory.AtualizarBoleto<EditarBoletoSicoobRequestDto>(Bancos.Sicoob, UrlSandBox, "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

            var novoValor = new AlterarValorNominalBody()
            {
                Valor = 600
            };

            var boleto = new EditarBoletoSicoobRequestBody()
            {                 
                CodigoModalidade = 1, //ModalidadeBoletoSicoob.SIMPLES_COM_REGISTRO,
                ValorNominal = novoValor,            
            };

            var request = new EditarBoletoSicoobRequestDto()
            {
                Boleto = boleto,
                ClientId = ClientId,
                NossoNumero = 12
            };

            var response = await provider.AlterarValorBoleto(request);

            Assert.AreEqual(response.IsValid, true);

            Assert.AreEqual(HttpStatusCode.NoContent, response.Object);

        }

        [Test]
        public async Task AtualizarDataVencimentoBoletoSucesso()
        {
            var provider = ProviderFactory.AtualizarBoleto<EditarBoletoSicoobRequestDto>(Bancos.Sicoob, UrlSandBox, "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

            var novoVencimento = new ProrrogacaoVencimentoSicoobBody()
            {
                DataVencimento = DateOnly.FromDateTime(new DateTime(2026, 12, 29))
               
            };

            var boleto = new EditarBoletoSicoobRequestBody()
            {
                 CodigoModalidade = 1, //ModalidadeBoletoSicoob.SIMPLES_COM_REGISTRO,
                 ProrrogacaoVencimento = novoVencimento,                 
              
            };

            var request = new EditarBoletoSicoobRequestDto()
            {
                Boleto = boleto,
                ClientId = ClientId,
                NossoNumero = 12
              
            };

            var response = await provider.AlterarValorBoleto(request);

            Assert.AreEqual(response.IsValid, true);

            Assert.AreEqual(HttpStatusCode.NoContent, response.Object);

        }


        [Test]
        public async Task EmitirLoteCobrancasSucesso()
        {

            var provider = ProviderFactory.EmitirBoleto<EmitirBoletoSicoobResquetDto>(Bancos.Sicoob, UrlSandBox, "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

            var pagador = PagadorSicoobMaker.MakePagador();

            var emitirCobrancaRequest = CobrancaSicoobMaker.MakeCobranca(pagador, ClientId);

            var listRequest = new List<EmitirBoletoSicoobResquetDto>() { emitirCobrancaRequest, emitirCobrancaRequest };

            var response = await provider.EmitirBoleto(listRequest);

            Assert.AreEqual(response.IsValid, true);

            Assert.IsInstanceOf(typeof(IncluirBoletoSicoobResponseDto), ((List<IncluirBoletoSicoobResponseDto>)response.Object).FirstOrDefault());

        }
    }
}
