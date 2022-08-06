using AutoMapper;
using cineweb_user_api.Context;
using cineweb_user_api.DTO;
using cineweb_user_api.Models;
using cineweb_user_api.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_user_api.Repositories
{
    public class UserRepository : IBaseRepository<UserRegisterDTO>
    {
        private readonly UserContext _userContext;
        private readonly IMapper _mapper;
        private readonly ICriptography _criptography;

        public UserRepository(UserContext userContext, IMapper mapper, ICriptography criptography)
        {
            _userContext = userContext;
            _mapper = mapper;
            _criptography = criptography;
        }
        public void Delete(User entity)
        {
            _userContext.Remove(entity);
            _userContext.SaveChanges();
        }

        public UserRegisterDTO FindById(Guid Id)
        {
            try
            {
                return _mapper.Map<UserRegisterDTO>(_userContext.Users.Where(x => x.Id == Id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserRegisterDTO FindByEmail(string email)
        {
            try
            {
                return _mapper.Map<UserRegisterDTO>(_userContext.Users.Where(x => x.Email == email).FirstOrDefault());           
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Save(UserRegisterDTO entity)
        {
            try
            {
                var user = _mapper.Map<User>(entity);
                user.Id = Guid.NewGuid();
                user.RegisterDate = DateTime.Now;
                user.UpdatedDate = user.RegisterDate;
                user.Password = _criptography.Encrypt($"{user.Email}:{user.Password}");
                _userContext.Users.Add(user);
                _userContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(UserRegisterDTO entity)
        {
            try
            {
                var newUser = _mapper.Map<User>(entity);
                var oldUser = _userContext.Users.Where(x => x.Email == newUser.Email).FirstOrDefault();
                oldUser.Name = newUser.Name;
                oldUser.Password = _criptography.Encrypt($"{newUser.Email}:{newUser.Password}");
                oldUser.UpdatedDate = DateTime.Now;
                _userContext.Entry<User>(oldUser).State = EntityState.Modified;
                _userContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UserRegisterDTO> FindAll()
        {
            try
            {
                List<UserRegisterDTO> list = new List<UserRegisterDTO>();
                _userContext.Users.ToList().ForEach(x => list.Add(_mapper.Map<UserRegisterDTO>(x)));
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserRegisterDTO FindByPassword(string password)
        {
            try
            {
                return _mapper.Map<UserRegisterDTO>(_userContext.Users.Where(x => x.Password == password).FirstOrDefault());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(string email)
        {
            try
            {
                var userToRemove = _userContext.Users.Where(x => x.Email == email);
                _userContext.Remove(userToRemove);
                _userContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}