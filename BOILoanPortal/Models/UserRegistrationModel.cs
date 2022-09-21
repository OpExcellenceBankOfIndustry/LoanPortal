using System;
using System.Collections.Generic;
using System.Text;

namespace BOILoanPortal.Models
{
    public class UserRegistrationModel
    {
        public class RegisterUserRequest
        {
            public string? email { get; set; }
            public string? phoneNumber { get; set; }
            public string? roleName { get; set; }
            public string? businessName { get; set; }
            public string? rcNumber { get; set; }
            public string? businessType { get; set; }
            public string? businessLocation { get; set; }
            public DateTime registeredDate { get; set; }
            public string? password { get; set; }
            public string? confirmPassword { get; set; }
        }


        public class RegisterUserResponse : BaseResponse
        {
            public userDetailViewModelRes? userDetailViewModel { get; set; }
        }

        public class userDetailViewModelRes
        {
            public string? modelMessage { get; set; }
            public string? errorMessage { get; set; }
        }

    }
}
