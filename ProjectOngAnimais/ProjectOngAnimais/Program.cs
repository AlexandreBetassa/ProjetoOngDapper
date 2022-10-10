using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Xml;
using Models;
using Repository;
using Controllers;

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
                int op = Utils.ColetarValorInt("Informe a opção desejada\n(0 - Sair)\n(1 - Opções pessoas adotantes)\n(2 - Opções Pets)\n" +
                    "(3 - Nova adoção)\n(4 - Listar Registro de adoções)\nInforme opção: ");
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
                    case 3:
                        NovaAdocao();
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        #region Menus
        static void MenuPessoas()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("### OPÇÕES PESSOAS ADOTANTES ###");
                int op = Utils.ColetarValorInt("Informe a opção desejada\n(1 - Atualizar Dados)\n" +
                    "(2 - Inativar Cadastro)\n(3 - Listar Pessoas com Cadastro Ativo)\n(4 - Listar cadastro de inativos)\nInforme: ");
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("### ATUALIZAR DADOS ###");
                        EditarCadastroPessoa();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("### DELETAR PESSOA ###");
                        DeletarPessoa();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("### LISTAR TODAS AS PESSOAS COM CADASTRO ATIVO ###");
                        new PessoaController().Select().Where(item => item.Status == "ATIVA").ToList().ForEach(Item => Console.WriteLine(Item));
                        Utils.Pause();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("### LISTAR TODAS AS PESSOAS COM CADASTRO INATIVO ###");
                        new PessoaController().Select().Where(item => item.Status != "ATIVA").ToList().ForEach(Item => Console.WriteLine(Item));
                        Utils.Pause();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("### DELETAR  CPF ###");
                        DeletarPessoa();
                        Utils.Pause();
                        break;
                    default:
                        Console.WriteLine("OPÇÃO NVÁLIDA");
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
                int op = Utils.ColetarValorInt("Informe a opção desejada\n(0 - Retornar)\n(1 - Cadastrar Novo Pet para adoção)\n(2 - Editar Dados do Pet)\n" +
                    "(3 - Listar Pets disponiveis para adoção)\nInforme opção: ");
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        Console.WriteLine("### CADASTRAR NOVO PET PARA ADOÇÃO ###");
                        CadastrarAnimal();
                        break;
                    case 2:
                        Console.WriteLine("### ATUALIZAR DADOS ###");
                        EditarPet();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("### PET's DISPONIVEIS PARA ADOÇÃO ###");
                        new AnimalController().Select().ForEach(item => Console.WriteLine(item));
                        Utils.Pause();
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        static void NovaAdocao()
        {
            Console.Clear();
            Console.WriteLine("### NOVA ADOÇÃO ###");
            int id;
            var lstAdocao = new AnimalController().Select().Where(Item => Item.Disponivel == "DISPONIVEL");
            if (!lstAdocao.Any()) Console.WriteLine("Não há animais para adoção cadastrados");
            else
            {
                do
                {
                    id = Utils.ColetarValorInt("Informe o número do chip do pet a ser adotado: ");
                    var pet = new AnimalController().Select().Where(item => item.NChip == id && item.Disponivel == "DISPONIVEL");
                    if (!pet.Any()) Console.WriteLine("Informe um ID válido");
                    else break;
                } while (true);
                CadastrarPessoa(id);
            }
            Utils.Pause();
        }
        #endregion Menus

        #region Pessoa
        public static void CadastrarPessoa(int id)
        {
            Pessoa pessoa = new();
            do
            {
                pessoa.Nome = Utils.ColetarString("Informe seu nome: ");
                if (pessoa.Nome.Length > 50) Console.WriteLine("Informe um nome que contenha menos de 50 caracteres");
                else break;
            } while (true);
            do pessoa.Cpf = Utils.ColetarString("Informe seu CPF: ").Replace("-", "").Replace(".", "");
            while (!Utils.ValidarCpf(pessoa.Cpf));
            do
            {
                pessoa.DataNasc = Utils.ColetarData("Informe sua data de nascimento: ");
                if (pessoa.DataNasc > DateTime.Now) Console.WriteLine("Informe uma data válida");
                else break;
            } while (true);

            pessoa.Sexo = Utils.ColetarString("Informe o sexo informado no RG (M  - Masculino) ou (F - Feminino): ");
            pessoa.Endereco = Utils.ColetarString("Informe seu endereço completo: ");
            pessoa.Telefone = Utils.ColetarString("Informe o número do teledone com DDD: ").Replace("(", "").Replace("-", "").Replace(")", "");
            pessoa.Status = "ATIVA";
            pessoa.NChip = id;
            new PessoaController().Insert(pessoa);
        }

        public static void EditarCadastroPessoa()
        {
            string cpf = Utils.ColetarString("Informe o CPF da pessoa a ser atualizada: ");
            var pessoa = new PessoaController().Select().Where(item => item.Cpf == cpf).First();
            Console.WriteLine("\n" + pessoa);

            int op = Utils.ColetarValorInt("INFORME A ALTERAÇÃO QUE DESEJA EFETUAR:\n(1 - Nome)\n(2 - Telefone)\n(3 - Endereco)\n(4 - Inativar cadastro)\nInforme:  ");
            switch (op)
            {
                case 0:
                    Console.WriteLine("Operação cancelada!!!");
                    Utils.Pause();
                    return;
                case 1:
                    pessoa.Nome = Utils.ColetarString("Informe o novo nome: ");
                    break;
                case 2:
                    pessoa.Telefone = Utils.ColetarString("Informe o novo telefone: ").Replace("(", "").Replace("-", "").Replace(")", "");
                    break;
                case 3:
                    pessoa.Endereco = Utils.ColetarString("Informe o novo endereço: ");
                    break;
                case 4:
                    pessoa.Status = Utils.ColetarString("Informe o novo status: ");
                    break;
                default:
                    Console.WriteLine("Opção inválida!!!");
                    break;
            }
            new PessoaController().Update(pessoa);
        }

        public static void DeletarPessoa()
        {
            Console.Clear();
            Console.WriteLine("### DELETAR PESSOA ###");
            string cpf = Utils.ColetarString("Informe o CPF da pessoa a ser Deletada: ");
            var pessoa = new PessoaController().Select().Where(item => item.Cpf == cpf).First();
            Console.WriteLine("\n" + pessoa);

            new PessoaController().Delete(pessoa);
        }

        #endregion Pessoa

        #region Animal
        public static void CadastrarAnimal()
        {
            Animal animal = new()
            {
                Familia = Utils.ColetarString("Informe a familia do PET: "),
                Raca = Utils.ColetarString("Informe a raça do pet: "),
                Sexo = Utils.ColetarString("Informe o sexo do PET (Opcional): "),
                Nome = Utils.ColetarString("Informe o nome do PET (Opcional): "),
                Disponivel = "DISPONIVEL",
            };
            new AnimalController().Insert(animal);
        }

        public static void EditarPet()
        {
            Console.Clear();
            Console.WriteLine("### EDITAR PET ###");
            int id = Utils.ColetarValorInt("Informe o número do Chip do pet que será alterado: ");
            var animal = new AnimalController().Select().Where(item => item.NChip == id).First();
            Console.WriteLine("\n" + animal);
            int op = Utils.ColetarValorInt("INFORME O QUE DESEJA ALTERAR\n(1 - Tipo do Pet)\n(2 - Raça)\n(3 - Sexo)" +
                "\n(4 - Nome)\n(5 - Alterar status)\nInforme: ");

            switch (op)
            {
                case 0:
                    return;
                case 1:
                    animal.Familia = Utils.ColetarString("informe a familia do pet: ");
                    break;
                case 2:
                    animal.Raca = Utils.ColetarString("Informe a raça: ");
                    break;
                case 3:
                    animal.Sexo = Utils.ColetarString("Informe o sexo do pet: ");
                    break;
                case 4:
                    animal.Nome = Utils.ColetarString("informe o nome do pet: ");
                    break;
                case 5:
                    animal.Disponivel = Utils.ColetarString("Informe o status do pet para adoção: ");
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
            new AnimalController().Update(animal);
        }

        public static void DeletarPet()
        {
            Console.Clear();
            Console.WriteLine("### EXCLUIR PET ###");
            int id = Utils.ColetarValorInt("Informe o número do Chip do pet que será excluido: ");
            var animal = new AnimalController().Select().Where(item => item.NChip == id).First();
            Console.WriteLine("\n" + animal);
            new AnimalController().Delete(animal);
        }
        #endregion Animal

    }
}
