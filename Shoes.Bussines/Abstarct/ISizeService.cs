using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.SizeDTOs;

namespace Shoes.Bussines.Abstarct
{
    public interface ISizeService
    {
        public IResult AddSize(AddSizeDTO addSizeDTO,string langCode);
        public IResult UpdateSize(UpdateSizeDTO updateSizeDTO, string langCode);
        public Task<IDataResult<PaginatedList<GetSizeDTO>>> GetAllSizeForTableAsync(int page);
        public IDataResult<IQueryable<GetSizeForUpdateDTO>> GetAllSize();
        public IDataResult<GetSizeForUpdateDTO> GetSize(Guid Id);
        public IResult DeleteSize(Guid Id);
    }
}
