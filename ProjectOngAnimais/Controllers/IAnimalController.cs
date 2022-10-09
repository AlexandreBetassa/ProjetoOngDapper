using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    internal interface IAnimalController
    {
        public bool Insert(Animal animal);
        public bool Delete(Animal animal);
        public bool Update(Animal animal);
        public List<Animal> Select();

    }
}
