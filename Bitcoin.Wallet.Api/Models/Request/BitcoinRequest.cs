namespace Bitcoin.Wallet.Api.Models.Request
{
    public class BitcoinRequest
    {
        public string Address { get; set; }

        public decimal Amount { get; set; }
    }
}