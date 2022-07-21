using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Taller.CarModel.Data.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        T GetById(T car);                                          
        void Add(T car);
        void Delete(T car);
        void Update(T car);
    }
}
