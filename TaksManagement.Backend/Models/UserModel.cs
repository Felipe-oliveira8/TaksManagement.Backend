using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaksManagement.Backend.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
    }
}
