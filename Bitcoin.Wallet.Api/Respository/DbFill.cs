using System.Configuration;
using System.Data.SqlClient;

namespace Bitcoin.Wallet.Api.Respository
{
    public class DbFill
    {
        public DbFill()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(_sqlScriptCreateDb, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = new SqlCommand(_sqlScriptCreateTables, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private static string _dbName = "Bitcoin";

        private string _sqlScriptCreateDb = $@"USE master
                                                IF NOT EXISTS(SELECT* FROM sysdatabases WHERE name= '{_dbName}')
                                                    BEGIN 
                                                        CREATE DATABASE[{_dbName}];
                                                    END;";

        private string _sqlScriptCreateTables = $@"
                                                USE {_dbName}                                                                          
                                                IF NOT EXISTS ( SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'BALANCE' )
	                                                CREATE TABLE [dbo].[BALANCE](
	                                                             [ACCOUNT_ADDRESS] [varchar](500) NULL,
	                                                             [AMOUNT] [decimal](18, 0) NULL
                                                              )

                                                IF NOT EXISTS ( SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'CONFIRMATION_LOGS' )
	                                                CREATE TABLE [dbo].[CONFIRMATION_LOGS](
	[ID] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[CONFIRMATION] [int] NULL,
	[ACCOUNT_ADDRESS] [varchar](500) NULL,
	[DATE] [date] DEFAULT GETDATE() NULL)

                                                IF NOT EXISTS ( SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'TRANSACTIONS' )
	                                               CREATE TABLE [dbo].[TRANSACTIONS](
	[ID] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[ACCOUNT_ADDRESS] [varchar](500) NULL,
	[AMOUNT] [decimal](18, 0) NULL,
	[CONFIRMATION] [int] DEFAULT 0 NULL,
	[DATE] [datetime] DEFAULT GETDATE() NULL,
	[IS_REQUEST] [int] DEFAULT 0 NULL,
	[FROM_WALLET_ID] [varchar](500) NULL)";
    }
}