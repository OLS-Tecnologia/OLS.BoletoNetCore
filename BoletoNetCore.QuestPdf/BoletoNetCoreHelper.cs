using OLS.BoletoNetCore.QuestPdf;

namespace OLS.BoletoNetCore
{
    public static class BoletoNetCoreHelper
    {
        public static byte[] ImprimirCarnePdf(this Boletos listaBoletos)
        {
            return new BoletoCarne().BoletoPdf(listaBoletos);
        }
    }
}
