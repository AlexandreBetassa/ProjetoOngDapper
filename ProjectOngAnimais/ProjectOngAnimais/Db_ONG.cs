﻿using System;
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
            string sql = $"insert into dbo.pessoa (cpf, nome, sexo, telefone, endereco, dataNascimento, status) values ('{pessoa.Cpf}' , " +
                $"'{pessoa.Nome}', '{pessoa.Sexo}', '{pessoa.Telefone}', '{pessoa.End}', '{pessoa.DataNascimento}', '{pessoa.Status}');";
            SqlCommand cmd = new SqlCommand(sql, conn);
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
                    Console.WriteLine($"Nome: {r.GetString(1)}");
                    Console.WriteLine($"CPF: {r.GetString(0)}");
                    Console.WriteLine($"Sexo: {r.GetString(2)}");
                    Console.WriteLine($"Tel: {r.GetString(3)}");
                    Console.WriteLine($"Endereço: {r.GetString(4)}");
                    Console.WriteLine($"Data de nascimento: {r.GetDateTime(5).ToShortDateString()}");
                    Console.WriteLine();
                }
            }
            conn.Close();
        }

        public void DeleteDataPessoa(String cpf)
        {
            conn.Open();
            string sql = $"update dbo.pessoa set status = 'I' where cpf = '{cpf}'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateTable(String sql)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void InsertTablePet(Pet pet)
        {
            conn.Open();
            string sql = $"insert into dbo.pet (familiaPet, racaPet, sexoPet, nomePet, disponivel) " +
                $"values ('{pet.TipoPet}', '{pet.Raca}', '{pet.SexoPet}', '{pet.NomePet}', '{pet.PetDisponivel}');";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void SelectTablePet()
        {
            conn.Open();
            string sql = "select nChipPet, familiaPet, racaPet, sexoPet, nomePet from dbo.pet where disponivel = 'A'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            using (SqlDataReader r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    Console.WriteLine($"Chip de Identificação: {r.GetInt32(0)}");
                    Console.WriteLine($"Familia: {r.GetString(1)}");
                    Console.WriteLine($"Raça: {r.GetString(2)}");
                    Console.WriteLine($"Sexo: {r.GetString(3)}");
                    Console.WriteLine($"Nome: {r.GetString(4)}\n");
                }
            }
            conn.Close();
        }

    }
}