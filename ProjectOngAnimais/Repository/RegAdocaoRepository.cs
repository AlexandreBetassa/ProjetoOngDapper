using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Models;
using Services;

namespace Repository
{
    public class RegAdocaoRepository : IRegAdocaoRepository
    {
        public bool Insert(RegAdocao regAdocao)
        {
            bool result = false;
            using (SqlConnection db = DbOng.OpenConnection())
            {
                db.Execute(RegAdocao.INSERT, regAdocao);
                result = true;
            }
            return result;
        }

        public List<RegAdocao> Select()
        {
            using (SqlConnection db = DbOng.OpenConnection())
            {
                var lstRegAdocao = db.Query<RegAdocao>(RegAdocao.SELECT);
                return (List<RegAdocao>)lstRegAdocao;
            }
        }
    }
}
