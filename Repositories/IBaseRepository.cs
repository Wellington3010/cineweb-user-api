using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_user_api.Repositories
{
    public interface IBaseRepository<T> where T: class
    {
        void Save(T entity);

        void Update(T entity);

        void Delete(T entity);

        T FindById(Guid Id);

        List<T> FindAll();
        
        T FindByEmail(string email);
    }
}
