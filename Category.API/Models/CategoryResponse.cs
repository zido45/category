﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Category.API.Models
{
    public class CategoryResponse
    {

        public int CategoryId { get; set; }
        public string Description { get; set; }
        public List<ProductResponse> Products { get; set; }
    }
}