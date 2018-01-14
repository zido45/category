using Category.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Category.API.Models
{
    public class ProductRequest:Products
    {
      public byte[] ImageArray { get; set; }
    }
}