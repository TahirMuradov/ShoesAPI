﻿using Shoes.Core.Helpers.PageHelper;
using Shoes.Core.Utilites.Results.Abstract;
using Shoes.Entites.DTOs.PictureDTOs;

namespace Shoes.DataAccess.Abstarct
{
    public interface IPictureDAL
    {
        public Task<IResult> AddPictureAsync(AddPictureDTO addPictureDTO);
        public IResult DeletePicture(Guid Id);
        public Task<IDataResult<PaginatedList<GetPictureDTO>>> GetAllPictureAsync(int page);

    }
}
