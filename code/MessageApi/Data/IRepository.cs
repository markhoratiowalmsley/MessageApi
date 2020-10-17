using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepository<T> where T : IDataObject
    {
        Task<IEnumerable<T>> SelectAll();

        Task<T> SelectById(Guid id);

        Task<T> Insert(T item);
    }
}
