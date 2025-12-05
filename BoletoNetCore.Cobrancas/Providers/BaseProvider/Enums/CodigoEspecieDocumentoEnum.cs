using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums
{
    public enum CodigoEspecieDocumentosEnum
    {
        CH = 1,  // "Cheque" 
        DM = 2,  // "Duplicata Mercantil" 
        DMI = 3,  //  "Duplicata Mercantil Indicação" 
        DS = 4,  // "Duplicata de Serviço" 
        DSI = 5,  //  "Duplicata Serviço Indicação" 
        DR = 6,  // "Duplicata Rural" 
        LC = 7,  // "Letra de Câmbio" 
        NCC = 8,  //  "Nota de Crédito Comercial" 
        NCE = 9,  //  "Nota de Crédito Exportação" 
        NCI = 10,  //  "Nota de Crédito Industrial" 
        NCR = 11,  //  "Nota de Crédito Rural" 
        NP = 12,  // "Nota Promissória" 
        NPR = 13,  //  "Nota Promissória Rural" 
        TM = 14,  // "Triplicata Mercantil" 
        TS = 15,  // "Triplicata de Serviço" 
        NS = 16,  // "Nota de Seguro" 
        RC = 17,  // "Recibo" 
        FAT = 18,  //  "Fatura" 
        ND = 19,  // "Nota de Débito" 
        AP = 20,  // "Apólice de Seguro" 
        ME = 21,  // "Mensalidade Escolar" 
        PC = 22,  // "Pagamento de Consórcio" 
        NF = 23,  // "Nota Fiscal" 
        DD = 24,  // "Documento de Dívida" 
        CC = 25,  // "Cartão de Crédito" 
        BDP = 26,  //  "Boleto Proposta" 
        OU = 27  // "Outros"
    }
}
