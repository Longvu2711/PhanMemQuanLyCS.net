using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Account
{
    internal class Connection
    {
        private static string stringConnection = @"Data Source=LAPTOP-LIGMABAL\MSSQLSERVER01;Initial Catalog=quanlyquay1;Integrated Security=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection);
        }
    }
}
