using MarkomPos.DataInterface.Common.Response;
using System.Threading.Tasks;

namespace MarkomPos.Interface
{
    public interface IUserService
    {
        Task<IResponseDTO> Login(string username, string password);
    }
}
