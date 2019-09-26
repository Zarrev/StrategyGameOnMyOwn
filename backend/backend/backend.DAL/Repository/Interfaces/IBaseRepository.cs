using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace backend.DAL.Repository
{
    public interface IBaseRepository<T, K> : IDisposable
    {
        Task<IEnumerable<T>> GetElements();
        Task<T> GetElementById(K elementId);
        Task InsertElement(T element);
        Task DeleteElement(K elementId);
        Task UpdateElement(T element);
        Task Save();
    }

}
