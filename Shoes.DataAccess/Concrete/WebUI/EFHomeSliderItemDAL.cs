using Microsoft.EntityFrameworkCore;
using Shoes.Core.Helpers.FileHelper;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct.WebUI;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites.DTOs.WebUI.HomeSliderItemDTOs;
using Shoes.Entites.WebUIEntites;
using System.Net;

namespace Shoes.DataAccess.Concrete.WebUI
{
    public class EFHomeSliderItemDAL : IHomeSliderItemDAL
    {
        private readonly AppDBContext _dbContext;

        public EFHomeSliderItemDAL(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task< IResult>  AddHomeSliderItemAsync(AddHomeSliderItemDTO addHomeSliderItemDTO)
        {
                HomeSliderItem homeSliderItem = new HomeSliderItem();
            string fileResult = await FileHelper.SaveFileAsync(addHomeSliderItemDTO.BackgroundImage, IsWebUI: true, IsOrderPdf: false);
            homeSliderItem.BackgroundImageUrl = fileResult;
            _dbContext.HomeSliderItems.Add(homeSliderItem);
            foreach (var desc in addHomeSliderItemDTO.Description)
            {
                HomeSliderLanguage homeSliderLanguage = new HomeSliderLanguage()
                {
                    Description=desc.Value,
                    HomeSliderItemId=homeSliderItem.Id,
                    LangCode=desc.Key,
                    Title=addHomeSliderItemDTO.Title.GetValueOrDefault(desc.Key),
                    
                };
                _dbContext.HomeSliderLanguages.Add(homeSliderLanguage);
            }

            _dbContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);

        }

        public IResult DeleteHomeSliderItem(Guid Id, string LangCode)
        {
          var checekedData=_dbContext.HomeSliderItems.FirstOrDefault(x => x.Id == Id);
            if (checekedData is { })return new ErrorResult(HttpStatusCode.NotFound);
         bool fileResult=   FileHelper.RemoveFile(checekedData.BackgroundImageUrl);
            if (!fileResult)
         return new ErrorResult(HttpStatusCode.BadRequest);
           
            _dbContext.HomeSliderItems.Remove(checekedData);
            _dbContext.SaveChanges(); 
            return new SuccessResult(HttpStatusCode.OK);
        }

        public async Task<IDataResult<PaginatedList<GetHomeSliderItemDTO>>> GetAllHomeSliderAsync(string LangCode, int page)
        {
          IQueryable<GetHomeSliderItemDTO> dataQuery=_dbContext.HomeSliderItems.AsNoTracking().AsSplitQuery().AsQueryable().Select(x=>new GetHomeSliderItemDTO
          {
              Id = x.Id,
              Description=x.Languages.FirstOrDefault(y=>y.LangCode==LangCode).Description,
              Title=x.Languages.FirstOrDefault(y=>y.LangCode==LangCode).Title,
              ImageUrl=x.BackgroundImageUrl
              

          });
            var resultData = await PaginatedList<GetHomeSliderItemDTO>.CreateAsync(dataQuery, page, 10);
            return new SuccessDataResult<PaginatedList<GetHomeSliderItemDTO>>(resultData, HttpStatusCode.OK);
        }

        public IDataResult<IQueryable<GetHomeSliderItemForUIDTO>> GetHomeSliderItemForUI(string LangCode)
        {
            var dataQuery = _dbContext.HomeSliderItems.AsNoTracking().AsSplitQuery().Select(x => new GetHomeSliderItemForUIDTO
            {
                Description = x.Languages.FirstOrDefault(y => y.LangCode == LangCode).Description,
                Title = x.Languages.FirstOrDefault(y => y.LangCode == LangCode).Title,
                ImageUrl = x.BackgroundImageUrl

            });
            return new SuccessDataResult<IQueryable<GetHomeSliderItemForUIDTO>>(dataQuery, HttpStatusCode.OK);
        }

        public IDataResult<GetHomeSliderItemForUpdateDTO> GetHomeSliderItemForUpdate(Guid Id)
        {
            var dataQuery = _dbContext.HomeSliderItems.AsNoTracking().Select(x => new GetHomeSliderItemForUpdateDTO
            {
                Id = x.Id,
                Description = x.Languages.Select(y => new KeyValuePair<string, string>(y.LangCode, y.Description)).ToDictionary(),
                Title = x.Languages.Select(y => new KeyValuePair<string, string>(y.LangCode, y.Title)).ToDictionary(),
                ImageUrl = x.BackgroundImageUrl
            }).FirstOrDefault(x=>x.Id==Id);
            if (dataQuery is { })
                return new ErrorDataResult<GetHomeSliderItemForUpdateDTO>(HttpStatusCode.NotFound);
            return new SuccessDataResult<GetHomeSliderItemForUpdateDTO>(response: dataQuery, HttpStatusCode.OK);
        }

        public async Task< IResult> UpdateHomeSliderItemAsync(UpdateHomeSliderItemDTO updateHomeSliderItemDTO)
        {
            var checkedData = _dbContext.HomeSliderItems.Include(x => x.Languages).FirstOrDefault(x => x.Id == updateHomeSliderItemDTO.Id);
            if (checkedData is { })
                return new ErrorResult(HttpStatusCode.NotFound);
            foreach (var desc in updateHomeSliderItemDTO.Description)
            {
                var langChecked = checkedData.Languages.FirstOrDefault(x => x.LangCode == desc.Key);
                if (langChecked is { })
                    continue;
                langChecked.Description = desc.Value;
                langChecked.Title = updateHomeSliderItemDTO.Title.GetValueOrDefault(desc.Key);
                

            }
            if (string.IsNullOrEmpty(updateHomeSliderItemDTO.CurrentImage)&& updateHomeSliderItemDTO.NewImage is not null)
            {
             var removeFile=   FileHelper.RemoveFile(checkedData.BackgroundImageUrl);
                if (removeFile)
                {
                    string newPictureUrl = await FileHelper.SaveFileAsync(updateHomeSliderItemDTO.NewImage, false, true);
                    checkedData.BackgroundImageUrl = newPictureUrl;
                }
            }
            _dbContext.HomeSliderItems.Update(checkedData);
            _dbContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);
        }
    }
}
