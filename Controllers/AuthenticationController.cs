using Authentication_Service.DTOs.API;
using Authentication_Service.DTOs.Users;
using Authentication_Service.Mappers;
using Authentication_Service.Models;
using Authentication_Service.Records;
using Authentication_Service.Repositories;
using Authentication_Service.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_Service.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IUserService _userservice;

        public AuthenticationController(IAuthRepository authRepository,IUserService userService)
        {
            this._repo = authRepository;
            this._userservice = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] DTOs.Users.RegisterRequest request)
        {
            Users? createdUser = await this._repo.RegisterUser(user: UserMapper.RegisterRequestToModel(request));
            if (createdUser == null)
            {
                return BadRequest(new CustomAPIResponse<object>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Data = null,
                    Message = "User registration failed"
                });
            }
                

           return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, new CustomAPIResponse<CreatedUserResponse>
            {
                Code = StatusCodes.Status201Created,
                Data = UserMapper.ModelToCreatedUserResponse(createdUser),
               Message = "User registered successfully"
            });
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await this._repo.GetUserById(id);

            if (user == null)
            {
                return NotFound(new CustomAPIResponse<object>
                {
                    Code = StatusCodes.Status404NotFound,
                    Data = null,
                    Message = "User not found"
                });
            }

            return Ok(new CustomAPIResponse<Users>
            {
                Code = StatusCodes.Status200OK,
                Data = user,
                Message = "User retrieved successfully"
            });
        }
        [HttpGet]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            LoginResult result =await this._userservice.AuthenticateUser(request.Email , request.Password);



            if (!result.IsValid && result.Token.Equals(string.Empty))
            {
                return Unauthorized(new CustomAPIResponse<object>
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Data = null,
                    Message = "Invalid email or password"
                });
            }
            return Ok(new CustomAPIResponse<string>
            {
                Code = StatusCodes.Status200OK,
                Data = result.Token,
                Message = "Login successful"
            });
        }
    }
}
