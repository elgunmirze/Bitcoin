using Bitcoin.Wallet.Api.Interfaces;
using Bitcoin.Wallet.Api.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Bitcoin.Wallet.Api.Respository
{
    public class DbExecutor: IDbExecutor
    {
        private string _sqlConnection = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public async Task SaveTransactions(BitcoinQuery bitcoinQuery)
        {
            using (SqlConnection conn = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand(DbQueries.InsertTransactions, conn))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        await conn.OpenAsync();
                    }
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ACCOUNT_ADDRESS", bitcoinQuery.Address);
                    cmd.Parameters.AddWithValue("@AMOUNT", bitcoinQuery.Amount);
                    cmd.Parameters.AddWithValue("@FROM_WALLET_ID", string.Empty);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
           
        }

        public async Task<IList<Transaction>> GetLatestTransactions()
        {
            IList<Transaction> transactions = new List<Transaction>();
            using (SqlConnection conn = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand(DbQueries.GetLastTransactions, conn))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        await conn.OpenAsync();
                    }
                    cmd.CommandType = CommandType.Text;
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            transactions.Add(new Transaction
                            {
                                Id = Convert.ToInt32(reader["ID"]),
                                Address = (string)reader["ACCOUNT_ADDRESS"],
                                Amount = Convert.ToDecimal(reader["AMOUNT"]),
                                Confirmation = Convert.ToInt32(reader["CONFIRMATION"]),
                                Date = Convert.ToDateTime(reader["DATE"]),
                                IsRequested = Convert.ToInt32(reader["IS_REQUEST"]) == 0 ? false : true
                            });
                        }
                    }
                }
            }
            return transactions;
        }

        public async Task UpdateRequestedTransactions(string ids)
        {
            var updateSql = DbQueries.GetUpdateRequestSql(ids);
            
            using (SqlConnection conn = new SqlConnection(_sqlConnection))
            {
                using (var cmd = new SqlCommand(updateSql, conn))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        await conn.OpenAsync();
                    }
                    cmd.CommandType = CommandType.Text;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}