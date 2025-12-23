using OLS.BoletoNetCore;
using System;
using System.Diagnostics;
using System.IO;

namespace BoletoNetCore.QuestPDF.AppTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var currentDir = Directory.GetCurrentDirectory();
                Console.Write("Informe o diretório para gerar o pdf: ");
                currentDir = Console.ReadLine();
                if (!Directory.Exists(currentDir))
                    throw new Exception("O diretório informado não existe: " + currentDir);

                Console.WriteLine("Aguarde, gerando pdf...");

                var contaBancaria = new ContaBancaria
                {
                    Agencia = "0156",
                    Conta = "85305",
                    DigitoConta = "4",
                    CarteiraPadrao = "1",
                    TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
                    VariacaoCarteiraPadrao = "A",
                    TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
                    TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa,
                    OperacaoConta = "05"
                };
                var banco = Banco.Instancia(Bancos.Sicredi);
                banco.Beneficiario = Utils.GerarBeneficiario("85305", "", "", contaBancaria);
                banco.FormataBeneficiario();

                var quantidadeBoletos = 500;

                var boletos = Utils.GerarBoletos(banco, quantidadeBoletos, "N", 10);
                 
                var sw = new Stopwatch();
                sw.Start();
                var bytes = boletos.ImprimirCarnePdf();
                sw.Stop();

                Console.WriteLine($"{quantidadeBoletos} boletos gerados em {sw.ElapsedMilliseconds}ms | {(sw.ElapsedMilliseconds / 60000)} minuto(s)");

                Console.WriteLine("Pdf gerado, salvando arquivo...");
                var fileName = Path.Combine(currentDir, "carne.pdf");
                File.WriteAllBytes(fileName, bytes);
                Console.WriteLine("Pdf gerado com sucesso: " + fileName);                
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            
            }
        }
    }
}
