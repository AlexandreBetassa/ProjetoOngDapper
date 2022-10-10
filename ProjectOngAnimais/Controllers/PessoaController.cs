using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repository;

namespace Controllers
{
    public class PessoaController 
    {
        public bool Delete(Pessoa pessoa)
        {
            return new PessoaRepository().Delete(pessoa);
        }

        public bool Insert(Pessoa pessoa)
        {
            return new PessoaRepository().Insert(pessoa);
        }

        public List<Pessoa> Select()
        {
            return new PessoaRepository().Select();
        }

        public bool Update(Pessoa pessoa)
        {
            return new PessoaRepository().Update(pessoa);
        }
    }
}
