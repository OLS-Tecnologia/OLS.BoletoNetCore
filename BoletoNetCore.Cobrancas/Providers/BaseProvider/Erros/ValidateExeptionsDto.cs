using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoNetCore.Cobrancas.Providers.BaseProvider.Erros
{
    public class ValidationExceptionsDto : Exception
    {
        public IReadOnlyCollection<string> Errors { get; }

        public ValidationExceptionsDto(IEnumerable<string> errors)
            : base("Um ou mais erros de validação ocorreram.")
        {
            Errors = errors.ToList().AsReadOnly();
        }
    }
}
