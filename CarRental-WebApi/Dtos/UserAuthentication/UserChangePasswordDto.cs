using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental_WebApi.Dtos.UserAuthentication
{
    public class UserChangePasswordDto
    {
        public int Id { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}