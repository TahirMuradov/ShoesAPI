using Shoes.Bussines.Abstarct;
using Shoes.Bussines.FluentValidations.PictureDTOValidations;
using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Core.Utilites.Results.Concrete.ErrorResults;
using Shoes.DataAccess.Abstarct;
using Shoes.Entites.DTOs.PictureDTOs;
using System.Net;

namespace Shoes.Bussines.Concrete
{
    public class PictureManager : IPictureService
    {
        private readonly IPictureDAL _pictureDAL;

        public PictureManager(IPictureDAL pictureDAL)
        {
            _pictureDAL = pictureDAL;
        }

        public async Task<IResult> AddPictureAsync(AddPictureDTO addPictureDTO, string LangCode)
        {
            AddPictureValidation validationRules = new AddPictureValidation(LangCode);
            var validationresult= await validationRules.ValidateAsync(addPictureDTO);
            if (!validationresult.IsValid)
            {
                List<string> errors = validationresult.Errors.Select(x => x.ErrorMessage).ToList();
                return new ErrorResult(messages: errors, HttpStatusCode.BadRequest);
            }
            return await _pictureDAL.AddPictureAsync(addPictureDTO);
        }

        public IResult DeletePicture(Guid Id)
        {
            if (Id == default)
                return new ErrorResult(HttpStatusCode.BadRequest);
            return _pictureDAL.DeletePicture(Id);
        }

        public async Task<IDataResult<PaginatedList<GetPictureDTO>>> GetAllPictureAsync(int page)
        {
            if (page <= 0)
                page = 1;
            return await _pictureDAL.GetAllPictureAsync(page);
        }
    }
}
