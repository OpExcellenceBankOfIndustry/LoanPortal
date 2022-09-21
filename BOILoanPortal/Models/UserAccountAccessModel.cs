using System;
using System.Collections.Generic;
using System.Text;

namespace BOILoanPortal.Models
{

    public class AuthenticateUser
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    //public class AuthenticatedUser : BaseResponse
    //{
    //    public Loginviewmodel loginViewModel { get; set; }
    //}

    //public class Loginviewmodel
    //{
    //    public int userId { get; set; }
    //    public string email { get; set; }
    //    public string password { get; set; }
    //    public bool defaultPassword { get; set; }
    //    public string errorMessage { get; set; }
    //    public string token { get; set; }
    //}


    public class AuthenticatedUser : BaseResponse
    {
        public Userdetail? userDetail { get; set; }
    }

    public class Userdetail
    {
        public string? jwtToken { get; set; }
        public string? status { get; set; }
        public string? message { get; set; }
        public string? errorMessage { get; set; }
        public string? businessName { get; set; }
        public string? rcNumber { get; set; }
        public string? userRole { get; set; }
        public string? businessType { get; set; }
        public string? businessLocation { get; set; }
        public DateTime registeredDate { get; set; }
        public string? profileImage { get; set; }
        public Int32 id { get; set; }
        public string? userName { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
    }



    public class ChangePasswordRequest
    {
        public string? email { get; set; }
        public string? oldPassword { get; set; }
        public string? newPassword { get; set; }
        public string? confirmPassword { get; set; }
    }


    public class ChangePasswordResponse : BaseResponse
    {
        public Changepasswordviewmodel changePasswordViewModel { get; set; }
    }

    public class Changepasswordviewmodel
    {
        public string email { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }
    }


    public class ForgetPasswordRequest
    {
        public string email { get; set; }
    }


    public class ForgetPasswordResponse : BaseResponse
    {
        public Forgetpasswordviewmodel forgetPasswordViewModel { get; set; }
    }

    public class Forgetpasswordviewmodel
    {
        public string message { get; set; }
        public string errorMessage { get; set; }
        public string email { get; set; }
    }

}
