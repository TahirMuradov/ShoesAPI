using Microsoft.EntityFrameworkCore;
using Shoes.Core.Helpers.FileHelper;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct.WebUI;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites.DTOs.WebUI.TopCategoryAreaDTOs;
using Shoes.Entites.WebUIEntites;
using System.Net;

namespace Shoes.DataAccess.Concrete.WebUI
{
    public class EFTopCategoryAreaDAL : ITopCategoryAreaDAL
    {
        private readonly AppDBContext _appDBContext;

        public EFTopCategoryAreaDAL(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task< IResult> AddTopCategoryAreaAsync(AddTopCategoryAreaDTO addTopCategoryAreaDTO)
        {
            if (addTopCategoryAreaDTO.CategoryId is not null ||addTopCategoryAreaDTO.CategoryId!=default)
            {
                
            var checkedCategoryId=_appDBContext.Categories.FirstOrDefault(x=>x.Id == addTopCategoryAreaDTO.CategoryId);
            if (checkedCategoryId is null)
                return new ErrorResult(HttpStatusCode.NotFound);
            }
            if (addTopCategoryAreaDTO.SubCategoryId is not null || addTopCategoryAreaDTO.SubCategoryId !=default)
            {
                var cehcekSubCategory = _appDBContext.SubCategories.FirstOrDefault(x => x.Id == addTopCategoryAreaDTO.SubCategoryId);
                if (cehcekSubCategory is null)
                    return new ErrorResult(HttpStatusCode.NotFound);
            }

       TopCategoryArea topCategoryArea = new TopCategoryArea()
       {
           CategoryId = addTopCategoryAreaDTO.CategoryId,
           SubCategoryId=addTopCategoryAreaDTO.SubCategoryId,
           
       };
            var pictureUrl =await FileHelper.SaveFileAsync(addTopCategoryAreaDTO.Image, false, true);
            topCategoryArea.ImageUrl = pictureUrl;
            _appDBContext.TopCategoryAreas.Add(topCategoryArea);
            foreach (var desc in addTopCategoryAreaDTO.Description )
            {
                TopCategoryAreaLanguage topCategoryAreaLanguage = new TopCategoryAreaLanguage()
                {
                    LangCode = desc.Key,
                    Description = desc.Value,
                    Title = addTopCategoryAreaDTO.Title.GetValueOrDefault(desc.Key),
                    TopCategoryAreaId = topCategoryArea.Id
                };
                _appDBContext.TopCategoryAreaLanguages.Add(topCategoryAreaLanguage);
            }
            _appDBContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);    
        }

        public async Task<IDataResult<PaginatedList<GetTopCategoryAreaDTO>>> GetTopCategoryAreaAsync(string LangCode, int page)
        {
            var dataQuery = _appDBContext.TopCategoryAreas.AsNoTracking().AsSplitQuery().Select(x => new GetTopCategoryAreaDTO
            {
                Id = x.Id,
                CategoryName = x.CategoryId == default ? x.SubCategory.SubCategoryLanguages.FirstOrDefault(y => y.LangCode == LangCode).Content
                : x.Category.CategoryLanguages.FirstOrDefault(y => y.LangCode == LangCode).Content,
                Description = x.TopCategoryAreaLanguages.FirstOrDefault(y => y.LangCode == LangCode).Description,
                PictureUrl = x.ImageUrl,
                Title = x.TopCategoryAreaLanguages.FirstOrDefault(y => y.LangCode == LangCode).Title
            });
            var result= await PaginatedList<GetTopCategoryAreaDTO>.CreateAsync(dataQuery,page,10);
           return new SuccessDataResult<PaginatedList<GetTopCategoryAreaDTO>>(result,HttpStatusCode.OK);
        }

        public IDataResult<IQueryable<GetTopCategoryAreaForUIDTO>> GetTopCategoryAreaForUI(string LangCode)
        {
           return new SuccessDataResult<IQueryable<GetTopCategoryAreaForUIDTO>>(response:_appDBContext.TopCategoryAreas
               .AsNoTracking()
               .AsSplitQuery()
               .Select(x=>new GetTopCategoryAreaForUIDTO
           {
               CategoryName=x.Category.CategoryLanguages.FirstOrDefault(y=>y.LangCode==LangCode).Content,
               Description=x.TopCategoryAreaLanguages.FirstOrDefault(y=>y.LangCode==LangCode).Description,
               PictureUrl=x.ImageUrl,
               Title=x.TopCategoryAreaLanguages.FirstOrDefault(y=>y.LangCode==LangCode).Title
           }),HttpStatusCode.OK);
        }

        public IDataResult<GetTopCategoryAreaForUpdateDTO> GetTopcategoryAreaForUpdate(Guid Id)
        {
            var data = _appDBContext.TopCategoryAreas
                .AsNoTracking()
                .AsSplitQuery()
                .Select(x => new GetTopCategoryAreaForUpdateDTO
                {
                    Id = x.Id,
                    CategoryId = x.CategoryId == default ? x.SubCategoryId : x.CategoryId,
                    Description = x.TopCategoryAreaLanguages.Select(y => new KeyValuePair<string, string>(y.LangCode, y.Description)).ToDictionary(),
                    PictureUrl = x.ImageUrl,
                    Title = x.TopCategoryAreaLanguages.Select(y => new KeyValuePair<string, string>(y.LangCode, y.Title)).ToDictionary(),

                }).FirstOrDefault(x=>x.Id==Id);

            return new SuccessDataResult<GetTopCategoryAreaForUpdateDTO>(response:data, HttpStatusCode.OK);
        }

        public IResult RemoveTopCategoryArea(Guid Id)
        {
        var ceheckedData=_appDBContext.TopCategoryAreas.FirstOrDefault(x=>x.Id==Id);
            if (ceheckedData is null) return new ErrorResult(HttpStatusCode.NotFound);
            FileHelper.RemoveFile(ceheckedData.ImageUrl);
            _appDBContext.TopCategoryAreas.Remove(ceheckedData);
            _appDBContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);
        }

        public async Task<IResult> UpdateTopCategoryAreaAsync(UpdateTopCategoryAreaDTO updateTopCategoryAreaDTO)
        {var checkedData=_appDBContext.TopCategoryAreas.Include(x=>x.TopCategoryAreaLanguages).FirstOrDefault(x=>x.Id == updateTopCategoryAreaDTO.Id);
            if (checkedData is null)
                return new ErrorResult(HttpStatusCode.NotFound);

            foreach (var title in updateTopCategoryAreaDTO.Title)
            {
                var checkedLangcode = checkedData.TopCategoryAreaLanguages.FirstOrDefault(x => x.LangCode == title.Key);
                if (checkedLangcode is null) continue;
                checkedLangcode.Title = title.Value;
                checkedLangcode.Description = updateTopCategoryAreaDTO.Description.GetValueOrDefault(title.Key);
                _appDBContext.TopCategoryAreaLanguages.Update(checkedLangcode);

            }
    
            if (string.IsNullOrEmpty( updateTopCategoryAreaDTO.CurrentPictureUrl) )
            {
                if (FileHelper.RemoveFile(checkedData.ImageUrl))
                {
                    
                var fileUrl = await FileHelper.SaveFileAsync(updateTopCategoryAreaDTO.NewImage);
                    checkedData.ImageUrl = fileUrl;
                
                }


            }
            checkedData.SubCategoryId = updateTopCategoryAreaDTO.SubCategoryId??updateTopCategoryAreaDTO.SubCategoryId;
            checkedData.CategoryId = updateTopCategoryAreaDTO.CategoryId??updateTopCategoryAreaDTO.CategoryId;
            _appDBContext.TopCategoryAreas.Update(checkedData);
            _appDBContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);

        }
    }
}
