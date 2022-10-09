using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal class Animal
    {
        public int Id { get; set; }
        public String Familia { get; set; }
        public String Raca { get; set; }
        public String Sexo { get; set; }
        public String Disponivel { get; set; }
        public String Nome { get; set; }

        public override string ToString()
        {
            return $"Nome do Animal: {Nome}\nFamilia: {Familia}\nRaça: {Raca}\tSexo: {Sexo}\nSituação: {Disponivel}".ToString();
        }
    }
}
