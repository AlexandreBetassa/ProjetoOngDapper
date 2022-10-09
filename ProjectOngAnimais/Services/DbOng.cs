using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DbOng : IDbOng
    {
        private readonly string _connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\..\\..\\..\\..\\ProjotoOngDapper\\ProjectOngAnimais\\Services\\DataBase\\DataBase.mdf;Integrated Security=True;Connect Timeout=30";

        public SqlConnection OpenConnection()
        {
            SqlConnection conn = new(_connection);
            conn.Open();
            return conn;
        }
    }
}
