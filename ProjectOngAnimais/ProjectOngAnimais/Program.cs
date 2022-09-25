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
                int op = Utils.ColetarValorInt("Informe a opção desejada (0 - Sair) (1 - Opções pessoas adotantes) (2 - Opções Pets) (3 - Nova adoção) (4 - Listar Registro de adoções): ");
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
                int op = Utils.ColetarValorInt("Informe a opção desejada (0 - Retornar) (1 - Cadastrar nova pessoa) (2 - Atualizar Dados) " +
                    "(3 - Inativar Cadastro) (4 - Listar Pessoas com Cadastro Ativo): ");
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
                int op = Utils.ColetarValorInt("Informe a opção desejada (0 - Retornar) (1 - Cadastrar Novo Pet para adoção) (2 - Editar Dados do Pet) (3 - Listar Pets disponiveis para adoção): ");
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
            Console.WriteLine("### NOVA ADOÇÃO - SELEÇÃO DE CANDIDATO ADOTANTE ###");
            string cpf = BuscarPessoa(db);
            if (cpf == "0") return;
            int pet = BuscarPet(db);
            if (pet == 0) return;
            confirmacao = Utils.ColetarValorInt("Confirmar adoção? (1 - Sim) (2 - Não): ");
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
                Utils.Pause();
            }
            else
            {
                Console.WriteLine("Houve um problema na solicitação");
                Utils.Pause();
            }
        }

        static int BuscarPet(Db_ONG db)
        {
            int id, confirmacao;

            do
            {
                Console.Clear();
                Console.WriteLine("### NOVA ADOÇÃO - SELEÇÃO DE PET ###");
                string sqlPet = "select nChipPet, familiaPet, racaPet, sexoPet, nomePet from dbo.pet where disponivel = 'A'";
                db.SelectTablePet(sqlPet);
                id = Utils.ColetarValorInt("Informe o numero do chip de identificação do Pet desejado informado na listagem acima: ");
                sqlPet = $"select nChipPet, familiaPet, racaPet, sexoPet, nomePet from dbo.pet where nChipPet = {id} and disponivel = 'A'";
                if (db.SelectTablePet(sqlPet))
                {
                    do
                    {
                        confirmacao = Utils.ColetarValorInt("Confirmar seleção de Pet (0 - Cancelar) (1 - Sim): ");
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
                do
                {
                    confirmacao = Utils.ColetarValorInt("Deseja realmente efetuar uma nova adoção (1 - Sim) (2 - Não): ");
                    if (confirmacao == 1) break;
                    else if (confirmacao == 2) return "0";
                    else Console.WriteLine("Selecione opção válida...");
                } while (true);

                string cpf, sql;

                do cpf = Utils.ColetarString("Informe o CPF da pessoa que deseja adotar: ");
                while (!Utils.ValidarCpf(cpf));
                sql = $"Select cpf, nome, sexo, telefone, endereco, dataNascimento from pessoa where cpf ='{cpf}' and status = 'A';";
                if (!db.SelectTablePessoa(sql)) return "0";
                confirmacao = Utils.ColetarValorInt("Confirmar candidato (1 - Sim) (2 - Não): ");
                if (confirmacao == 1) return cpf;
            } while (true);
        }

        static void ListarRegAdocoes()
        {
            Db_ONG db = new Db_ONG();
            string sql = "select p.cpf, p.nome, pt.nChipPet, pt.familiaPet, pt.racaPet, ra.dataAdocao from pessoa p, pet pt, regAdocao ra where ra.cpf = p.cpf and pt.nChipPet = ra.nChipPet;";
            db.SelectRegAdocao(sql);
            Utils.Pause();
        }
    }
}
