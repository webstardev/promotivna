using MarkomPos.DataInterface.Common.Response;
using MarkomPos.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarkomPos.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public IResponseDTO _response;

        public UserController(IUserService userService, IResponseDTO response)
        {
            _userService = userService;
            _response = response;
        }

        [HttpPost]
        public async Task<IResponseDTO> Login(string username, string password)
        {
            _response = await _userService.Login(username, password);
            return _response;
        }

    }
}
