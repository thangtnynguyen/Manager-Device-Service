﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Device_Service.Domains.Model.Identity.User
{
    public class ConfirmEmailResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
