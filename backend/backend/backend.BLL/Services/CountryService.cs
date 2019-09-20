using backend.BLL.Services.Interfaces;
using backend.DAL.Repository.Interfaces;
using backend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.BLL
{
    public class CountryService: ICountryService
    {
        private readonly ICountryRepository _repository;

        public CountryService(ICountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Country>> GetElements()
        {
            return await _repository.GetElements().ContinueWith(task => (List<Country>) task.Result);
        }

        public async Task<Country> GetElementById(string elementId)
        {
            return await _repository.GetElementById(elementId);
        }

        public void InsertElement(Country element)
        {
            _repository.InsertElement(element);
            _repository.Save();
        }

        public void DeleteElement(string elementId)
        {
            _repository.DeleteElement(elementId);
            _repository.Save();
        }

        public void UpdateElement(Country element)
        {
            _repository.UpdateElement(element);
            _repository.Save();
        }

    }
}
