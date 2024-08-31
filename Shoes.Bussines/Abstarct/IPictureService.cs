using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.PictureDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface IPictureService
    {
        public Task<IResult> AddPictureAsync(AddPictureDTO addPictureDTO,string LangCode);
        public IResult DeletePicture(Guid Id);
        public Task<IDataResult<PaginatedList<GetPictureDTO>>> GetAllPictureAsync(int page);
    }
}
