using System;
using Project_OnTheFly;

namespace ProjectOngAnimais
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }

        static void EditarCadastro()
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
    }
}
