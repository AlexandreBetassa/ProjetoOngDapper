using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository
{
    public interface IAnimal
    {
        public bool Insert(Animal pessoa);
        public List<Animal> Select();
        public bool Update(Animal animal);
        public bool Delete(Animal animal);
    }
}
