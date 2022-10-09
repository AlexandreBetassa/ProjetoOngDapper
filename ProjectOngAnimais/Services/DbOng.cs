using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DbOng
    {
        private static readonly string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Alexandre\\source\\repos\\ProjotoOngDapper\\ProjectOngAnimais\\Services\\DataBase\\DataBase.mdf;Integrated Security=True;Connect Timeout=30";

        public static SqlConnection OpenConnection()
        {
            SqlConnection conn = new(connection);
            conn.Open();
            return conn;
        }
    }
}
