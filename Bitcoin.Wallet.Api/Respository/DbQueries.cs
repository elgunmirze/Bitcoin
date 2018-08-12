namespace Bitcoin.Wallet.Api.Respository
{
    public static class DbQueries
    {
        public static string InsertTransactions = @"INSERT INTO TRANSACTIONS (ACCOUNT_ADDRESS,AMOUNT,FROM_WALLET_ID) 
VALUES (@ACCOUNT_ADDRESS,@AMOUNT,@FROM_WALLET_ID)";

        public static string GetUpdateRequestSql(string input)
        {
            return string.Format("UPDATE TRANSACTIONS SET IS_REQUEST = 1 WHERE ID IN ({0}) and IS_REQUEST = 0",input);
        }

        public static string GetLastTransactions = @"SELECT ID,ACCOUNT_ADDRESS,AMOUNT,CONFIRMATION,DATE,IS_REQUEST
   FROM TRANSACTIONS WHERE IS_REQUEST = 0 OR CONFIRMATION < 3";
    }
}