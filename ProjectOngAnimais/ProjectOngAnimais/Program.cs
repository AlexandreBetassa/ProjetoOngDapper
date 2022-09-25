using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;

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
                        ListarRegAdocoes();
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
                    "(3 - Inativar Cadastro)\n(4 - Listar Pessoas com Cadastro Ativo)\n(5 - Listar cadastro de inativos)\n(6 - Buscar CPF)\n" +
                    "(7 - Reativar cadastro)\nInforme a opção: ");
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
                        Console.Clear();
                        Console.WriteLine("### ATUALIZAR DADOS ###");
                        Pessoa.EditarCadastroPessoa();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("### DELETAR PESSOA ###");
                        Pessoa.DeletarPessoa();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("### LISTAR TODAS AS PESSOAS COM CADASTRO ATIVO ###");
                        Db_ONG db = new Db_ONG();
                        string sql = "Select cpf, nome, sexo, telefone, endereco, dataNascimento from pessoa where status = 'A'";
                        db.SelectTablePessoa(sql);
                        Utils.Pause();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("### LISTAR TODAS AS PESSOAS COM CADASTRO INATIVO ###");
                        db = new Db_ONG();
                        sql = "Select cpf, nome, sexo, telefone, endereco, dataNascimento from pessoa where status = 'I'";
                        db.SelectTablePessoa(sql);
                        Utils.Pause();
                        break;
                    case 6:
                        Console.Clear();
                        string cpf;
                        do cpf = Utils.ColetarString("Informe o CPF a ser reativado: ");
                        while (!Utils.ValidarCpf(cpf));
                        BuscarCadastro(cpf);
                        Utils.Pause();
                        break;
                    case 7:
                        Console.Clear();
                        ReativarCadastro();
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
                int op = Utils.ColetarValorInt("Informe a opção desejada\n(0 - Retornar)\n(1 - Cadastrar Novo Pet para adoção)\n(2 - Editar Dados do Pet)\n" +
                    "(3 - Listar Pets disponiveis para adoção)\nInforme opção: ");
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        Console.WriteLine("### CADASTRAR NOVO PET PARA ADOÇÃO ###");
                        Pet pet = new Pet();
                        pet.CadastrarPet();
                        break;
                    case 2:
                        Console.WriteLine("### ATUALIZAR DADOS ###");
                        Pet.EditarPet();
                        break;
                    case 3:
                        Console.WriteLine("### LISTAR PET's DISPONIVEIS PARA ADOÇÃO ###");
                        Db_ONG db = new Db_ONG();
                        string sql = "select nChipPet, familiaPet, racaPet, sexoPet, nomePet from dbo.pet where disponivel = 'A'";
                        db.SelectTablePet(sql);
                        Utils.Pause();
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        static void NovaAdocao()
        {
            int confirmacao;
            Db_ONG db = new Db_ONG();

            Console.Clear();
            Console.WriteLine("### NOVA ADOÇÃO ###");
            int pet = BuscarPet(db);
            if (pet == 0) return;
            string cpf = BuscarPessoa(db);
            if (cpf == "0") return;
            confirmacao = Utils.ColetarValorInt("Confirmar adoção?\n(1 - Sim)\n(2 - Não)\nInforme opção: ");
            if (confirmacao != 1) return;
            else ConfirmarAdocao(cpf, pet, db);
            Utils.Pause();
        }

        static void ConfirmarAdocao(string cpf, int IDpet, Db_ONG db)
        {
            string sql = $"insert into dbo.regAdocao (cpf, nChipPet, dataAdocao) values('{cpf}','{IDpet}', '{DateTime.Now}')";
            if (db.InsertRegAdocao(sql) != 0)
            {
                sql = $"update dbo.pet set disponivel = 'I' where nChipPet = {IDpet}";
                db.UpdateTable(sql);
                Console.WriteLine("Adoção efetuada com sucesso!!!");
            }
            else Console.WriteLine("Houve um problema na solicitação");
        }

        static int BuscarPet(Db_ONG db)
        {
            int id, confirmacao;

            do
            {
                Console.Clear();
                Console.WriteLine("### NOVA ADOÇÃO - SELEÇÃO DE PET ###");
                string sqlPet = "select nChipPet, familiaPet, racaPet, sexoPet, nomePet from dbo.pet where disponivel = 'A'";
                if (!db.SelectTablePet(sqlPet)) return 0;
                id = Utils.ColetarValorInt("Informe o numero do chip de identificação do Pet desejado informado na listagem acima: ");
                sqlPet = $"select nChipPet, familiaPet, racaPet, sexoPet, nomePet from dbo.pet where nChipPet = {id} and disponivel = 'A'";
                if (db.SelectTablePet(sqlPet))
                {
                    do
                    {
                        confirmacao = Utils.ColetarValorInt("Confirmar seleção de Pet\n(0 - Cancelar)\n(1 - Sim)\nInforme opção: ");
                        if (confirmacao == 0) return 0;
                        else if (confirmacao == 1) return id;
                        else
                        {
                            Console.WriteLine("Opção Inválida...");
                            Utils.Pause();
                        }
                    } while (true);
                }
            } while (true);
        }

        static String BuscarPessoa(Db_ONG db)
        {
            do
            {
                int confirmacao;
                string cpf, sql;
                do cpf = Utils.ColetarString("Informe o CPF da pessoa que deseja adotar: ");
                while (!Utils.ValidarCpf(cpf));
                sql = $"Select cpf, nome, sexo, telefone, endereco, dataNascimento from pessoa where cpf ='{cpf}' and status = 'A';";
                if (!db.SelectTablePessoa(sql)) return "0";
                confirmacao = Utils.ColetarValorInt("Confirmar candidato\n(1 - Sim)\n(2 - Não)\nInforme opção: ");
                if (confirmacao == 1) return cpf;
            } while (true);
        }

        static void ListarRegAdocoes()
        {
            Db_ONG db = new Db_ONG();
            do
            {
                Console.Clear();
                Console.WriteLine("### REGISTROS DE ADOÇÕES ###");
                int op = Utils.ColetarValorInt("(0 - Retornar)\n(1 - Consultar todos os registros)\n(2 - Consultar por CPF)\n(3 - Consultar pelo chip de identificação): ");
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("### CONSULTA A TODOS OS REGISTROS DE ADOÇÕES ###");
                        string sql = "select ra.cpf, p.nome, ra.nChipPet, pt.familiaPet, pt.racaPet, ra.dataAdocao from pessoa p, pet pt, regAdocao ra where p.cpf = ra.cpf and pt.nChipPet = ra.nChipPet;";
                        db.SelectRegAdocao(sql);
                        Utils.Pause();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("### CONSULTA A REGISTROS DE ADOÇÕES POR CPF###");
                        string cpf = Utils.ColetarString("Informe o número do CPF do tutor: ");
                        sql = $"select ra.cpf, p.nome, ra.nChipPet, pt.familiaPet, pt.racaPet, ra.dataAdocao from pessoa p, pet pt, regAdocao ra where ra.cpf = {cpf} and p.cpf = ra.cpf and pt.nChipPet = ra.nChipPet;";
                        db.SelectRegAdocao(sql);
                        Utils.Pause();
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("### CONSULTA A REGISTROS DE ADOÇÕES PELO CHIP DO ANIMAL###");
                        int id = Utils.ColetarValorInt("Informe o número do Chip do animal: ");
                        sql = $"select ra.cpf, p.nome, ra.nChipPet, pt.familiaPet, pt.racaPet, ra.dataAdocao from pessoa p, pet pt, regAdocao ra where ra.nChipPet = {id} and p.cpf = ra.cpf and pt.nChipPet = ra.nChipPet;";
                        db.SelectRegAdocao(sql);
                        Utils.Pause();
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
            } while (true);
        }

        static bool BuscarCadastro(string cpf)
        {

            string sql = $"select cpf, nome, sexo, telefone, endereco, dataNascimento, status from dbo.pessoa where cpf = '{cpf}';";
            Db_ONG db = new Db_ONG();
            if (!db.SelectTablePessoaInativa(sql)) return false;
            else return true;
        }

        static void ReativarCadastro()
        {
            Db_ONG db = new Db_ONG();
            string cpf;
            do cpf = Utils.ColetarString("Informe o CPF a ser reativado: ");
            while (!Utils.ValidarCpf(cpf));
            if (!BuscarCadastro(cpf)) return;
            int op = Utils.ColetarValorInt("Confirmar reativação\n(1 - Sim)\n(2 - Não)\nInforme opção: ");
            if (op == 2) return;
            else
            {
                string sql = $"update dbo.pessoa set status = 'A' where cpf = {cpf}";
                if (db.UpdateTable(sql) != 0) Console.WriteLine("Cadastro reativado com sucesso");
                else Console.WriteLine("Erro na solicitação");
            }
        }
    }
}
