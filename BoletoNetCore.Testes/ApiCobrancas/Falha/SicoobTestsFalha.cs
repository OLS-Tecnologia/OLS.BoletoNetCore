using BoletoNetCore.Cobrancas.Factory;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Base;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Enums;
using BoletoNetCore.Testes.ApiCobrancas.Makers.Sicoob;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BoletoNetCore.Testes.ApiCobrancas.Falha
{

    [TestFixture]
    [Category("Testes Cobranca falha")]
    public class SicoobApiTest
    {

        private string ClientId = "9b5e603e428cc477a2841e2683c92d21";
        private string UrlSandBox = "https://sandbox.sicoob.com.br/sicoob/sandbox/cobranca-bancaria/v3";

        [Test]
        [Description("Deve Falhar ao não passar dados obrigatórios ")]
        public async Task EmitirCobrancaFalha()
        {
            var provider = ProviderFactory.EmitirBoleto<EmitirBoletoSicoobResquetDto>(Bancos.Sicoob, UrlSandBox, "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

            var pagador = PagadorSicoobMaker.MakePagador();          
            var emitirCobrancaRequest = CobrancaSicoobMaker.MakeCobranca(pagador, ClientId);

            emitirCobrancaRequest.Boleto.SeuNumero = null;

            var listRequest = new List<EmitirBoletoSicoobResquetDto>() { emitirCobrancaRequest };

            var response = await provider.EmitirBoleto(listRequest);

            Assert.AreEqual(response.IsValid, false);
           

        }
               

        [Test]
        [Description("Deve falhar ao serem passados dados incompatíveis, com o campo CodigoModalidade do boleto")]
        public async Task CancelarCobrancaFalha()
        {
            var provider = ProviderFactory.BaixarBoleto<BaixarBoletoSicoobRequestDto>(Bancos.Sicoob, UrlSandBox, "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

            var boleto = new BaixarBoletoRequestBody()
            {
                NumeroCliente = 12,
                CodigoModalidade = 100 // código inexistente
            };

            var request = new BaixarBoletoSicoobRequestDto()
            {
                NossoNumero = "3456565",
                ClientId = "",//ClientId,
                Boleto = boleto
            };

            var response = await provider.BaixarBoleto(request);

            Assert.AreEqual(response.IsValid, false);        


        }

      
        [Test ]
        [Description("Deve falhar ao ser passada uma data de vencimento inválida(passada)")]
        public async Task AtualizarDataVencimentoBoletoFalha()
        {
            var provider = ProviderFactory.AtualizarBoleto<EditarBoletoSicoobRequestDto>(Bancos.Sicoob, UrlSandBox, "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

            var novoVencimento = new ProrrogacaoVencimentoSicoobBody()
            {
                DataVencimento = DateOnly.FromDateTime(new DateTime(2024, 12, 29))
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

            Assert.AreEqual(response.IsValid, false);
            Assert.AreEqual(response.Message.ToString(), "[ERRO] [ERRO] Data de vencimento da cobranca não pode ser menor que a data atual.\r\n\r\n");           

        }
       
        [Test ]
        [Description("Deve falhar ao ser passado um valor negativo")]
        public async Task AtualizarValorBoletoFalha()
        {
            var provider = ProviderFactory.AtualizarBoleto<EditarBoletoSicoobRequestDto>(Bancos.Sicoob, UrlSandBox, "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

            var novoValor = new AlterarValorNominalBody()
            {
                Valor = -34
            };

            var boleto = new EditarBoletoSicoobRequestBody()
            {
                CodigoModalidade = 1, //ModalidadeBoletoSicoob.SIMPLES_COM_REGISTRO,
                ValorNominal = novoValor
            };

            var request = new EditarBoletoSicoobRequestDto()
            {
                Boleto = boleto,
                ClientId = ClientId,
                NossoNumero = 12
            };

            var response = await provider.AlterarValorBoleto(request);

            Assert.AreEqual(response.IsValid, false);
            Assert.AreEqual(response.Message.ToString(), "[ERRO] [ERRO] Valor do boleto não pode ser negativo.\r\n\r\n");           

        }      
    }
}
