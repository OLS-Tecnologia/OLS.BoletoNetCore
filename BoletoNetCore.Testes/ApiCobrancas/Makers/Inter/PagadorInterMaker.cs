using BoletoNetCore.Cobrancas.Providers.Inter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Testes.ApiCobrancas.Makers.Inter
{
    public  class PagadorInterMaker
    {
        public static PagadorInter MakePagador()
        {
            return new PagadorInter()
            {
                CpfCnpj = "63037800674",
                TipoPessoa = "FISICA",
                Nome = "Wanderson",
                Cep = "35030446",
                Uf = "MG",
                Cidade = "Governador Valadares",
                Endereco = "Pedro Lessa",
                Bairro = "Lourdes",
                Numero = "1685",
                Ddd = "33",
                Telefone = "666666555",
                Complemento = "Casa",
                Email = "teste@gmail.com"
            };
        }
    }
}
