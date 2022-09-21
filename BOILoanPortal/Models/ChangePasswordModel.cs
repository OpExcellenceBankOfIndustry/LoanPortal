using System;
using System.Collections.Generic;
using System.Text;

namespace BOILoanPortal.Models
{
    public class ChangePasswordModel
    {
        public class ChangePasswordRequest
        {
            public string? email { get; set; }
            public string? oldPassword { get; set; }
            public string? newPassword { get; set; }
            public string? confirmPassword { get; set; }
        }

        public class ChangePasswordResponse
        {
            public string? email { get; set; }
            public string? oldPassword { get; set; }
            public string? newPassword { get; set; }
            public string? confirmPassword { get; set; }
            public string? errorMessage { get; set; }
            public bool success { get; set; }
            public string? message { get; set; }
        }

    }
}
