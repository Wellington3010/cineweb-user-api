using AutoMapper;
using cineweb_user_api.DTO;
using cineweb_user_api.Models;
using cineweb_user_api.Repositories;
using cineweb_user_api.Util;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace cineweb_user_api.Controllers
{
    [Route("users")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IBaseRepository<UserRegisterDTO> _userRepository;
        private readonly ICriptography _criptography;
        public UserController(IMapper mapper, IBaseRepository<UserRegisterDTO> userRepository, ICriptography criptography)
        {
            _userRepository = userRepository;
            _criptography = criptography;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] UserLoginDTO userLoginRequest)
        {
            var userLogin = _criptography.Encrypt($"{userLoginRequest.Email}:{userLoginRequest.Senha}");

            var user = _userRepository.FindByPassword(userLogin);

            if (user == null)
                return BadRequest();

            return Json($"{Guid.NewGuid()}_{DateTime.Now}_{user.Nome}_{user.AdminUser}_{user.CPF}");
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register([FromBody] UserRegisterDTO userRegisterRequest)
        {
            _userRepository.Save(userRegisterRequest);

            return Json($"{Guid.NewGuid()}_{DateTime.Now}_{userRegisterRequest.Nome}_{false}");
        }

        [HttpGet]
        [Route("admin/findAll")]
        public ActionResult FindAll()
        {
            var users = _userRepository.FindAll();
            return Json(users);
        }

        [HttpPost]
        [Route("admin/save")]
        public ActionResult Save(UserRegisterDTO userDTO)
        {
            _userRepository.Save(userDTO);

            return Ok();
        }


        [HttpPost]
        [Route("admin/update")]
        public ActionResult Update(UserRegisterDTO userDTO)
        {
            _userRepository.Update(userDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("admin/delete")]
        public ActionResult Delete(string email)
        {
            _userRepository.Delete(email);
            return Ok();
        }
    }
}
