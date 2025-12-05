using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Sicoob.Enums
{
    public class SicoobStatusBoleto
    {

       public static Dictionary<string, int> Status = new Dictionary<string, int>() {
           // A documentação do Sicoob apresenta apenas os 3 primeiros status abaixo,
           // porém segui o padrão deles de usar string para mapear os outros possíveis status

            { "Liquidado", 1 } , { "Em aberto", 2 }, { "Baixado", 3 }, {"Cancelado", 5}, { "Atrasado" , 4}, {  "Expirado", 6}, { "Protesto", 9},
           {"Falha emissao" , 7 }, {"Em processamento", 8} 
        
       };       

    }
}
