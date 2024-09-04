using Microsoft.EntityFrameworkCore;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites;
using Shoes.Entites.DTOs.SizeDTOs;
using System.Net;

namespace Shoes.DataAccess.Concrete
{
    public class EFSizeDAL:ISizeDAL
    {
        private readonly AppDBContext _appDBContext;

        public EFSizeDAL(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public IResult AddSize(AddSizeDTO addSizeDTO)
        {
            try
            {
                Size ChekcedSize = _appDBContext.Sizes.FirstOrDefault(x => x.SizeNumber == addSizeDTO.Size);
                if (ChekcedSize is not null) return new ErrorResult(statusCode: HttpStatusCode.Conflict);
                Size size = new()
                {
                    SizeNumber = addSizeDTO.Size,

                };
                _appDBContext.Sizes.Add(size);
                _appDBContext.SaveChanges();
                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult DeleteSize(Guid Id)
        {
            try
            {
                Size size = _appDBContext.Sizes.FirstOrDefault(x => x.Id == Id);
                if (size is null) return new ErrorResult(statusCode: HttpStatusCode.NotFound);
                _appDBContext.Sizes.Remove(size);
                _appDBContext.SaveChanges();
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ErrorResult(message:ex.Message, statusCode: HttpStatusCode.BadRequest);
              
            }


           
        }

        public IDataResult<IQueryable<GetSizeForUpdateDTO>> GetAllSize()
        {
            IQueryable<GetSizeForUpdateDTO> sizeQuery = _appDBContext.Sizes.AsNoTracking()
               .Select(x => new GetSizeForUpdateDTO
               {
                   Id = x.Id,
                   Size = x.SizeNumber
               });
            return new SuccessDataResult<IQueryable<GetSizeForUpdateDTO>>(response:sizeQuery,HttpStatusCode.OK);
        }

        public async Task<IDataResult<PaginatedList<GetSizeDTO>>> GetAllSizeForTableAsync(int page)
        {
            IQueryable<GetSizeDTO> sizeQuery = _appDBContext.Sizes.AsNoTracking().Include(x=>x.SizeProducts)
                .Select(x => new GetSizeDTO
                {
                    Id = x.Id,
                  Size=x.SizeNumber,
                  StockCount=x.SizeProducts.Select(x=>x.StockCount).Sum()
                });
            var returnData = await PaginatedList< GetSizeDTO>.CreateAsync(sizeQuery, page, 10);
            return new SuccessDataResult<PaginatedList<GetSizeDTO>>(response: returnData, HttpStatusCode.OK);
        }

        public IDataResult<GetSizeForUpdateDTO> GetSize(Guid Id)
        {
            var sizeQuery = _appDBContext.Sizes.AsNoTracking().FirstOrDefault(x=>x.Id==Id);
            if (sizeQuery is null)
                return new ErrorDataResult<GetSizeForUpdateDTO>(statusCode: HttpStatusCode.NotFound);
           

            return new SuccessDataResult<GetSizeForUpdateDTO>(response: new GetSizeForUpdateDTO
            {
                Id = sizeQuery.Id,
                Size = sizeQuery.SizeNumber
            

            }, statusCode: HttpStatusCode.OK);

            
                }

        public IResult UpdateSize(UpdateSizeDTO updateSizeDTO)
        {
            try
            {
                var checekdSize = _appDBContext.Sizes.FirstOrDefault(x => x.Id == updateSizeDTO.Id);
                if (checekdSize is null)
                    return new ErrorResult(statusCode: HttpStatusCode.NotFound);
                checekdSize.SizeNumber = updateSizeDTO.NewSizeNumber;
                _appDBContext.Sizes.Update(checekdSize);
                _appDBContext.SaveChanges();
                return new SuccessResult(HttpStatusCode.OK);
                    
                
            }
            catch (Exception ex)
            {
                return new ErrorResult(message: ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
