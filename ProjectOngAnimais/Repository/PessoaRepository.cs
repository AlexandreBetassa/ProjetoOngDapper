using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Models;
using Services;

namespace Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        public bool Insert(Pessoa pessoa)
        {
            bool result = false;
            using (SqlConnection db = new DbOng().OpenConnection())
            {
                db.Execute(Pessoa.INSERT, pessoa);
                result = true;
            }
            return result;
        }

        public List<Pessoa> Select()
        {
            using SqlConnection db = new DbOng().OpenConnection();
            var lstPessoa = db.Query<Pessoa>(Pessoa.SELECT);
            return (List<Pessoa>)lstPessoa;
        }

        public bool Update(Pessoa pessoa)
        {
            bool result = false;
            using (SqlConnection db = new DbOng().OpenConnection())
            {
                db.Execute(Pessoa.UPDATE, pessoa);
                result = true;
            }
            return result;
        }

        public bool Delete(Pessoa pessoa)
        {
            bool result = false;
            using (SqlConnection db = new DbOng().OpenConnection())
            {
                db.Execute(Pessoa.DELETE, pessoa);
                result = true;
            }
            return result;
        }
    }
}
