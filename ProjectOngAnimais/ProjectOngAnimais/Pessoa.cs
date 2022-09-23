using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_OnTheFly;
using ProjectOngAnimais;

namespace ProjectOngAnimais
{
    internal class Pessoa
    {
        public String Cpf { get; set; }
        public String Nome { get; set; }
        public Char Sexo { get; set; }
        public String Telefone { get; set; }
        public Endereco End { get; set; }
        public Char Ativa { get; set; }
        public DateTime DataNascimento { get; set; }

        public Pessoa() { }

        public void CadastrarPessoa()
        {
            do
            {
                Nome = Utils.ColetarString("Informe seu nome: ");
                if (Nome.Length > 50) Console.WriteLine("Informe um nome que contenha menos de 50 caracteres");
                else break;
            } while (true);
            do Cpf = Utils.ColetarString("Informe seu CPF (Somente Números): ");
            while (!Utils.ValidarCpf(Cpf));
            do
            {
                DataNascimento = Utils.ColetarData("Informe sua data de nascimento: ");
                if (DataNascimento > DateTime.Now) Console.WriteLine("Informe uma data válida");
                else break;
            } while (true);
            do
            {
                Sexo = Utils.ColetarValorChar("Informe o sexo informado no RG (M  - Masculino) ou (F - Feminino)");
                if (Sexo != 'M' && Sexo != 'F') Console.WriteLine("Informe um valor válido...");
                else break;
            } while (true);

            Telefone = Utils.ColetarString("Informe o número do teledone com DDD: ");
            Ativa = 'A';
        }

        public override string ToString()
        {
            return $"Nome: {Nome}\nCPF: {Cpf.Substring(0, 3)}.{Cpf.Substring(3, 3)}.{Cpf.Substring(6, 3)}-{Cpf.Substring(9, 2)}\n" +
                $"Sexo: {Sexo}\nTelefone: ({Telefone.Substring(0, 2)}){Telefone.Substring(2, 5)}-{Telefone.Substring(7, 4)}\n" +
                $"Endereço: \nData de nascimento: {DataNascimento.ToShortDateString()}".ToString();
        }

    }
}
