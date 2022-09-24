using System;

namespace ProjectOngAnimais
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Db_ONG db = new Db_ONG();
            Pessoa pessoa = new Pessoa();
            pessoa.CadastrarPessoa();
            db.InsertTablePessoa(pessoa);

        }
    }
}
