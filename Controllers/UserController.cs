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
        private readonly IMapper _mapper;
        private readonly IBaseRepository<User> _userRepository;
        private readonly ICriptography _criptography;
        public UserController(IMapper mapper, IBaseRepository<User> userRepository, ICriptography criptography)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _criptography = criptography;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] UserLoginDTO userLoginRequest)
        {
            var userLogin = _criptography.Encrypt($"{userLoginRequest.email}:{userLoginRequest.password}");

            var user = _userRepository.FindByPassword(userLogin);

            if (user == null)
                return BadRequest();

            return Json($"{Guid.NewGuid()}_{DateTime.Now}_{user.Name}");
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register([FromBody] UserRegisterDTO userRegisterRequest)
        {
            var newUser = new User();
            newUser.Id = Guid.NewGuid();
            newUser.Email = userRegisterRequest.email;
            newUser.Name = userRegisterRequest.name;
            newUser.Password = _criptography.Encrypt($"{userRegisterRequest.email}:{userRegisterRequest.password}");
            newUser.RegisterDate = DateTime.Now;
            newUser.UpdatedDate = DateTime.Now;

            _userRepository.Save(newUser);

            return Json($"{Guid.NewGuid()}_{DateTime.Now}_{userRegisterRequest.name}");
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
        public ActionResult Save(AdminUserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            user.UpdatedDate = DateTime.Now;
            user.RegisterDate = DateTime.Now;
            _userRepository.Save(user);

            return Ok();
        }


        [HttpPost]
        [Route("admin/update")]
        public ActionResult Update(AdminUserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            user.UpdatedDate = DateTime.Now;
            _userRepository.Update(user);

            return Ok();
        }

        [HttpDelete]
        [Route("admin/delete")]
        public ActionResult Delete(Guid id)
        {
            var user = _userRepository.FindById(id);
            _userRepository.Delete(user);
            return Ok();
        }
    }
}
