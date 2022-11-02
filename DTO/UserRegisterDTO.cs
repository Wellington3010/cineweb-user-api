using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_user_api.DTO
{
    public class UserRegisterDTO
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public string Senha { get; set; }

        public bool AdminUser { get; set; }
    }
}
