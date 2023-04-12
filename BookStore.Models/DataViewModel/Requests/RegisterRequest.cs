using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Requests
{
    public class RegisterRequest
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password not match !")]
        public string? ConfirmPassWord { get; set; }
    }
}
