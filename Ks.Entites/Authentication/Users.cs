using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ks.Entities.Authentication
{
    public class Users
    {
        public int UseNcode { get; set; }
        public string UseCemail { get; set; }
        public string UseCpassword { get; set; }
        public int role { get; set; }

    }
}
