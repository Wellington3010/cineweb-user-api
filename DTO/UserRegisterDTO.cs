using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_user_api.DTO
{
    public class UserRegisterDTO
    {
        public string name { get; set; }

        public string email { get; set; }

        public string password { get; set; }
    }
}
