using cineweb_user_api.Context;
using cineweb_user_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_user_api.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly UserContext _userContext;
        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }
        public void Delete(User entity)
        {
            _userContext.Remove(entity);
            _userContext.SaveChanges();
        }

        public User FindById(Guid Id)
        {
            return _userContext.Users.Where(x => x.Id == Id).FirstOrDefault();
        }

        public User FindByEmail(string email)
        {
            return _userContext.Users.Where(x => x.Email == email).FirstOrDefault();           
        }

        public void Save(User entity)
        {
            _userContext.Users.Add(entity);
            _userContext.SaveChanges();
        }

        public void Update(User entity)
        {
            _userContext.Entry<User>(entity).State = EntityState.Modified;
            _userContext.SaveChanges();
;        }

        public List<User> FindAll()
        {
            return _userContext.Users.ToList();
        }
    }
}
