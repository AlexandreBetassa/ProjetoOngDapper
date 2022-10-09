using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    internal interface IPessoaController
    {
        public bool Insert(Pessoa pessoa);
        public bool Delete(Pessoa pessoa);
        public bool Update(Pessoa pessoa);
        public List<Pessoa> Select();
    }
}
