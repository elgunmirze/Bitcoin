using System.Globalization;

namespace Bitcoin.Wallet.Api.Client.Models
{
    public class BitcoinValue
    {
        private const int SatoshisPerBitcoin = 100000000;
        private readonly decimal _btc;

        public BitcoinValue(decimal btc)
        {
            _btc = btc;
        }

        public long Satoshis
        {
            get { return (long) (_btc * SatoshisPerBitcoin); }
        }

        public decimal GetBtc() => _btc;

        public static BitcoinValue FromBtc(decimal btc) => new BitcoinValue(btc);


        public override string ToString() => _btc.ToString(CultureInfo.InvariantCulture);
    }
}
