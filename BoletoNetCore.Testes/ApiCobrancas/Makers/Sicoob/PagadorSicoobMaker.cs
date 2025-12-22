using BoletoNetCore.Cobrancas.Providers.Sicoob.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Testes.ApiCobrancas.Makers.Sicoob
{
    public  class PagadorSicoobMaker
    {
        public static PagadorSicoob MakePagador()
        {
            return new PagadorSicoob()
            {
                NumeroCpfCnpj = "61529233526",
                Nome = "Wanderson",
                Uf = "MG",
                Cidade = "Governador Valadares",
                Bairro = "Lourdes",
                Cep = "35030883",
                Endereco = "Rua Pedro Lessa"

            };
        }
    }
}
