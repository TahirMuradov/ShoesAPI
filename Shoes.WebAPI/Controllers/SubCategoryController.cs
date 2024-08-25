﻿using Microsoft.AspNetCore.Mvc;
using Shoes.Bussines.Abstarct;
using Shoes.Entites.DTOs.SubCategoryDTOs;

namespace Shoes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpPost("[action]")]
        public IActionResult AddSubCategory([FromBody] AddSubCategoryDTO subCategoryDTO, [FromHeader] string LangCode)
        {
            var result = _subCategoryService.AddSubCategory(subCategoryDTO, LangCode);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("[action]")]
        public IActionResult UpdateSubCategory([FromBody] UpdateSubCategoryDTO subCategoryDTO, [FromHeader] string LangCode)
        {
            var result = _subCategoryService.UpdateSubCategory(subCategoryDTO, langCode: LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("[action]")]
        public IActionResult DeleteSubCategory([FromQuery] Guid Id)
        {
            var result = _subCategoryService.DeleteSubCategory(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
       public IActionResult GetSubCategory([FromQuery]Guid Id,[FromHeader]string LangCode)
        {
            var result=_subCategoryService.GetSubCategory(Id, LangCode);
            return result.IsSuccess? Ok(result):BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllSubCategory( [FromHeader] string LangCode)
        {
            var result = await _subCategoryService.GetAllSubCategoryAsync(LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}