using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.BaseProvider.Enums
{  
    public enum TipoDesconto
    {
        SEMDESCONTO = 0,
        VALORFIXODATAINFORMADA = 1,
        PERCENTUALDATAINFORMADA = 2,
        VALORPORANTECIPACAODIACORRIDO = 3,
        VALORPORANTECIPACAODIAUTL = 4,
        PERCENTUALPORANTECIPACAODIACORRIDO = 5,
        PERCENTUALPORANTECIPACAODIAUTIL = 6
    }
    
}
 