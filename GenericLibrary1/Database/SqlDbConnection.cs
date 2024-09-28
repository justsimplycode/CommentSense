using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GenericLibrary.Database
{
    public class SqlDbConnection : ISqlDbConnection
    {
        private string _sqlConnectionString = @"Server=tcp:kjdthvq9uh.database.windows.net,1433;Initial Catalog=CommentCategorization;Persist Security Info=False;User ID=rcazuresqladmin;Password=welcome2!Azure;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public SqlConnection SqlConnectionToDb { get; set; }

        public SqlDbConnection()
        {
            SqlConnectionToDb = new SqlConnection(_sqlConnectionString);
        }

        public void Open()
        {
            if (SqlConnectionToDb != null && SqlConnectionToDb.State == ConnectionState.Closed)
            {
                SqlConnectionToDb.Open();
            }
        }

        public void Close()
        {
            if (SqlConnectionToDb != null && SqlConnectionToDb.State == ConnectionState.Open)
            {
                SqlConnectionToDb.Close();
            }
        }
    }
}
