using System;
using System.Collections.Generic;
using System.Drawing;
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
        public String End { get; set; }
        public Char Status { get; set; }
        public DateTime DataNascimento { get; set; }

        public Pessoa() { }

        public void CadastrarPessoa()
        {
            Db_ONG db = new Db_ONG();
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

            End = Utils.ColetarString("Informe seu endereço completo: ");
            Telefone = Utils.ColetarString("Informe o número do teledone com DDD: ");
            Status = 'A';
            db.InsertTablePessoa(this);
        }


        static void EditarCadastroPessoa()
        {
            Db_ONG db = new Db_ONG();
            Console.WriteLine("Informe o CPF da pessoa que deseja atualizar: ");
            string cpf = Console.ReadLine();
            int op = Utils.ColetarValorInt("Informe o campo que deseja atualizar (0 - Cancelar) (1 - Nome) (2 - Telefone) (4 - Endereço): ");
            switch (op)
            {
                case 0:
                    Console.WriteLine("Operação cancelada!!!");
                    Utils.Pause();
                    return;
                case 1:
                    string nome = Utils.ColetarString("Informe o novo nome: ");
                    string sql = $"update dbo.pessoa set nome='{nome}' where cpf='{cpf}';";
                    db.UpdateDataPessoa(sql);
                    return;
                case 2:
                    string telefone = Utils.ColetarString("Informe o novo telefone: ");
                    sql = $"update dbo.pessoa set telefone = '{telefone}' where cpf = '{cpf}'";
                    db.UpdateDataPessoa(sql);
                    return;
                case 3:
                    string end = Utils.ColetarString("Informe o novo endereço: ");
                    sql = $"update dbo.pessoa set endereco = '{end}' where cpf = '{cpf}'";
                    db.UpdateDataPessoa(sql);
                    return;
                default:
                    Console.WriteLine("Opção inválida!!!");
                    break;
            }
        }

        public override string ToString()
        {
            return $"Nome: {Nome}\nCPF: {Cpf.Substring(0, 3)}.{Cpf.Substring(3, 3)}.{Cpf.Substring(6, 3)}-{Cpf.Substring(9, 2)}\n" +
                $"Sexo: {Sexo}\nTelefone: ({Telefone.Substring(0, 2)}){Telefone.Substring(2, 5)}-{Telefone.Substring(7, 4)}\n" +
                $"Endereço: {End}\nData de nascimento: {DataNascimento.ToShortDateString()}".ToString();
        }

    }
}
