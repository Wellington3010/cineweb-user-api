using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_user_api.Util
{
    public interface ICriptography
    {
        string Encrypt(string value);

        string Decrypt(string value);
    }
}
