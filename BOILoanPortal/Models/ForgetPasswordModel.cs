using System;
using System.Collections.Generic;
using System.Text;

namespace BOILoanPortal.Models
{
    public class ForgetPasswordModel
    {
        public class ForgetPasswordRequest
        {
            public string? email { get; set; }
        }

        public class ForgetPasswordResponse
        {
            public string? email { get; set; }
            public bool success { get; set; }
            public string? errorMessage { get; set; }
            public string? message { get; set; }
        }
    }
}
