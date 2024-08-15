using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Account
{
    class Acc
    {
        public Acc() 
        {
        }
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public List<Taikhoan> TaiKhoans(string query)
        {
            List<Taikhoan> TaiKhoans=new List<Taikhoan>();

            using(SqlConnection sqlConnection = Connection.GetSqlConnection() )
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand (query, sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                while ( dataReader.Read() ) 
                {
                    TaiKhoans.Add(new Taikhoan(dataReader.GetValue(1).ToString(),dataReader.GetValue(2).ToString()));
                }
                sqlConnection.Close();
            }
                
                return TaiKhoans;

        }

        public bool isCheckLogin(string query)
        {
            int countAccount = 0;

            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                countAccount = (int)sqlCommand.ExecuteScalar();
                
                sqlConnection.Close();
            }

            return countAccount !=0;

        }

        public int getDataFromQuery(string query)
        {
            int maso = -1;
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                SqlCommand command = new SqlCommand (query, sqlConnection);
                sqlConnection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        maso = reader.GetInt32(0);
                    }
                }


                sqlConnection.Close();
            }

            return maso;
        }

    }
}
