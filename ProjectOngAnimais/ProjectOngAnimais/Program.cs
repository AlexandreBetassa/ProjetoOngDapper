using System;

namespace ProjectOngAnimais
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pet p = new Pet();
            p.CadastrarPet();

            Console.WriteLine(p);
        }
    }
}
