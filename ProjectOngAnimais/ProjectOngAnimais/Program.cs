using System;
using System.Data;

namespace ProjectOngAnimais
{
    internal class Program
    {
        static void Main(string[] args)
        {

            do
            {
                Console.Clear();
                Console.WriteLine("### ONG ADOTE UM PET ###\n### BEM VINDO ###");
                int op = Utils.ColetarValorInt("Informe a opção desejada (0 - Sair) (1 - Opções pessoas adotantes) (2 - Opções Pets) (3 - Nova adoção)");
                switch (op)
                {
                    case 0:
                        Console.WriteLine("Sair ...");
                        Environment.Exit(0);
                        break;
                    case 1:
                        MenuPessoas();
                        break;
                    case 2:
                        MenuPet();
                        break;

                    default:
                        break;
                }
            } while (true);
        }

        static void MenuPessoas()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("### OPÇÕES PESSOAS ADOTANTES ###");
                int op = Utils.ColetarValorInt("Informe a opção desejada (0 - Retornar) (1 - Cadastrar nova pessoa) (2 - Atualizar Dados) (3 - Inativar Cadastro) (4 - Listar Pessoas com Cadastro Ativo)");
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        Console.WriteLine("### CADASTRAR NOVA PESSOA ###");
                        Pessoa pessoa = new Pessoa();
                        pessoa.CadastrarPessoa();
                        break;
                    case 2:
                        Console.WriteLine("### ATUALIZAR DADOS ###");
                        Pessoa.EditarCadastroPessoa();
                        break;
                    case 3:
                        Console.WriteLine("### DELETAR PESSOA ###");
                        Pessoa.DeletarPessoa();
                        break;
                    case 4:
                        Console.WriteLine("### LISTAR TODAS AS PESSOAS COM CADASTRO ATIVO ###");
                        Db_ONG db = new Db_ONG();
                        db.SelectTablePessoa();
                        Utils.Pause();
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        static void MenuPet()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("### OPÇÕES PET ###");
                int op = Utils.ColetarValorInt("Informe a opção desejada (0 - Retornar) (1 - Cadastrar Novo Pet para adoção) ");
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        Console.WriteLine("### CADASTRAR NOVO PET PARA ADOÇÃO ###");
                        Pet pet = new Pet();
                        pet.CadastrarPet();
                        break;
                    //case 2:
                    //    Console.WriteLine("### ATUALIZAR DADOS ###");
                    //    Pessoa.EditarCadastroPessoa();
                    //    break;
                    //case 3:
                    //    Console.WriteLine("### DELETAR PESSOA ###");
                    //    Pessoa.DeletarPessoa();
                    //    break;
                    //case 4:
                    //    Console.WriteLine("### LISTAR TODAS AS PESSOAS COM CADASTRO ATIVO ###");
                    //    Db_ONG db = new Db_ONG();
                    //    db.SelectTablePessoa();
                    //    Utils.Pause();
                    //    break;
                    default:
                        break;
                }
            } while (true);
        }
    }
}
