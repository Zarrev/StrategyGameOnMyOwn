using System;
using System.Collections.Generic;
using System.Text;

namespace backend.DAL.Repository
{
    public interface IBaseRepository<T, K> : IDisposable
    {
        IEnumerable<T> GetElements();
        T GetElementById(K elementId);
        void InsertElement(T element);
        void DeleteElement(K elementId);
        void UpdateElement(T element);
        void Save();
    }

}
