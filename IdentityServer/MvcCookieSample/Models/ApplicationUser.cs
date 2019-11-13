﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCookieSample.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Avatar { get; set; }
    }
}
