using BoletoNetCore.QuestPdf.Carne;
using Microsoft.Extensions.FileProviders;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Linq;
using System.Reflection;

namespace OLS.BoletoNetCore.QuestPdf
{
    internal class BoletoCarne : IDocument
    {
        private Boletos listaBoletos;
        private int _codBanco;
        private byte[] _logo;

        public BoletoCarne()
        {            
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.MarginHorizontal(20);
                page.MarginVertical(20);
                page.Content().Element(this.ComposeContent);
            });
        }

        private byte[] ObterLogoBanco(IBanco banco)
        {
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            using (var reader = embeddedProvider.GetFileInfo($"logos/{banco.Codigo.ToString("000")}.bmp").CreateReadStream())
            {
                var logo = new byte[reader.Length];
                reader.Read(logo, 0, (int)reader.Length);
                return logo;
            }
        }

        private byte[] ObterLogoBanco(int codBanco)
        {
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            using (var reader = embeddedProvider.GetFileInfo($"logos/{codBanco.ToString("000")}.bmp").CreateReadStream())
            {
                var logo = new byte[reader.Length];
                reader.Read(logo, 0, (int)reader.Length);
                return logo;
            }
        }

        private void ComposeContent(IContainer container)
        {
            container.Stack(stack =>
            {
                byte[] logo = null;
                var codBanco = this.listaBoletos?.Select(x => x.Banco.Codigo).FirstOrDefault() ?? 0;

                _logo = ObterLogoBanco(codBanco);

                foreach (var bol in this.listaBoletos)
                {
                    //if (logo == null || codBanco != bol.Banco.Codigo)
                    //{
                    //    codBanco = bol.Banco.Codigo;
                    //    logo = this.ObterLogoBanco(codBanco);
                    //}                    

                    stack.Item().Row(row =>
                    {
                        row.ConstantColumn(100).Component(new ReciboLateralCarne(new BoletoViewModel(bol), _logo));
                        row.RelativeColumn().PaddingLeft(5).Component(new ConteudoBoleto(bol, _logo));
                    });

                    stack.Item().PaddingBottom(3).Text("Recibo do Pagador - Autenticar no Verso", BoletoPdfConstants.LabelStyle);
                    stack.Item().ExtendHorizontal().BorderHorizontal(BoletoPdfConstants.BorderSize);
                    stack.Item().Height(15).ExtendHorizontal();
                }
            });
        }

        public DocumentMetadata GetMetadata()
        {
            return DocumentMetadata.Default;
        }

        public byte[] BoletoPdf(OLS.BoletoNetCore.Boletos listaBoletos)
        {
            this.listaBoletos = listaBoletos;
            return this.GeneratePdf();
        }
    }
}