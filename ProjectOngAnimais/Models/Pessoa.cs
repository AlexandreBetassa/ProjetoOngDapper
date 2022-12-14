using System;

namespace Models
{
    public class Pessoa
    {
        public readonly static string INSERT = "INSERT INTO dbo.pessoa (cpf, nome, sexo, telefone, endereco, status, dataNasc, nChip) VALUES (@cpf, @nome, @sexo, @telefone, @endereco, @status, @dataNasc, @nChip)";
        public readonly static string SELECT = "SELECT cpf, nome, sexo, telefone, endereco, status, dataNasc, NChip FROM dbo.pessoa";
        public readonly static string UPDATE = "UPDATE dbo.pessoa SET nome = @nome, telefone = @telefone, endereco = @endereco, status = @status WHERE cpf = @cpf";
        public readonly static string DELETE = "DELETE FROM dbo.pessoa WHERE cpf = @cpf";


        public String Cpf { get; set; }
        public String Nome { get; set; }
        public String Sexo { get; set; }
        public String Telefone { get; set; }
        public String Endereco { get; set; }
        public String Status { get; set; }
        public DateTime DataNasc { get; set; }
        public int NChip { get; set; }

        public override string ToString()
        {
            return $"Nome: {Nome}\nCPF: {Cpf}\nSexo: {Sexo}\nTelefone: {Telefone}\nEndereço: {Endereco}\n" +
                   $"Data de nascimento: {DataNasc.ToShortDateString()}\nStatus Cadastro: {Status}\n" +
                   $"Número do chip do animal adotado: {NChip}";
        }
    }
}