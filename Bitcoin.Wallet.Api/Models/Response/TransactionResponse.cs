using System;

namespace Bitcoin.Wallet.Api.Models.Response
{
    public class TransactionResponse
    {
        public DateTime Date { get; set; }

        public string Address { get; set; }

        public decimal Amount { get; set; }

        public int Confirmation { get; set; }
    }
}