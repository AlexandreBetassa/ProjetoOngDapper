using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Models;
using Services;
using Services.DataBaseConfiguration;

namespace Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly string _conn;

        public AnimalRepository()
        {
            _conn = DataBaseConfiguration.Get();
        }
        public bool Delete(Animal animal)
        {
            bool result = false;
            using (SqlConnection db = new(_conn))
            {
                db.Open();
                db.Execute(Animal.DELETE, animal);
                result = true;
            }
            return result;
        }

        public bool Insert(Animal animal)
        {
            bool result = false;
            using (SqlConnection db = new(_conn))
            {
                db.Open();
                db.Execute(Animal.INSERT, animal);
                result = true;
            }
            return result;
        }

        public List<Animal> Select()
        {
            using (SqlConnection db = new(_conn))
            {
                db.Open();
                var lstAnimal = db.Query<Animal>(Animal.SELECT);
                return (List<Animal>)lstAnimal;
            }
        }

        public bool Update(Animal animal)
        {
            bool result = false;
            using (SqlConnection db = new(_conn))
            {
                db.Open();
                db.Execute(Animal.UPDATE, animal);
                result = true;
            }
            return result;
        }
    }
}
