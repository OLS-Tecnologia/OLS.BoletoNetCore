using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Inter;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Inter.Entities;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BoletoNetCore.Testes.ApiCobrancas
{

    [TestFixture]
    [Category("Testes Cobranca")]
    [NonParallelizable]

    public class InterApiTest
    {
        private InterProvider provider = new InterProvider("https://cdpj-sandbox.partners.uatinter.co"); // TODO: Usar a factory aqui

        public string ArquivoCertificado = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Certificado.crt";
        public string ArquivoChave = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Chave.key";
        public string ClientId = "32d83ffa-ba06-44a3-9ef3-c0736b15e209";
        public string ClientSecret = "732171c2-391c-4baf-a632-8d31a449d171";

        private string CodigoCobranca { get; set; } = string.Empty ;

        [Test, Order(1)]
        public async Task EmitirCobrancaSucesso()
        {
            var pagador = new PagadorInter()
            {
                CpfCnpj =  "63037800674", 
                TipoPessoa = "FISICA",
                Nome =  "Wanderson",
                Cep=  "35030446",
                Uf = "MG", 
                Cidade = "Governador Valadares",
                Endereco=  "Pedro Lessa",
                Bairro=   "Lourdes", 
                Numero=  "1685",
                Ddd = "33",
                Telefone= "666666555", 
                Complemento = "Casa",
                Email= "teste@gmail.com"
            };

            string seuNumero = Random.Shared.Next(100000, 1000000).ToString();

            var body = new EmitirBoletoInterRequestBody()
            {
              SeuNumero=  seuNumero, 
              ValorNominal=  2.5, 
              DataVencimento =   new DateOnly(2026, 09, 07), 
              NumDiasAgenda =  60, 
              Pagador=   pagador
            };
         

            var interRequest = new EmitirBoletoInterRequestDto()
            {
                RequestDto = body,
                ClientSecret = "", 
                ArquivoCertificado = ArquivoCertificado,
                ArquivoChave = ArquivoChave,
                ClientId = ClientId,
                XContaCorrente = "1234"
                
            };               
              

            var result = await provider.EmitirBoleto(interRequest);

            Assert.IsInstanceOf(typeof(RecuperarCobrancaInterResponse), result.Object);

            CodigoCobranca = ((RecuperarCobrancaInterResponse)result.Object).Cobranca.CodigoSolicitacao;

        }

        [Test, Order(2)]
        public async Task BuscarCobrancaSucesso()
        {
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
    }
}
