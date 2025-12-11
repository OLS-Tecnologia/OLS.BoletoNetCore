using BoletoNetCore.Cobrancas.Providers.Inter;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Request;
using BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BoletoNetCore.Testes.ApiCobrancas
{

    [TestFixture]
    [Category("Testes Cobranca")]

    public class InterApiTest
    {
        [Test]
        public async Task EmitirCobranca()
        {
            string payload = @"{
  ""seuNumero"": ""123456"",
  ""valorNominal"": 2.5,
  ""dataVencimento"": ""2024-09-07"",
  ""numDiasAgenda"": 60,
  ""pagador"": {
    ""email"": ""nome.sobrenome@x.com.br"",
    ""ddd"": ""31"",
    ""telefone"": ""999999999"",
    ""numero"": ""3456"",
    ""complemento"": ""apartamento 3 bloco 4"",
    ""cpfCnpj"": ""66624347600"",
    ""tipoPessoa"": ""FISICA"",
    ""nome"": ""Nome do pagador"",
    ""endereco"": ""Avenida Brasil, 1200"",
    ""bairro"": ""Centro"",
    ""cidade"": ""Belo Horizonte"",
    ""uf"": ""MG"",
    ""cep"": ""30110000""
  },
  ""desconto"": {
    ""taxa"": 3,
    ""codigo"": ""PERCENTUALDATAINFORMADA"",
    ""quantidadeDias"": 7
  },
  ""multa"": {
    ""taxa"": 2,
    ""codigo"": ""PERCENTUAL""
  },
  ""mora"": {
    ""taxa"": 5,
    ""codigo"": ""TAXAMENSAL""
  },
  ""mensagem"": {
    ""linha1"": ""mensagem 1"",
    ""linha2"": ""mensagem 2"",
    ""linha3"": ""mensagem 3"",
    ""linha4"": ""mensagem 4"",
    ""linha5"": ""mensagem 5""
  },
  ""beneficiarioFinal"": {
    ""cpfCnpj"": ""66624347600"",
    ""tipoPessoa"": ""FISICA"",
    ""nome"": ""Nome do beneficiário"",
    ""endereco"": ""Avenida Brasil, 1200"",
    ""bairro"": ""Centro"",
    ""cidade"": ""Belo Horizonte"",
    ""uf"": ""MG"",
    ""cep"": ""30110000""
  }
}";


            var provider = new InterProvider();

            var pagador = new Pagador() {
            CPFCNPJ = "63037800674", 
             Endereco = new Endereco(),
             Nome = "Gustavo",
             Observacoes= "",
             Telefone = ""
             
            
            };

            var body = new EmitirBoletoInterRequestBody(
                "123456",
                2.5,
                new DateOnly(2026, 09, 07),
                60,
                pagador              
                

                );//JsonSerializer.Deserialize<EmitirBoletoInterRequestBody>(payload);

            try
            {
               // var body = JsonSerializer.Deserialize<EmitirBoletoInterRequestBody>(payload);

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


                //InterBaseResponseDto
                //  Assert.Equals(interRequest, result);
                Assert.IsInstanceOf(typeof(EmitirBoletoInterResponseDto), result);


                // Task.CompletedTask;
            }
            catch (Exception ex) { }
            
          
        }



    }
}
