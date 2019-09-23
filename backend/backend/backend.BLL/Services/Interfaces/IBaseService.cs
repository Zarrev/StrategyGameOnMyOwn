using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace backend.BLL.Services.Interfaces
{
    public interface IBaseService<T, K>
    {
        Task<List<T>> GetElements();
        Task<T> GetElementById(K elementId);
        void InsertElement(T element);
        void DeleteElement(K elementId);
        void UpdateElement(T element);
    }

}
