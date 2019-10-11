using backend.Model.Frontend;
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
        Task Create(TViewModel elementViewModel);
        Task Delete(string elementViewModelId);
        Task Update(TViewModel elementViewModel);
    }

}
