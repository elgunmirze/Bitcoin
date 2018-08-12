using System;

namespace Bitcoin.Wallet.Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public int Confirmation { get; set; }
        public bool IsRequested { get; set; }
    }
}