using System;
using System.Collections.Generic;
using Models;
using Repository;

namespace Controllers
{
    public class AnimalController
    {
        public bool Delete(Animal animal)
        {
            return new AnimalRepository().Delete(animal);
        }

        public bool Insert(Animal animal)
        {
            return new AnimalRepository().Insert(animal);
        }

        public List<Animal> Select()
        {
            return new AnimalRepository().Select();
        }

        public bool Update(Animal animal)
        {
            return new AnimalRepository().Insert(animal);
        }
    }
}
