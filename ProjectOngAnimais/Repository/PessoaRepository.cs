using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using Dapper;
using Models;
using Services;
using Services.DataBaseConfiguration;

namespace Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly string _conn;

        public PessoaRepository()
        {
            _conn = DataBaseConfiguration.Get();
        }

        public bool Insert(Pessoa pessoa)
        {
            bool result = false;
            using (SqlConnection db = new(_conn))
            {
                db.Open();
                db.Execute(Pessoa.INSERT, pessoa);
                result = true;
            }
            return result;
        }

        public List<Pessoa> Select()
        {
            using (SqlConnection db = new(_conn))
            {
                db.Open();
                var lstPessoa = db.Query<Pessoa>(Pessoa.SELECT);
                return (List<Pessoa>)lstPessoa;
            }
        }

        public bool Update(Pessoa pessoa)
        {
            bool result = false;
            using (SqlConnection db = new(_conn))
            {
                db.Open();
                db.Execute(Pessoa.UPDATE, pessoa);
                result = true;
            }
            return result;
        }

        public bool Delete(Pessoa pessoa)
        {
            bool result = false;
            using (SqlConnection db = new(_conn))
            {
                db.Open();
                db.Execute(Pessoa.DELETE, pessoa);
                result = true;
            }
            return result;
        }
    }
}
