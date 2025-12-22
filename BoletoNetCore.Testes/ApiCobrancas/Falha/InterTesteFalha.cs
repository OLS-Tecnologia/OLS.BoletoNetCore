using BoletoNetCore.Cobrancas.Factory;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Testes.ApiCobrancas.Makers.Inter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoletoNetCore.Testes.ApiCobrancas.Falha
{
    public class InterTesteFalha
    {
        public string ArquivoCertificado = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Certificado.crt";
        public string ArquivoChave = @"C:\Users\fabio\Downloads\Inter_API-Chave_e_Certificado\Sandbox_InterAPI_Chave.key";
        public string ClientId = "32d83ffa-ba06-44a3-9ef3-c0736b15e209";
        public string ClientSecret = "732171c2-391c-4baf-a632-8d31a449d171";
        private string UrlSandBox = "https://cdpj-sandbox.partners.uatinter.co";

        [Test, Order(1)]
        [Description("Deve Falhar ao não passar dados obrigatórios ")]
        public async Task EmitirCobrancaFalha()
        {

            var provider = ProviderFactory.EmitirBoleto<EmitirBoletoInterRequestDto>(Bancos.BancoInter, UrlSandBox);

            var pagador = PagadorInterMaker.MakePagador();
            pagador.CpfCnpj = null;

            var interRequest = EmitirCobrancaMaker.MakeCobrancaInter(pagador, ClientSecret, ArquivoCertificado, ArquivoChave, ClientId);

            var listRquest = new List<EmitirBoletoInterRequestDto>() { interRequest };

            var result = await provider.EmitirBoleto(listRquest);

            Assert.AreEqual(result.IsValid, false);
           
        }

        [Test, Order(2)]
        [Description("Deve Falhar se a cobranca não for encontrada")]
        public async Task BuscarCobrancaFalha()
        {

            var provider = ProviderFactory.ConsultarBoleto<ConsultarBoletoInterRequestDto>(Bancos.BancoInter, UrlSandBox);

            string cobrancaAtual = "d8a35f4-Inexistente";

            var ConsultarBoletoRequest = new ConsultarBoletoInterRequestDto()
            {
                CodigoSolicitacao = cobrancaAtual,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                ArquivoChave = ArquivoChave,
                ArquivoCertificado = ArquivoCertificado,
                XContaCorrente = ""
            };


            var result = await provider.ConsultaBoleto(ConsultarBoletoRequest);


            Assert.AreEqual(result.IsValid, false);

        }

        [Test, Order(3)]
        [Description("Deve Falhar se for passado valor negativo")]
        public async Task AtualizarValorBoletoFalha()
        {
            var provider = ProviderFactory.AtualizarBoleto<AtualizarboletoInterRequestDto>(Bancos.BancoInter, UrlSandBox);

            double novoValor = -2;
            string cobrancaAtual = "1-4ff2-8213-5fc39d8a35f4";

            AtualizarBoletoBody body = new AtualizarBoletoBody(novoValor);

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

            Assert.AreEqual(response.IsValid, false);
            Assert.AreEqual(response.Message.ToString(), "[ERRO] [ERRO] Valor não pode ser menor que zero.\r\n\r\n");

        }

        [Test, Order(4)]
        [Description("Deve Falhar se for passada uma data de vencimento passada")]
        public async Task AtualizarDataVencimentoBoletoFalha()
        {
            var provider = ProviderFactory.AtualizarBoleto<AtualizarboletoInterRequestDto>(Bancos.BancoInter, UrlSandBox);

            string cobrancaAtual = "fc39d8a35f4";

            DateOnly dataVencimento = DateOnly.FromDateTime(new DateTime(2022, 12, 30));

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

            Assert.AreEqual(response.IsValid, false);

            Assert.AreEqual(response.Message.ToString(), "[ERRO] [ERRO] Data de vencimento não pode ser anterior a data atual.\r\n\r\n");

        }


    }
}
