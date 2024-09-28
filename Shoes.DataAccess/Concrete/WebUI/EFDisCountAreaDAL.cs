using Microsoft.EntityFrameworkCore;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct.WebUI;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites.DTOs.WebUI.DisCountAreDTOs;
using Shoes.Entites.WebUIEntites;
using System.Net;

namespace Shoes.DataAccess.Concrete.WebUI
{
    public class EFDisCountAreaDAL : IDisCountAreaDAL
    {
        private readonly AppDBContext _dbContext;

        public EFDisCountAreaDAL(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IResult AddDiscountArea(AddDisCountAreaDTO addDisCountAreaDTO)
        {
            DisCountArea disCountArea = new DisCountArea();
            _dbContext.DisCountAreas.Add(disCountArea);
            foreach (var item in addDisCountAreaDTO.DescriptionContent)
            {
                DisCountAreaLanguage Language = new DisCountAreaLanguage()
                {
                    DisCountAreaId = disCountArea.Id,
                    LangCode = item.Key,
                    Description = item.Value,
                    Title = addDisCountAreaDTO.TitleContent.GetValueOrDefault(item.Key)
            }; 
        
            _dbContext.DisCountAreaLanguages.Add(Language); 


            }
            _dbContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);
        }

        public IResult Delete(Guid Id)
        {
           var checekdDB=_dbContext.DisCountAreas.FirstOrDefault(x => x.Id == Id);
            if (checekdDB is null) return new ErrorResult(HttpStatusCode.NotFound);
            _dbContext.DisCountAreas.Remove(checekdDB);
            _dbContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);
        }

        public IDataResult<IQueryable< GetDisCountAreaDTO>> GetDisCountArea( string LangCode)
        {
            IQueryable<GetDisCountAreaDTO> query = _dbContext.DisCountAreas.AsSplitQuery().AsNoTracking().Select(x =>
            new GetDisCountAreaDTO()
            {
                Id = x.Id,
                Description = x.Languages.FirstOrDefault(y => y.LangCode == LangCode).Description,
                Title = x.Languages.FirstOrDefault(y => y.LangCode == LangCode).Title,

            });
            return new SuccessDataResult<IQueryable<GetDisCountAreaDTO>>(query,HttpStatusCode.OK);


        }

        public IDataResult<GETDisCountAreaForUpdateDTO> GetDisCountAreaForUpdate(Guid Id)
        {
            var data=_dbContext.DisCountAreas.Select(x=>new GETDisCountAreaForUpdateDTO
            {

                Id=x.Id,
                DescriptionContent=x.Languages.Select(y=> new KeyValuePair<string,string>(y.LangCode,y.Description)).ToDictionary(),
                TitleContent=x.Languages.Select(y=>new KeyValuePair<string,string>(y.LangCode,y.Title)).ToDictionary(),
                

            }).FirstOrDefault(x=>x.Id == Id);
            if (data is null)
                return new ErrorDataResult<GETDisCountAreaForUpdateDTO>(HttpStatusCode.NotFound);
            return new SuccessDataResult<GETDisCountAreaForUpdateDTO>(data,HttpStatusCode.OK);
        }

        public IResult UpdateDisCountArea(UpdateDisCountAreaDTO updateDisCountAreaDTO)
        {
            var checkedData = _dbContext.DisCountAreas
                .Include(x=>x.Languages)
                .FirstOrDefault(x => x.Id == updateDisCountAreaDTO.Id);
            if (checkedData is null)
          return new ErrorResult(HttpStatusCode.NotFound);
            foreach (var titelDic in updateDisCountAreaDTO.TitleContent)
            {
                var especialLang = checkedData.Languages.FirstOrDefault(x => x.LangCode == titelDic.Key);
                if (especialLang is null)
                    continue;
                especialLang.Title= titelDic.Value;
                especialLang.Description=updateDisCountAreaDTO.DescriptionContent.GetValueOrDefault(titelDic.Key);  
                _dbContext.DisCountAreaLanguages.Update(especialLang);
            }
            _dbContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);

        }
    }
}
