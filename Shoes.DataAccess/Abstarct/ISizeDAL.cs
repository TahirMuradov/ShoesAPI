using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.SizeDTOs;

namespace Shoes.DataAccess.Abstarct
{
    public interface ISizeDAL
    {
        public IResult AddSize(AddSizeDTO addSizeDTO);
        public IResult UpdateSize(UpdateSizeDTO updateSizeDTO );
        public Task<IDataResult<PaginatedList<GetSizeDTO>>> GetAllSizeAsync(int page);
        public IDataResult<GetSizeForUpdateDTO> GetSize(Guid Id);
     public IResult DeleteSize(Guid Id);
    }
}
