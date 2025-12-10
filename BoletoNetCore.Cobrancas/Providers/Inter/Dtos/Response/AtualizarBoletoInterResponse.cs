using BoletoNetCore.Cobrancas.Providers.BaseProvider.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.Inter.Dtos.Response
{
    public class AtualizarBoletoInterResponseDto : InterBaseResponseDto
    {

        [JsonProperty("status")]
        public StatusAtualizacaoBoletoInter Status {  get; set; }

        [JsonProperty("mensagem")]
        public string Mensagem {  get; set; }

        [JsonProperty("codigoEdicao")]
        public string CodigoEdicao {  get; set; }
    }


    public enum StatusAtualizacaoBoletoInter
    {
        PROCESSANDO = 1,
        SUCESSO = 2,
        FALHA = 3
    }
}
