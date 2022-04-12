using AutoMapper;
using cineweb_user_api.DTO;
using cineweb_user_api.Models;
using cineweb_user_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_user_api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<User> _userRepository;
        public UserController(IMapper mapper, IBaseRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login(string login)
        {
            return Json("");
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
