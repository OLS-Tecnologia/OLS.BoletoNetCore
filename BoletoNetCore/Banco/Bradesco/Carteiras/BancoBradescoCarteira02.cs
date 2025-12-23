using System;

namespace OLS.BoletoNetCore
{
    [CarteiraCodigo("02")]
    public class BancoBradescoCarteira02 : BancoBradescoCarteiraBase, ICarteira<BancoBradesco>
    {
        internal static Lazy<ICarteira<BancoBradesco>> Instance { get; } = new Lazy<ICarteira<BancoBradesco>>(() => new BancoBradescoCarteira02());

        private BancoBradescoCarteira02()
        {
        }
    }
}
