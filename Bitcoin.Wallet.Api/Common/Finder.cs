using Bitcoin.Wallet.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bitcoin.Wallet.Api.Common
{
    public static class Finder
    {
        public static string RequestedFinder(IList<Transaction> transactions)
        {
            IEnumerable<int> ids = transactions.Where(p=>p.IsRequested==false).Select(p => p.Id);
            string requestedIds = "0";
            foreach(int i in ids)
            {
                requestedIds += ","+i.ToString();
            }

            return requestedIds;
        }
    }
}