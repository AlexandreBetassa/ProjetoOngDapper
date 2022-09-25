using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectOngAnimais;


namespace ProjectOngAnimais
{
    internal class Pet
    {
        public String TipoPet { get; set; }
        public String Raca { get; set; }
        public Char SexoPet { get; set; }
        public Char PetDisponivel { get; set; }
        public String NomePet { get; set; }

        public Pet() { }

        public void CadastrarPet()
        {
            Db_ONG db = new Db_ONG();
            do
            {
                TipoPet = Utils.ColetarString("Informe o tipo de PET: ");
                if (TipoPet.Length > 20) Console.WriteLine("Tamanho máximo da informação: 50 caracteres");
                else break;
            } while (true);

            do
            {
                Raca = Utils.ColetarString("Informe a raça do pet: ");
                if (Raca.Length > 20) Console.WriteLine("Tamanho máximo da informação: 20 caracteres");
                else break;
            } while (true);

            SexoPet = Utils.ColetarValorChar("Informe o sexo do PET (Opcional): ");
            Console.Write("Informe o nome do PET (Opcional): ");
            NomePet = Console.ReadLine();
            PetDisponivel = 'A';
            db.InsertTablePet(this);
        }

        public static void EditarPet()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("### EDITAR PET ###");
                int id = Utils.ColetarValorInt("Informe o número do Chip do pet que será alterado: ");
                int op = Utils.ColetarValorInt("Informe o que deseja alterar (1 - Tipo do Pet) (2 - Raça) (3 - Sexo) (4 - Nome): ");

                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        string tipoPet = Utils.ColetarString("informe o tipo do pet: ");
                        Db_ONG db = new Db_ONG();
                        string sql = $"update dbo.pet set familiaPet = '{tipoPet}' where nChipPet = {id}";
                        if (db.UpdateTable(sql) != 0) Console.WriteLine("Cadastro efetuado com sucesso!!!");
                        else Console.WriteLine("Houve um erro na solicitação...");
                        Utils.Pause();
                        return;

                    case 2:
                        string raca = Utils.ColetarString("Informe a raça: ");
                        db = new Db_ONG();
                        sql = $"update dbo.pet set racaPet = '{raca}' where nChipPet = {id};";
                        if (db.UpdateTable(sql) != 0) Console.WriteLine("Cadastro efetuado com sucesso!!!");
                        else Console.WriteLine("Houve um erro na solicitação...");
                        Utils.Pause();
                        return;

                    case 3:
                        Char sexo;
                        do
                        {
                            sexo = Utils.ColetarValorChar("Informe o sexo do pet: ");
                            if (sexo != 'M' && sexo != 'F')
                            {
                                sexo = ' ';
                                break;
                            }
                            else break;
                        } while (true);
                        db = new Db_ONG();
                        sql = $"update dbo.pet set sexoPet = '{sexo}' where nChipPet = {id};";
                        if (db.UpdateTable(sql) != 0) Console.WriteLine("Cadastro efetuado com sucesso!!!");
                        else Console.WriteLine("Houve um erro na solicitação...");
                        Utils.Pause();
                        return;

                    case 4:
                        db = new Db_ONG();
                        string nome = Utils.ColetarString("informe o nome do pet: ");
                        sql = $"update dbo.pet set nomePet = '{nome}' where nChipPet = {id};";
                        if (db.UpdateTable(sql) != 0) Console.WriteLine("Cadastro efetuado com sucesso!!!");
                        else Console.WriteLine("Houve um erro na solicitação...");
                        Utils.Pause();
                        return;

                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
                Utils.Pause();
            } while (true);
        }

        public override string ToString()
        {
            return $"Tipo do PET: {TipoPet}\nRaça: {Raca}\nSexo: {SexoPet}\nNome do Pet: {NomePet}".ToString();
        }
    }
}
