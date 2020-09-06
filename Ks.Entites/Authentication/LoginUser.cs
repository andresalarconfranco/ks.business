using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ks.Entities.Authentication
{
    public class LoginUser
    {
        public string IdUser { get; set; }
        public string Password { get; set; }
        public int ROL_NCODE { get; set; }

    }
}
