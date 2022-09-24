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

        

        public override string ToString()
        {
            return $"Tipo do PET: {TipoPet}\nRaça: {Raca}\nSexo: {SexoPet}\nNome do Pet: {NomePet}".ToString();
        }
    }
}
