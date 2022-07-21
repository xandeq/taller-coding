using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Taller.CarModel.Data.Repository;
using Taller.CarModel.Models;

namespace Taller.CarModel.Data.Repository
{
    public class CarRepository : IRepository<Car>
    {
        public static List<Car> cars = new(){
            new Car { Id = 1, Make = "Audi", Model = "R8", Year = 2018, Doors = 2, Color = "Red", Price = 79995 },
            new Car { Id = 2, Make = "Tesla", Model = "3", Year = 2018, Doors = 4, Color = "Black", Price = 54995 },
            new Car { Id = 3, Make = "Porsche", Model = " 911 991", Year = 2020, Doors = 2, Color = "White", Price = 155000 },
            new Car { Id = 4, Make = "Mercedes-Benz", Model = "GLE 63S", Year = 2021, Doors = 5, Color = "Blue", Price = 83995 },
            new Car { Id = 5, Make = "BMW", Model = "X6 M", Year = 2020, Doors = 5, Color = "Silver", Price = 62995 },
        };

        public CarRepository(List<Car> carsContext)
        {
            carsContext = cars;
        }

        public CarRepository()
        {

        }

        public void Add(Car car)
        {
            cars.Add(car);
        }
        public void Delete(Car car)
        {
            var objFromDb = cars.FirstOrDefault(x => x.Id == car.Id);
            if (objFromDb == null)
            {
                cars.Remove(objFromDb);
            }
        }
        public void Update(Car car)
        {
            var index = cars.FindIndex(r => r.Id == car.Id);
            if (index != -1)
            {
                cars[(int)index] = car;
            }
        }
        public IEnumerable<Car> Get()
        {
            return cars.AsEnumerable();
        }

        public Car GetById(Car car)
        {
            return cars.SingleOrDefault(s => s.Id == car.Id);
        }
    }
}