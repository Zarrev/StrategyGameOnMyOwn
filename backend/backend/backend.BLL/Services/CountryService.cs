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

        public async Task<Country> GetElementByUserId(string userId)
        {
            return await _repository.getElementByUserId(userId);
        }

        public async Task InsertElement(Country element)
        {
            await _repository.InsertElement(element);
            await _repository.Save();
        }

        public async Task DeleteElement(string elementId)
        {
            await _repository.DeleteElement(elementId);
            await _repository.Save();
        }

        public async Task UpdateElement(Country element)
        {
            await _repository.UpdateElement(element);
            await _repository.Save();
        }

    }
}
