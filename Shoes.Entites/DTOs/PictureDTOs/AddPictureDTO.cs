using Microsoft.AspNetCore.Http;

namespace Shoes.Entites.DTOs.PictureDTOs
{
    public class AddPictureDTO
    {
        public Guid ProductId { get; set; }
        public IFormFileCollection Pictures { get; set; }


    }
}
