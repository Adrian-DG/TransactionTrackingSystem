using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class AuthController : BaseController
	{
		private IUnitOfWork _uow;
		private IConfiguration _configuration;
		public AuthController(IUnitOfWork unitOfWork, IConfiguration configuration)
		{
			_uow = unitOfWork;
			_configuration = configuration;
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] UserModelDTO model)
		{
			try
			{
				var response = (ServerResponse) await _uow.AuthRepository.Register(model);
				return Ok(response);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserModelDTO model)
		{
			try
			{
				string secretKey = _configuration.GetSection("SecretKey").Value;
				var response = (LoginResponse) await _uow.AuthRepository.Login(model, secretKey);
				return Ok(response);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpGet]
		[Authorize]
		public IActionResult GetDemoResponse()
		{
			return new JsonResult(new { message = "user authorized" });
		}

	}
}
