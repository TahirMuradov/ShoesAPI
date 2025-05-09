﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.PictureDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AllRole")]
    public class PictureController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddPicture([FromQuery] Guid ProductId, [FromForm] IFormFileCollection Pictures, [FromHeader] String LangCode)
        {
            var result = await _pictureService.AddPictureAsync(new AddPictureDTO { ProductId = ProductId, Pictures = Pictures }, LangCode);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpDelete("[action]")]
        public IActionResult DeletePicture([FromQuery] Guid PictureId)
        {
            var result = _pictureService.DeletePicture(PictureId);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPicture([FromQuery] int page)
        {
            var result = await _pictureService.GetAllPictureAsync(page);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
