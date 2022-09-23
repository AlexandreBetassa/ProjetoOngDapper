using System;

namespace ProjectOngAnimais
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pessoa p = new Pessoa();
            p.CadastrarPessoa();
            Console.WriteLine(p);
        }
    }
}
