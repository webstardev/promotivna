using MarkomPos.DataInterface;
using MarkomPos.DataInterface.Common.Response;
using System.Threading.Tasks;

namespace MarkomPos.Interface
{
    public interface IUnitOfMeasureService
    {
        IResponseDTO GetAll();
        Task<IResponseDTO> Create(UnitOfMeasureDto unitOfMeasureDto);
        Task<IResponseDTO> Update(UnitOfMeasureDto unitOfMeasureDto);
        Task<IResponseDTO> Delete(int measureId);
    }
}
