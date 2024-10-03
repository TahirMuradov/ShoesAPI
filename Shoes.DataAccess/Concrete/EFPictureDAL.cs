using Microsoft.EntityFrameworkCore;
using Shoes.Core.Helpers.FileHelper;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.Core.Utilites.Results.Concrete.SuccessResults;
using Shoes.DataAccess.Abstarct;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites;
using Shoes.Entites.DTOs.PictureDTOs;
using System.Net;
using System.Threading.Tasks;

namespace Shoes.DataAccess.Concrete
{
    public class EFPictureDAL :IPictureDAL
    {
        private readonly AppDBContext _appDBContext;

        public EFPictureDAL(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<IResult> AddPictureAsync(AddPictureDTO addPictureDTO)
        {
            var product = _appDBContext.Products.FirstOrDefault(x => x.Id == addPictureDTO.ProductId);
            if (product == null) return new ErrorResult(HttpStatusCode.NotFound);

            List<string> urls = await FileHelper.PhotoFileSaveRangeAsync(addPictureDTO.Pictures);
            foreach (var url in urls)
            {

                Picture picture = new Picture()
                {
                    ProductId = addPictureDTO.ProductId,
                    Url =url
            };
                _appDBContext.Pictures.Add(picture);
            }
            _appDBContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK);
        }

        public IResult DeletePicture(Guid Id)
        {
          var picture=_appDBContext.Pictures.FirstOrDefault(x=>x.Id == Id);
            if (picture is null)
                return new ErrorResult(HttpStatusCode.NotFound);
       FileHelper.RemoveFile(picture.Url);
         
            _appDBContext.Pictures.Remove(picture);
            _appDBContext.SaveChanges();
            return new SuccessResult(HttpStatusCode.OK); 
        }

        public async Task<IDataResult<PaginatedList<GetPictureDTO>>> GetAllPictureAsync(int page)
        {
            var picture=_appDBContext.Pictures.AsNoTracking().AsSplitQuery().AsQueryable().Select(x=>new GetPictureDTO
            {
                Id = x.Id,
                Path=x.Url,
                ProductCode=x.Product.ProductCode
            });
            var result = await PaginatedList<GetPictureDTO>.CreateAsync(picture, page, 10);
            return new SuccessDataResult<PaginatedList<GetPictureDTO>>(result, HttpStatusCode.OK);
        }
    }
}
