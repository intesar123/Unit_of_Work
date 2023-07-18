using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingVilla.Data.Entities;
using ShoppingVilla.Data.Entities.Interface;
using ShoppingVilla.Data.Entities.UnitOfWork;

namespace ShoppingVillaAPi.Controllers
{
    [Route("api/Account/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users= await _unitOfWork.userRegisterRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet(Name = "Get/{Id}")]
        public async Task<UserRegister> Get(int Id)
        {
            return await _unitOfWork.userRegisterRepository.GetByIdAsync(Id);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegister user)
        {
             _unitOfWork.userRegisterRepository.CreateAsync(user);
            var result=  await _unitOfWork.SaveChangesAsync();
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserRegister user)
        {
             _unitOfWork.userRegisterRepository.UpdateAsync(user);
            var result= await _unitOfWork.SaveChangesAsync();
            return Ok(result);
        }
        [HttpDelete(Name = "Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var user = await _unitOfWork.userRegisterRepository.GetByIdAsync(Id);
            _unitOfWork.userRegisterRepository.DeleteAsync(user);
            var result =await _unitOfWork.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            var userData =_unitOfWork.userLoginRepository.Login(user);
            //var result = await _unitOfWork.SaveChangesAsync();
            return Ok( new { Token = userData.Result.Token });
        }
    }
}
