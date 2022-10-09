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
                        new RegAdocaoRepository().Select().ForEach(item => Console.WriteLine(item));
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
                int op = Utils.ColetarValorInt("Informe a opção desejada\n(0 - Retornar)\n(1 - Cadastrar nova pessoa)\n(2 - Atualizar Dados)\n" +
                    "(3 - Inativar Cadastro)\n(4 - Listar Pessoas com Cadastro Ativo)\n(5 - Listar cadastro de inativos)\nInforme: ");
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        Console.WriteLine("### CADASTRAR NOVA PESSOA ###");
                        CadastrarPessoa();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("### ATUALIZAR DADOS ###");
                        EditarCadastroPessoa();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("### DELETAR PESSOA ###");
                        DeletarPessoa();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("### LISTAR TODAS AS PESSOAS COM CADASTRO ATIVO ###");
                        new PessoaRepository().Select().Where(item => item.Status == "ATIVA").ToList().ForEach(Item => Console.WriteLine(Item));
                        Utils.Pause();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("### LISTAR TODAS AS PESSOAS COM CADASTRO INATIVO ###");
                        new PessoaRepository().Select().Where(item => item.Status != "ATIVA").ToList().ForEach(Item => Console.WriteLine(Item));
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
                        new AnimalRepository().Select().ForEach(item => Console.WriteLine(item));
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
            string cpf = Utils.ColetarString("Informe o CPF do novo tutor: ");
            Pessoa pessoa = new PessoaRepository().Select().Where(item => item.Cpf == cpf && item.Status == "ATIVA").First();
            new AnimalRepository().Select().Where(Item => Item.Disponivel == "DISPONIVEL").ToList().ForEach(item => Console.WriteLine(item));
            int id = Utils.ColetarValorInt("Informe o número do chip do pet a ser adotado: ");
            Animal animal = new AnimalRepository().Select().Where(item => item.NChip == id && item.Disponivel == "DISPONIVEL").First();
            Console.WriteLine($"TUTOR\n{pessoa}\nANIMAL\n{animal}");
            RegAdocao regAdocao = new()
            {
                Cpf = cpf,
                NChip = id,
                Data = DateTime.Now
            };
            new RegAdocaoRepository().Insert(regAdocao);
        }

        //static void ConfirmarAdocao(string cpf, int IDpet, Db_ONG db)
        //{
        //    string sql = $"insert into dbo.regAdocao (cpf, nChipPet, dataAdocao) values('{cpf}','{IDpet}', '{DateTime.Now}')";
        //    if (db.InsertRegAdocao(sql) != 0)
        //    {
        //        sql = $"update dbo.pet set disponivel = 'I' where nChipPet = {IDpet}";
        //        db.UpdateTable(sql);
        //        Console.WriteLine("Adoção efetuada com sucesso!!!");
        //    }
        //    else Console.WriteLine("Houve um problema na solicitação");
        //}

        //static int BuscarPet(Db_ONG db)
        //{
        //    int id, confirmacao;

        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine("### NOVA ADOÇÃO - SELEÇÃO DE PET ###");
        //        string sqlPet = "select nChipPet, familiaPet, racaPet, sexoPet, nomePet from dbo.pet where disponivel = 'A'";
        //        if (!db.SelectTablePet(sqlPet)) return 0;
        //        id = Utils.ColetarValorInt("Informe o numero do chip de identificação do Pet desejado informado na listagem acima: ");
        //        sqlPet = $"select nChipPet, familiaPet, racaPet, sexoPet, nomePet from dbo.pet where nChipPet = {id} and disponivel = 'A'";
        //        if (db.SelectTablePet(sqlPet))
        //        {
        //            do
        //            {
        //                confirmacao = Utils.ColetarValorInt("Confirmar seleção de Pet\n(0 - Cancelar)\n(1 - Sim)\nInforme opção: ");
        //                if (confirmacao == 0) return 0;
        //                else if (confirmacao == 1) return id;
        //                else
        //                {
        //                    Console.WriteLine("Opção Inválida...");
        //                    Utils.Pause();
        //                }
        //            } while (true);
        //        }
        //    } while (true);
        //}

        //static String BuscarPessoa(Db_ONG db)
        //{
        //    do
        //    {
        //        int confirmacao;
        //        string cpf, sql;
        //        do cpf = Utils.ColetarString("Informe o CPF da pessoa que deseja adotar: ");
        //        while (!Utils.ValidarCpf(cpf));
        //        sql = $"Select cpf, nome, sexo, telefone, endereco, dataNascimento from pessoa where cpf ='{cpf}' and status = 'A';";
        //        if (!db.SelectTablePessoa(sql)) return "0";
        //        confirmacao = Utils.ColetarValorInt("Confirmar candidato\n(1 - Sim)\n(2 - Não)\nInforme opção: ");
        //        if (confirmacao == 1) return cpf;
        //    } while (true);
        //}

        //static void ListarRegAdocoes()
        //{
        //    Db_ONG db = new Db_ONG();
        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine("### REGISTROS DE ADOÇÕES ###");
        //        int op = Utils.ColetarValorInt("(0 - Retornar)\n(1 - Consultar todos os registros)\n(2 - Consultar por CPF)\n(3 - Consultar pelo chip de identificação)\nInforme opção: ");
        //        switch (op)
        //        {
        //            case 0:
        //                return;
        //            case 1:
        //                Console.Clear();
        //                Console.WriteLine("### CONSULTA A TODOS OS REGISTROS DE ADOÇÕES ###");
        //                string sql = "select ra.cpf, p.nome, ra.nChipPet, pt.familiaPet, pt.racaPet, ra.dataAdocao, pt.nomePet from pessoa p, pet pt, regAdocao ra where p.cpf = ra.cpf and pt.nChipPet = ra.nChipPet;";
        //                db.SelectRegAdocao(sql);
        //                Utils.Pause();
        //                break;

        //            case 2:
        //                Console.Clear();
        //                Console.WriteLine("### CONSULTA A REGISTROS DE ADOÇÕES POR CPF###");
        //                string cpf = Utils.ColetarString("Informe o número do CPF do tutor: ");
        //                sql = $"select ra.cpf, p.nome, ra.nChipPet, pt.familiaPet, pt.racaPet, ra.dataAdocao, pt.nomePet from pessoa p, pet pt, regAdocao ra where ra.cpf = {cpf} and p.cpf = ra.cpf and pt.nChipPet = ra.nChipPet;";
        //                db.SelectRegAdocao(sql);
        //                Utils.Pause();
        //                break;

        //            case 3:
        //                Console.Clear();
        //                Console.WriteLine("### CONSULTA A REGISTROS DE ADOÇÕES PELO CHIP DO ANIMAL###");
        //                int id = Utils.ColetarValorInt("Informe o número do Chip do animal: ");
        //                sql = $"select ra.cpf, p.nome, ra.nChipPet, pt.familiaPet, pt.racaPet, ra.dataAdocao, pt.nomePet from pessoa p, pet pt, regAdocao ra where ra.nChipPet = {id} and p.cpf = ra.cpf and pt.nChipPet = ra.nChipPet;";
        //                db.SelectRegAdocao(sql);
        //                Utils.Pause();
        //                break;
        //            default:
        //                Console.WriteLine("Opção inválida");
        //                break;
        //        }
        //    } while (true);
        //}

        //static bool BuscarCadastro(string cpf)
        //{

        //    string sql = $"select cpf, nome, sexo, telefone, endereco, dataNascimento, status from dbo.pessoa where cpf = '{cpf}';";
        //    Db_ONG db = new Db_ONG();
        //    if (!db.SelectTablePessoaInativa(sql)) return false;
        //    else return true;
        //}

        //static void ReativarCadastro()
        //{
        //    Db_ONG db = new Db_ONG();
        //    string cpf;
        //    do cpf = Utils.ColetarString("Informe o CPF a ser reativado: ");
        //    while (!Utils.ValidarCpf(cpf));
        //    if (!BuscarCadastro(cpf)) return;
        //    int op = Utils.ColetarValorInt("Confirmar reativação\n(1 - Sim)\n(2 - Não)\nInforme opção: ");
        //    if (op == 2) return;
        //    else
        //    {
        //        string sql = $"update dbo.pessoa set status = 'A' where cpf = {cpf}";
        //        if (db.UpdateTable(sql) != 0) Console.WriteLine("Cadastro reativado com sucesso");
        //        else Console.WriteLine("Erro na solicitação");
        //    }
        //}

        #region Pessoa
        public static void CadastrarPessoa()
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
            new PessoaRepository().Insert(pessoa);
        }

        public static void EditarCadastroPessoa()
        {
            string cpf = Utils.ColetarString("Informe o CPF da pessoa a ser atualizada: ");
            var pessoa = new PessoaRepository().Select().Where(item => item.Cpf == cpf).First();
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
            new PessoaRepository().Update(pessoa);
        }

        public static void DeletarPessoa()
        {
            string cpf = Utils.ColetarString("Informe o CPF da pessoa a ser Deletada: ");
            var pessoa = new PessoaRepository().Select().Where(item => item.Cpf == cpf).First();
            Console.WriteLine("\n" + pessoa);

            new PessoaRepository().Delete(pessoa);
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
                Disponivel = "Disponivel",
            };
            new AnimalRepository().Insert(animal);
        }

        public static void EditarPet()
        {
            Console.Clear();
            Console.WriteLine("### EDITAR PET ###");
            int id = Utils.ColetarValorInt("Informe o número do Chip do pet que será alterado: ");
            var animal = new AnimalRepository().Select().Where(item => item.NChip == id).First();
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
            new AnimalRepository().Update(animal);
        }

        public static void DeletarPet()
        {
            Console.Clear();
            Console.WriteLine("### EXCLUIR PET ###");
            int id = Utils.ColetarValorInt("Informe o número do Chip do pet que será excluido: ");
            var animal = new AnimalRepository().Select().Where(item => item.NChip == id).First();
            Console.WriteLine("\n" + animal);
            new AnimalRepository().Delete(animal);
        }
        #endregion Animal

    }
}
