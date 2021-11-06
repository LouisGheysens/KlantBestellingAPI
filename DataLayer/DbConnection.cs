using System;
using Microsoft.Data.SqlClient;
namespace DataLayer {
    public static class DBConnection {
        private static SqlConnection _sqlConnection = null;

        public static SqlConnection Connection { get 
                { if (_sqlConnection == null) 
                    { _sqlConnection = CreateConnection();
                } return _sqlConnection; 
            } }


        public static SqlConnection CreateConnection() {
            return new SqlConnection(@"Data Source=DESKTOP-3CJB43N\SQLEXPRESS;Initial Catalog=WebAPI;Integrated Security=True");
        }
    }
}
