using Bitcoin.Wallet.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitcoin.Wallet.Api.Interfaces
{
    public interface IDbExecutor
    {
        Task SaveTransactions(BitcoinQuery bitcoinQuery);

        Task<IList<Transaction>> GetLatestTransactions();

        Task UpdateRequestedTransactions(string ids);
    }
}
