using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums
{
    public enum StatusBoletoEnum
    {
        RECEBIDO = 1,
        A_RECEBER = 2,
        MARCADO_RECEBIDO = 3,
        ATRASADO = 4,
        CANCELADO = 5,
        EXPIRADO = 6,
        FALHA_EMISSAO = 7,
        EM_PROCESSAMENTO = 8,
        PROTESTO = 9
       
    }
}
