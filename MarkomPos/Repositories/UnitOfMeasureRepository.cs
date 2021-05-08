using MarkomPos.DataInterface;
using MarkomPos.DataInterface.Common.Response;
using MarkomPos.Entities;
using MarkomPos.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarkomPos.Repositories
{
    public class UnitOfMeasureRepository : IUnitOfMeasureService
    {
        private readonly ApplicationContext _ctx;
        private readonly UserManager<User> _userManager;
        private readonly IResponseDTO _response;

        public UnitOfMeasureRepository(
            ApplicationContext context,
            UserManager<User> userManager,
            IResponseDTO responseDTO)
        {
            _ctx = context;
            _userManager = userManager;
            _response = responseDTO;
        }


        public IResponseDTO GetAll()
        {
            var measureList = _ctx.UnitOfMeasures.ToList();

            _response.Data = measureList;
            _response.Message = "Ok";
            _response.IsPassed = true;

            return _response;
        }

        public async Task<IResponseDTO> Create(UnitOfMeasureDto unitOfMeasureDto)
        {
            try
            {
                UnitOfMeasure unitOfMeasure = new UnitOfMeasure()
                {
                    Name = unitOfMeasureDto.Name,
                    DisplayName = unitOfMeasureDto.DisplayName
                };
                int save = await _ctx.SaveChangesAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Faild to create the UnitOfMeasure";
                    return _response;
                }
                _response.IsPassed = true;
                _response.Message = "UnitOfMeasure created Successfully";
                _response.Data = unitOfMeasure;
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }
            return _response;
        }

        public async Task<IResponseDTO> Update(UnitOfMeasureDto unitOfMeasureDto)
        {
            try
            {
                var unitOfMeasure = await _ctx.UnitOfMeasures.FirstOrDefaultAsync(x => x.ID == unitOfMeasureDto.Id);
                unitOfMeasure.Name = unitOfMeasureDto.Name;
                unitOfMeasure.DisplayName = unitOfMeasureDto.DisplayName;
                int save = await _ctx.SaveChangesAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Faild to update the UnitOfMeasure";
                    return _response;
                }
                _response.IsPassed = true;
                _response.Message = "UnitOfMeasure updated Successfully";
                _response.Data = unitOfMeasure;
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }


            return _response;
        }

        public async Task<IResponseDTO> Delete(int measureId)
        {

            try
            {
                var unitOfMeasure = await _ctx.UnitOfMeasures.FirstOrDefaultAsync(x => x.ID == measureId);
                _ctx.UnitOfMeasures.Remove(unitOfMeasure);

                int save = await _ctx.SaveChangesAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Faild to delete the UnitOfMeasure";
                    return _response;
                }
                _response.IsPassed = true;
                _response.Message = "UnitOfMeasure deleted Successfully";
                _response.Data = unitOfMeasure;
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }


    }
}
