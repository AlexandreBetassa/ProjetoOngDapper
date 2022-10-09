using System;

namespace Models
{
    public class Pessoa
    {
        public readonly static string INSERT = "INSERT INTO dbo.pessoa (cpf, nome, sexo, telefone, endereco, status, dataNasc) VALUES (@cpf, @nome, @sexo, @telefone, @endereco, @status, @dataNasc)";
        public readonly static string SELECT = "SELECT cpf, nome, sexo, telefone, endereco, status, dataNasc FROM dbo.pessoa";
        public readonly static string UPDATE = "UPDATE dbo.pessoa SET nome, sexo, telefone, endereco, status, dataNasc) VALUES (@cpf, @nome, @sexo, @telefone, @endereco, @status, @dataNasc)";
        public readonly static string DELETE = "INSERT INTO dbo.pessoa (cpf, nome, sexo, telefone, endereco, status, dataNasc) VALUES (@cpf, @nome, @sexo, @telefone, @endereco, @status, @dataNasc)";


        public String Cpf { get; set; }
        public String Nome { get; set; }
        public String Sexo { get; set; }
        public String Telefone { get; set; }
        public String End { get; set; }
        public String Status { get; set; }
        public DateTime DataNascimento { get; set; }

        public override string ToString()
        {
            return $"Nome: {Nome}\nCPF: {Cpf.Substring(0, 3)}.{Cpf.Substring(3, 3)}.{Cpf.Substring(6, 3)}-{Cpf.Substring(9, 2)}\n" +
                $"Sexo: {Sexo}\nTelefone: ({Telefone.Substring(0, 2)}){Telefone.Substring(2, 5)}-{Telefone.Substring(7, 4)}\n" +
                $"Endereço: {End}\nData de nascimento: {DataNascimento.ToShortDateString()}".ToString();
        }
    }
}
