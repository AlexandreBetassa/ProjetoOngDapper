using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository
{
    internal interface IPessoaRepository
    {
        public bool Insert(Pessoa pessoa);
        public List<Pessoa> Select();


    }
}
