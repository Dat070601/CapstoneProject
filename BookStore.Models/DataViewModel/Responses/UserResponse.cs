using BookStore.Models.DataViewModel;
using BookStore.Models.DTOs.Responses.Base;
using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DTOs.Responses
{
    public class UserResponse : GeneralResponses
    {
        public Account? User { get; set; }
        public UserViewModel? UserViewModel { get; set; }
    }
}
