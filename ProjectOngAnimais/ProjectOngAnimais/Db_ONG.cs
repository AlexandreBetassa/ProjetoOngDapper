using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace ProjectOngAnimais
{
    internal class Db_ONG
    {
        string conexao = "Data Source = DESKTOP-49RHHLK\\MSSQL;Initial Catalog=ong_adocao;Persist Security Info=True;User ID=sa;Password=834500";
        SqlConnection conn;

        public Db_ONG()
        {
            conn = new SqlConnection(conexao);
        }

        public void InsertTablePessoa(Pessoa pessoa)
        {
            conn.Open();
            string sql = "insert into dbo.pessoa (cpf, nome, sexo, telefone, endereco, dataNascimento, status) values (@cpf, @nome, @sexo, @telefone, @endereco, @dataNasc, @status);";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@cpf", pessoa.Cpf));
            cmd.Parameters.Add(new SqlParameter("@nome", pessoa.Nome));
            cmd.Parameters.Add(new SqlParameter("@sexo", pessoa.Sexo));
            cmd.Parameters.Add(new SqlParameter("@telefone", pessoa.Telefone));
            cmd.Parameters.Add(new SqlParameter("@endereco", pessoa.End));
            cmd.Parameters.Add(new SqlParameter("@dataNasc", pessoa.DataNascimento));
            cmd.Parameters.Add(new SqlParameter("@status", pessoa.Status));
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void SelectTablePessoa()
        {
            conn.Open();
            string sql = "Select cpf, nome, sexo, telefone, endereco, dataNascimento from pessoa where status = 'A'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            using (SqlDataReader r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    Console.WriteLine($"CPF: {r.GetString(0)}");
                    Console.WriteLine($"Nome: {r.GetString(1)}");
                    Console.WriteLine($"Sexo: {r.GetString(2)}");
                    Console.WriteLine($"Tel: {r.GetString(3)}");
                    Console.WriteLine($"Endereço: {r.GetString(4)}");
                    Console.WriteLine($"Data de nascimento: {r.GetDateTime(5).ToShortDateString()}");
                }
            }
            conn.Close();
        }

        public void DeleteDataPessoa(String cpf)
        {
            conn.Open();
            string sql = "update dbo.pessoa set status = 'I' where cpf = @cpf";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@cpf", cpf));
            cmd.ExecuteNonQuery();
        }

    }
}
