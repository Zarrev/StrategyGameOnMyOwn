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
        void InsertElement(T element);
        void DeleteElement(K elementId);
        void UpdateElement(T element);
        void Save();
    }

}
