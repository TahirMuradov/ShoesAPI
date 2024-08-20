using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.SizeDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface ISizeService
    {
        public IResult AddSize(AddSizeDTO addSizeDTO,string langCode);
        public IResult UpdateSize(UpdateSizeDTO updateSizeDTO, string langCode);
        public Task<IDataResult<PaginatedList<GetSizeDTO>>> GetAllSizeAsync(int page);
        public IDataResult<GetSizeDTO> GetSize(Guid Id);
        public IResult DeleteSize(Guid Id);
    }
}
