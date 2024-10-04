using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.HomeSliderItemDTOValidatons;
using Shoes.Core.Helpers;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct.WebUI;
using Shoes.Entites.DTOs.WebUI.HomeSliderItemDTOs;
using System.Net;

namespace Shoes.Bussines.Concrete
{
    public class HomeSliderItemManager : IHomeSliderItemService
    {
        private string[] SupportedLaunguages
        {
            get
            {

                return ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();


            }
        }

        private string DefaultLaunguage
        {
            get
            {
                return ConfigurationHelper.config.GetSection("SupportedLanguage:Default").Get<string>();
            }
        }
        private readonly IHomeSliderItemDAL _homeSliderItemDAL;

        public HomeSliderItemManager(IHomeSliderItemDAL homeSliderItemDAL)
        {
            _homeSliderItemDAL = homeSliderItemDAL;
        }

        public async Task<IResult> AddHomeSliderItemAsync(AddHomeSliderItemDTO addHomeSliderItemDTO, string culture)
        {
            if (string.IsNullOrEmpty(culture)||SupportedLaunguages.Contains(culture))
        culture = DefaultLaunguage;
            AddHomeSliderItemDTOValidation validationRules = new AddHomeSliderItemDTOValidation(culture);
            var validationResult=await validationRules.ValidateAsync(addHomeSliderItemDTO);
            if (!validationResult.IsValid) return new ErrorResult(messages: validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            return await _homeSliderItemDAL.AddHomeSliderItemAsync(addHomeSliderItemDTO);
        }

        public IResult DeleteHomeSliderItem(Guid Id)
        {
            if (Id == Guid.Empty)
                return new ErrorResult(HttpStatusCode.BadRequest);
 
            return _homeSliderItemDAL.DeleteHomeSliderItem(Id);
        }

        public async Task<IDataResult<PaginatedList<GetHomeSliderItemDTO>>> GetAllHomeSliderAsync(string LangCode, int page)
        {
            if (SupportedLaunguages.Contains(LangCode) || string.IsNullOrEmpty(LangCode)) LangCode = DefaultLaunguage;
            if (page < 1)
                page = 1;
            return await _homeSliderItemDAL.GetAllHomeSliderAsync(LangCode, page);
        }

        public IDataResult<IQueryable<GetHomeSliderItemForUIDTO>> GetHomeSliderItemForUI(string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) || !SupportedLaunguages.Contains(LangCode))
                LangCode = DefaultLaunguage;
            return _homeSliderItemDAL.GetHomeSliderItemForUI(LangCode);
        }

        public IDataResult<GetHomeSliderItemForUpdateDTO> GetHomeSliderItemForUpdate(Guid Id)
        {
            if (Id == Guid.Empty)
                return new ErrorDataResult<GetHomeSliderItemForUpdateDTO>(HttpStatusCode.BadRequest);
            return _homeSliderItemDAL.GetHomeSliderItemForUpdate(Id);
        }

        public async Task<IResult> UpdateHomeSliderItemAsync(UpdateHomeSliderItemDTO updateHomeSliderItemDTO, string culture)
        {

            if(string.IsNullOrEmpty(culture)||!SupportedLaunguages.Contains(culture)) culture = DefaultLaunguage;   

            UpdateHomeSliderItemDTOValidation validationRules = new UpdateHomeSliderItemDTOValidation(culture);
            var validationResult=await validationRules.ValidateAsync(updateHomeSliderItemDTO);
            if (!validationResult.IsValid)
                return new ErrorResult(messages: validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            return await _homeSliderItemDAL.UpdateHomeSliderItemAsync(updateHomeSliderItemDTO);
        }
    }
}
