using System.Collections.Generic;

namespace OLS.BoletoNetCore
{
    public class Boletos : List<Boleto>
    {
        public IBanco Banco { get; set; }
    }
}
