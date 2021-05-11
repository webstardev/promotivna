using MarkomPos.IRepository;
using MarkomPoss.Model.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkomPos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitOfMeasureController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public UnitOfMeasureController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<UnitOfMeasure>> Get()
        {
            return await _unitOfWork.UnitOfMeasure.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<UnitOfMeasure> Get(int id)
        {
            return await _unitOfWork.UnitOfMeasure.Get(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UnitOfMeasure unitOfMeasure)
        {
            _unitOfWork.UnitOfMeasure.Add(unitOfMeasure);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UnitOfMeasure unitOfMeasure)
        {
            unitOfMeasure.ID = id;
            _unitOfWork.UnitOfMeasure.Update(unitOfMeasure);
            _unitOfWork.Complete();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var unitOfMeasure = await _unitOfWork.UnitOfMeasure.Get(id);
            if(unitOfMeasure != null)
            {
                _unitOfWork.UnitOfMeasure.Delete(unitOfMeasure);
                _unitOfWork.Complete();
            }            
        }
    }
}
