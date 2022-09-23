using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOngAnimais
{
    internal class Pessoa
    {
        public String CPF { get; set; }
        public String Nome { get; set; }
        public Char Sexo { get; set; }
        public String telefone { get; set; }
        public Endereco End { get; set; }
        public Char Ativa { get; set; }

        public Pessoa()
        {
            Ativa = 'A';
        }

    }
}
