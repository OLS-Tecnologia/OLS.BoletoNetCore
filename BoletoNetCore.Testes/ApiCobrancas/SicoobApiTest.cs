using BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using BoletoNetCore.Cobrancas.Providers.Sicoob;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Base;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Request;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Response;
using BoletoNetCore.Cobrancas.Providers.Sicoob.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BoletoNetCore.Testes.ApiCobrancas
{

    [TestFixture]
    [Category("Testes Cobranca")]
    public class SicoobApiTest
    {

        private SicoobProvider provider = new SicoobProvider("https://sandbox.sicoob.com.br/sicoob/sandbox/cobranca-bancaria/v3", "1301865f-c6bc-38f3-9f49-666dbcfc59c3");
                    
        private string ClientId = "9b5e603e428cc477a2841e2683c92d21";

        [Test]
        public async Task EmitirCobrancaSucesso()
        {

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
                /*
                 25546454,
                ModalidadeBoletoSicoob.SIMPLES_COM_REGISTRO,
                456789, 
                CodigoEspecieDocumentosEnum.DM,
                DateOnly.FromDateTime(DateTime.Today),
                "01234567890123456789", 
                2,
                2, 
                560.00, 
                new DateOnly(2025, 12,30),
                TipoDesconto.SEMDESCONTO, 
                TipoMultaSicoob.ISENTO,
                TipoJurosMoraSicoob.ISENTO,
                1, pagador,
                beneficiario
                 
                 */
            };


            var emitirCobrancaRequest = new EmitirBoletoSicoobResquetDto()
            {
                ClienteId = ClientId,
                Boleto = boletoBody
            };

            var response = await provider.EmitirBoleto(emitirCobrancaRequest);

            Assert.AreEqual(response.IsValid, true);

            Assert.IsInstanceOf(typeof(EmitirBoletoInterResponseDto),  response.Object);

        }

        [Test]
        public async Task BuscarCobrancaSucesso()
        {
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
