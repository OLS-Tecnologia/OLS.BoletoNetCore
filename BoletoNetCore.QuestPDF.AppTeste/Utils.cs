using OLS.BoletoNetCore;
using System;

namespace BoletoNetCore.QuestPDF.AppTeste
{
    internal sealed class Utils
    {
        private static int _contador = 1;

        private static int _proximoNossoNumero = 1;

        internal static 
            Beneficiario GerarBeneficiario(string codigoBeneficiario, string digitoCodigoBeneficiario, string codigoTransmissao, ContaBancaria contaBancaria)
        {
            return new Beneficiario
            {
                CPFCNPJ = "56.908.293/0001-64",
                Nome = "CARTAO DE DESCONTOS SAO MATHEUS LTDA",
                Codigo = codigoBeneficiario,
                CodigoDV = digitoCodigoBeneficiario,
                CodigoTransmissao = codigoTransmissao,
                Endereco = new Endereco
                {
                    LogradouroEndereco = " AV. MINAS GERAIS",
                    LogradouroNumero = "448",
                    LogradouroComplemento = "Cj 333",
                    Bairro = "CENTRO",
                    Cidade = "GOVERNADOR VALADARES",
                    UF = "MG",
                    CEP = "35010-151"
                },
                ContaBancaria = contaBancaria
            };
        }

        internal static Pagador GerarPagador()
        {
            if (_contador % 2 == 0)
                return new Pagador
                {
                    CPFCNPJ = "443.316.101-28",
                    Nome = "CONSUMIDOR",
                    Observacoes = "Matricula 678/9",
                    Endereco = new Endereco
                    {
                        LogradouroEndereco = "RUA PEDRO LESSA",
                        LogradouroNumero = "1645",
                        Bairro = "VILA BRETAS",
                        Cidade = " GOVERNADOR VALADARES",
                        UF = "MG",
                        CEP = "35030-080"
                    }
                };
            return new Pagador
            {
                CPFCNPJ = "443.316.101-28",
                Nome = "CONSUMIDOR",
                Observacoes = "Matricula 678/9",
                Endereco = new Endereco
                {
                    LogradouroEndereco = "RUA PEDRO LESSA",
                    LogradouroNumero = "1645",
                    Bairro = "VILA BRETAS",
                    Cidade = " GOVERNADOR VALADARES",
                    UF = "MG",
                    CEP = "35030-080"
                }
            };
        }

        internal static Boletos GerarBoletos(IBanco banco, int quantidadeBoletos, string aceite, int NossoNumeroInicial)
        {
            var boletos = new Boletos
            {
                Banco = banco
            };
            for (var i = 1; i <= quantidadeBoletos; i++)
                boletos.Add(GerarBoleto(banco, i, aceite, NossoNumeroInicial));
            return boletos;
        }

        internal static Boleto GerarBoleto(IBanco banco, int i, string aceite, int NossoNumeroInicial)
        {
            if (aceite == "?")
                aceite = _contador % 2 == 0 ? "N" : "A";

            var boleto = new Boleto(banco)
            {
                Pagador = GerarPagador(),
                DataEmissao = DateTime.Now.AddDays(-3),
                DataProcessamento = DateTime.Now,
                DataVencimento = DateTime.Now.AddMonths(5),
                ValorTitulo = (decimal)100 * i,
                NossoNumero = NossoNumeroInicial == 0 ? "" : (NossoNumeroInicial + _proximoNossoNumero).ToString(),
                NumeroDocumento = "BB" + _proximoNossoNumero.ToString("D6") + (char)(64 + i),
                EspecieDocumento = TipoEspecieDocumento.DM,
                Aceite = aceite,
                CodigoInstrucao1 = "11",
                CodigoInstrucao2 = "22",
                DataDesconto = DateTime.Now.AddMonths(i),
                ValorDesconto = (decimal)(100 * i * 0.10),
                DataDesconto2 = DateTime.Now.AddMonths(i).AddDays(2),
                ValorDesconto2 = (decimal)(100 * i * 0.12),
                DataDesconto3 = DateTime.Now.AddMonths(i).AddDays(3),
                ValorDesconto3 = (decimal)(100 * i * 0.13),
                DataMulta = DateTime.Now.AddMonths(i),
                PercentualMulta = (decimal)2.00,
                ValorMulta = (decimal)(100 * i * (2.00 / 100)),
                DataJuros = DateTime.Now.AddMonths(i),
                PercentualJurosDia = (decimal)0.2,
                ValorJurosDia = (decimal)(100 * i * (0.2 / 100)),
                AvisoDebitoAutomaticoContaCorrente = "2",
                MensagemArquivoRemessa = "Mensagem para o arquivo remessa",
                NumeroControleParticipante = "CHAVEPRIMARIA" + _proximoNossoNumero
            };
            // Mensagem - Instruções do Caixa
            boleto.ImprimirValoresAuxiliares = true;

            boleto.ValidarDados();
            _contador++;
            _proximoNossoNumero++;
            return boleto;
        }
    }
}