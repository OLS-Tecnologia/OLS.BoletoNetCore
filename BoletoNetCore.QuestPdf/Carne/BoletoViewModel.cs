using OLS.BoletoNetCore;
using OLS.BoletoNetCore.QuestPdf;

namespace BoletoNetCore.QuestPdf.Carne
{
    public class BoletoViewModel
    {
        public string ParcelaInformativo { get; set; }
        public string DataVencimentoFormatada { get; set; }
        public string CodigoBeneficiarioFormatado { get; set; }
        public string EspecieMoeda { get; set; }
        public string ValorTituloFormatado { get; set; }
        public string NumeroDocumento { get; set; }
        public string NossoNumeroFormatado { get; set; }
        public string BeneficiarioCompleto { get; set; }
        public string PagadorCompleto { get; set; }

        public BoletoViewModel(Boleto boleto)
        {
            ParcelaInformativo = boleto.ParcelaInformativo;
            DataVencimentoFormatada = boleto.DataVencimento.ToDateStr();
            CodigoBeneficiarioFormatado = boleto.Banco.Beneficiario.CodigoFormatado;
            EspecieMoeda = boleto.EspecieMoeda;
            ValorTituloFormatado = boleto.ValorTitulo.FormatarMoeda();
            NumeroDocumento = boleto.NumeroDocumento;
            NossoNumeroFormatado = boleto.NossoNumeroFormatado;

            var ben = boleto.Banco.Beneficiario;
            BeneficiarioCompleto = $"Beneficiário: {ben.Nome} - {ben.Endereco.LogradouroEndereco}, {ben.Endereco.LogradouroNumero} - {ben.Endereco.Bairro} - {ben.Endereco.Cidade} - {ben.Endereco.UF} - {ben.Endereco.CEP.FormatarCep()} - CNPJ: {ben.CPFCNPJ.MascararCpfCnpj()}";

            var pag = boleto.Pagador;
            var cpfCnpj = pag.CPFCNPJ.IsCnpj() ? "CNPJ" : "CPF";
            PagadorCompleto = $"Pagador: {pag.Nome} - {cpfCnpj}: {pag.CPFCNPJ.MascararCpfCnpj()}";
        }
    }
}
