using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace eCustomersAPI.Models
{
    public class DBContext
    {
        private readonly IConfiguration _configuration;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DataTable ExecuteReaderDT(string SqlQuery, string SqlConnectionName ) 
        {
            DataTable dtReturn = new DataTable();
            string sqlConnectionString = _configuration.GetConnectionString(SqlConnectionName);
            SqlConnection sqlConn = new SqlConnection(sqlConnectionString);
            sqlConn.Open();

            SqlCommand sqlCommand = new SqlCommand(SqlQuery, sqlConn);
            try
            {
                using ( SqlDataReader sdr = sqlCommand.ExecuteReader())
                {
                    dtReturn.Load(sdr);
                }                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConn.Close();
            }

            return dtReturn;
        }

        public Boolean ExecuteCommand(string SqlQuery, string SqlConnectionName )
        {
            Boolean status;
            string sqlConnectionString = _configuration.GetConnectionString("eCustomers");
            SqlConnection sqlConn = new SqlConnection(sqlConnectionString);
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand(SqlQuery, sqlConn);

            try
            {
                sqlCommand.ExecuteNonQuery();
                status = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConn.Close();
            }

            return status;
        }

        public string ExecuteScalar(string SqlQuery, string SqlConnectionName)
        {
           string scalarOutput = null;
            string sqlConnectionString = _configuration.GetConnectionString("eCustomers");
            SqlConnection sqlConn = new SqlConnection(sqlConnectionString);
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand(SqlQuery, sqlConn);

            try
            {
                scalarOutput = sqlCommand.ExecuteScalar().ToString();                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConn.Close();
            }

            return scalarOutput;
        }

    }
}
