using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Product.API.Errors;
using Product.API.Extensions;
using Product.Core.Dto;
using Product.Core.Entities;
using Product.Core.Services;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly ITokenServices _tokenServices;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<AppUsers> userManager, ITokenServices tokenServices, SignInManager<AppUsers> signInManager, IMapper mapper, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _tokenServices = tokenServices;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
            {
                return Unauthorized(new BaseCommonResponse(401));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (result is null || result.Succeeded == false)
            {
                return Unauthorized(new BaseCommonResponse(401));
            }
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenServices.CreateToken(user)
            });
        }
        [HttpGet("check-email-exist")]
        public async Task<ActionResult<bool>> CheckEmailExist([FromQuery] string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result is not null)
            {
                return true;
            }
            return false;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (CheckEmailExist(dto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = new[] { "This Email is Already Token" }
                });
            }
            var user = new AppUsers
            {
                DisplayName = dto.DisplayName,
                UserName = dto.Email,
                Email = dto.Email,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result is null || result.Succeeded == false)
            {
                return BadRequest(new BaseCommonResponse(400));
            }
            return Ok(new UserDto
            {
                DisplayName = dto?.DisplayName,
                Email = dto?.Email,
                Token = _tokenServices.CreateToken(user)
            });
        }
        [Authorize]
        [HttpGet("test")]
        public ActionResult<string> Test()
        {
            _logger.LogInformation("test called.");
            return "Hi";
        }

        [Authorize]
        [HttpGet("get-current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.FindEmailByClaimPrincipal(HttpContext.User);
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenServices.CreateToken(user)
            });
        }

        [Authorize]
        [HttpGet("get-user-address")]
        public async Task<IActionResult> GetUserAddress()
        {
            var user = await _userManager.FindUserByClaimPrincipalWithAddress(HttpContext.User);
            var result = _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("update-user-address")]
        public async Task<IActionResult> UpdateUserAddress(AddressDto dto)
        {
            var user = await _userManager.FindUserByClaimPrincipalWithAddress(HttpContext.User);
            user.Address = _mapper.Map<AddressDto, Address>(dto);
            var result = await _userManager.UpdateAsync(user);
            if (result is not null && result.Succeeded)
            {
                return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            }
            return BadRequest($"Problem in Updating this {HttpContext.User}");
        }
    }
}
