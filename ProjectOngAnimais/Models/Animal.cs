using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Animal
    {

        public readonly static string INSERT = "INSERT INTO dbo.animal (familia, raca, sexo, disponivel, nome) VALUES (@familia, @raca, @sexo, @disponivel, @nome)";
        public readonly static string SELECT = "SELECT nChip, familia, raca, sexo, disponivel, nome FROM dbo.animal";
        public readonly static string UPDATE = "UPDATE dbo.animal SET familia = @familia, raca = @raca, sexo = @sexo, disponivel = @disponivel, nome = @nome WHERE nChip = @nChip";
        public readonly static string DELETE = "DELETE FROM dbo.animal WHERE nChip = @nChip";

        public int NChip { get; set; }
        public String Familia { get; set; }
        public String Raca { get; set; }
        public String Sexo { get; set; }
        public String Disponivel { get; set; }
        public String Nome { get; set; }

        public override string ToString()
        {
            return $"Número do chip: {NChip}\nNome do Animal: {Nome}\nFamilia: {Familia}\nRaça: {Raca}\tSexo: {Sexo}\nSituação: {Disponivel}".ToString();
        }
    }
}
