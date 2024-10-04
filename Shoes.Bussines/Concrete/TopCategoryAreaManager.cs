using Microsoft.Extensions.Configuration;
using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.TopCategoryAreaDTOValidations;
using Shoes.Core.Helpers;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct.WebUI;
using Shoes.Entites.DTOs.WebUI.TopCategoryAreaDTOs;
using System.Net;

namespace Shoes.Bussines.Concrete
{
    public class TopCategoryAreaManager:ITopCategoryAreaService
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
        private readonly ITopCategoryAreaDAL _topCategoryAreaDAL;

        public TopCategoryAreaManager(ITopCategoryAreaDAL topCategoryAreaDAL)
        {
            _topCategoryAreaDAL = topCategoryAreaDAL;
        }

        public async Task<IResult> AddTopCategoryAreaAsync(AddTopCategoryAreaDTO addTopCategoryAreaDTO, string culture)
        {
            if (!SupportedLaunguages.Contains(culture)|| string.IsNullOrEmpty(culture))
                culture = DefaultLaunguage;
            AddTopCategoryAreaDTOValidation validationRules = new AddTopCategoryAreaDTOValidation(culture);
            var validationResult = await validationRules.ValidateAsync(addTopCategoryAreaDTO);
            if (!validationResult.IsValid)
        return new ErrorResult(messages:validationResult.Errors.Select(x=>x.ErrorMessage).ToList(),HttpStatusCode.BadRequest);
            return await  _topCategoryAreaDAL.AddTopCategoryAreaAsync(addTopCategoryAreaDTO);
        }

        public async Task<IDataResult<PaginatedList<GetTopCategoryAreaDTO>>> GetTopCategoryAreaAsync(string LangCode, int page)
        {
            if (!SupportedLaunguages.Contains(LangCode)|| string.IsNullOrEmpty(LangCode))
                LangCode = DefaultLaunguage;
            if (page<1)page= 1;
            return await _topCategoryAreaDAL.GetTopCategoryAreaAsync(LangCode, page);
        }

        public IDataResult<IQueryable<GetTopCategoryAreaForUIDTO>> GetTopCategoryAreaForUI(string LangCode)
        {
           if(SupportedLaunguages.Contains(LangCode)||string.IsNullOrEmpty(LangCode))LangCode = DefaultLaunguage;
           return _topCategoryAreaDAL.GetTopCategoryAreaForUI(LangCode);

        }

        public IDataResult<GetTopCategoryAreaForUpdateDTO> GetTopcategoryAreaForUpdate(Guid Id)
        {
         if(Id==Guid.Empty)return new ErrorDataResult<GetTopCategoryAreaForUpdateDTO>(HttpStatusCode.BadRequest); 
         return _topCategoryAreaDAL.GetTopcategoryAreaForUpdate(Id);
        }

        public IResult RemoveTopCategoryArea(Guid Id)
        {
            if (Id == Guid.Empty) return new ErrorResult(HttpStatusCode.BadRequest);
            return _topCategoryAreaDAL.RemoveTopCategoryArea(Id);
        }

        public async Task<IResult> UpdateTopCategoryAreaAsync(UpdateTopCategoryAreaDTO updateTopCategoryAreaDTO, string culture)
        {
         
            if (SupportedLaunguages.Contains(culture)||string.IsNullOrEmpty(culture)) culture = DefaultLaunguage;
            UpdateTopCategoryAreaDTOValidation validationRules = new UpdateTopCategoryAreaDTOValidation(culture);
            var validationResult=await validationRules.ValidateAsync(updateTopCategoryAreaDTO);
            if (!validationResult.IsValid)
                return new ErrorResult(messages: validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
            return await _topCategoryAreaDAL.UpdateTopCategoryAreaAsync(updateTopCategoryAreaDTO);
        }
    }
}
