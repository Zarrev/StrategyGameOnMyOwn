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
        Task InsertElement(T element);
        Task DeleteElement(K elementId);
        Task UpdateElement(T element);
    }

}
