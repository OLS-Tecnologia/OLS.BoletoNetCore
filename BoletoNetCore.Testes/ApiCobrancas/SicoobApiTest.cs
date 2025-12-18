using BoletoNetCore.Cobrancas.Factory;
using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Base;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BoletoNetCore.Testes.ApiCobrancas
{

    [TestFixture]
    [Category("Testes Cobranca")]
    public class SicoobApiTest
    {    
                    
        private string ClientId = "9b5e603e428cc477a2841e2683c92d21";

        [Test]
        public async Task EmitirCobrancaSucesso()
        {

            var provider = ProviderFactory.EmitirBoleto<EmitirBoletoSicoobResquetDto>(Bancos.Sicoob ,"https://sandbox.sicoob.com.br/sicoob/sandbox/cobranca-bancaria/v3", "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

            var pagador = new PagadorSicoob()
            {
                NumeroCpfCnpj = "61529233526",
                Nome = "Wanderson",
                Uf = "MG",
                Cidade = "Governador Valadares",
                Bairro = "Lourdes",
                Cep = "35030883",
                Endereco = "Rua Pedro Lessa"                
             
            };

            var beneficiario = new BeneficiarioFinalSicoob()
            {
                 Nome = "Paulo",
                 NumeroCpfCnpj = "43417424267"
            };

            var boletoBody = new IncluirBoletoSicoobRequestBody()
            {
                SeuNumero = "3243546",
                CodigoModalidade = 1,
                BeneficiarioFinal = beneficiario,
                Pagador = pagador,
                TipoDesconto = (int)TipoDesconto.SEMDESCONTO,
                TipoJurosMora = (int)TipoJurosMoraSicoob.ISENTO,
                TipoMulta = (int)TipoMultaSicoob.ISENTO,
                CodigoEspecieDocumento = Enum.GetName(CodigoEspecieDocumentosEnum.DM),
                DataEmissao = DateOnly.FromDateTime(DateTime.Today),
                DataVencimento = new DateOnly(2025, 12, 30),
                NumeroCliente = 25546454,
                NumeroParcela = 1,
                IdentificacaoDistribuicaoBoleto = 1,
                IdentificacaoEmissaoBoleto = 1,
                Valor = 500,
                NumeroContaCorrente = 12344
                
            };


            var emitirCobrancaRequest = new EmitirBoletoSicoobResquetDto()
            {
                ClienteId = ClientId,
                Boleto = boletoBody
            };
            var listRequest = new List<EmitirBoletoSicoobResquetDto>() { emitirCobrancaRequest };

            var response = await provider.EmitirBoleto(listRequest);

            Assert.AreEqual(response.IsValid, true);

            Assert.IsInstanceOf(typeof(IncluirBoletoSicoobResponseDto), ((List<IncluirBoletoSicoobResponseDto>)response.Object).FirstOrDefault());

        }

        [Test]
        public async Task BuscarCobrancaSucesso()
        {
            var provider = ProviderFactory.ConsultarBoleto<ConsultarBoletoRequestDto>(Bancos.Sicoob, "https://sandbox.sicoob.com.br/sicoob/sandbox/cobranca-bancaria/v3", "1301865f-c6bc-38f3-9f49-666dbcfc59c3");
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
            var provider = ProviderFactory.BaixarBoleto<BaixarBoletoSicoobRequestDto>(Bancos.Sicoob, "https://sandbox.sicoob.com.br/sicoob/sandbox/cobranca-bancaria/v3", "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

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
            var provider = ProviderFactory.AtualizarBoleto<EditarBoletoSicoobRequestDto>(Bancos.Sicoob, "https://sandbox.sicoob.com.br/sicoob/sandbox/cobranca-bancaria/v3", "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

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
            var provider = ProviderFactory.AtualizarBoleto<EditarBoletoSicoobRequestDto>(Bancos.Sicoob, "https://sandbox.sicoob.com.br/sicoob/sandbox/cobranca-bancaria/v3", "1301865f-c6bc-38f3-9f49-666dbcfc59c3");

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
    }
}
