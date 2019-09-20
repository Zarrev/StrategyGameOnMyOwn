using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace backend.BLL.Maps.Interfaces
{
    public interface IBaseMap<TViewModel>
    {
        Task<TViewModel> GetElement(string elementViewModelId);
        Task<List<TViewModel>> GetAll();
        void Create(TViewModel elementViewModel);
        void Delete(string elementViewModelId);
        void Update(TViewModel elementViewModel);
    }

}
