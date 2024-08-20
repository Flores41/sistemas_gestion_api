﻿using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace DataAccess.CustomConnection
{
    public class CustomConnection :ICustomConnection
    {
        //9
        private string connectionString;
        private SqlConnection con;
        private SqlTransaction trx;


        public CustomConnection (string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbTransaction GetTransaction() => trx;

        public async Task<IDbConnection> BeginConnection(bool useTransaction=false)
        {
            if (this.con == null)
                this.con = new SqlConnection(this.connectionString);
            if (this.con.State != System.Data.ConnectionState.Open)
            {
                if (string.IsNullOrEmpty(this.con.ConnectionString))
                    this.con.ConnectionString = this.connectionString;

                await this.con.OpenAsync();
            }

            if (useTransaction)
            {     
                if (trx == null)
                {
                    this.trx = await Task.Run<SqlTransaction>(
                    () => con.BeginTransaction("CustomTransaction")
                    );
                }
            }

            return this.con;
        }
        public async Task Complete() { 
            if(this.trx!= null)
            {
                await Task.Run(() =>
                {
                    if (trx.Connection != null)
                        trx.Commit();
                });
            }

            await this.CloseConnection();

            SqlConnection.ClearAllPools();
        }
        public async Task Rollback()
        {
            if (trx != null)
            {
                await Task.Run(() => { trx.Rollback(); });
            }
        }
        public async Task CloseConnection()
        {
            await Task.Run(() =>
            {
                if (trx != null)
                    this.trx.Dispose();

                this.con.Close();
                this.con.Dispose();
            });
        }

        #region Command Async
        public async Task ExecuteCommandAsync(string commandText, params DbParameter[] values)
        {
            if (con == null)
                await BeginConnection();

            var cmd = new SqlCommand(commandText, this.con);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            if (this.trx != null)
                cmd.Transaction = this.trx;

            cmd.Parameters.AddRange(values);

            await cmd.ExecuteNonQueryAsync();
        }
        public async Task ExecuteCommandAsync(string commandText, string name, object value)
        {
            await ExecuteCommandAsync(commandText, new SqlParameter(name, value));
        }

        #endregion
        public async Task<IDataReader> ExecuteReaderAsync(string commandText, params DbParameter[] values)
        {
            if (con == null)
                await BeginConnection();

            var cmd = new SqlCommand(commandText, this.con);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            if (this.trx != null)
                cmd.Transaction = this.trx;

            cmd.Parameters.AddRange(values);

            var dr = await cmd.ExecuteReaderAsync();

            return dr;
        }

        public async Task<IDataReader> ExecuteReaderAsync(string commandText, string name, object value)
        {
            return await ExecuteReaderAsync(commandText, new SqlParameter(name, value));
        }
    }
}
