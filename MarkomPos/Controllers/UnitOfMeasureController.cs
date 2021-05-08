using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarkomPos.DataInterface;
using MarkomPos.DataInterface.Common.Response;
using MarkomPos.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkomPos.Controllers
{
    [AllowAnonymous]
    public class UnitOfMeasureController : ControllerBase
    {
        private readonly IUnitOfMeasureService _unitOfMeasureService;
        public IResponseDTO _response;

        public UnitOfMeasureController(IUnitOfMeasureService unitOfMeasureService, IResponseDTO response)
        {
            _unitOfMeasureService = unitOfMeasureService;
            _response = response;
        }

        [HttpGet]
        public IResponseDTO GetAll()
        {
            _response = _unitOfMeasureService.GetAll();
            return _response;
        }

        [HttpPost]
        public async Task<IResponseDTO> Create(UnitOfMeasureDto unitOfMeasureDto)
        {
            var result = await _unitOfMeasureService.Create(unitOfMeasureDto);
            return result;
        }

        [HttpPut]
        public async Task<IResponseDTO> Update(UnitOfMeasureDto unitOfMeasureDto)
        {
            var result = await _unitOfMeasureService.Update(unitOfMeasureDto);
            return result;
        }

        [HttpDelete]
        public async Task<IResponseDTO> Delete(int measureId)
        {

            _response = await _unitOfMeasureService.Delete(measureId);
            return _response;
        }

    }
}