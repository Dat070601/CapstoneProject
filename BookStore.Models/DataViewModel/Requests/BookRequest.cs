﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Requests
{
    public class BookRequest 
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
