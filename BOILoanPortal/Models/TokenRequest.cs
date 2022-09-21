using System;
using System.Collections.Generic;
using System.Text;

namespace BOILoanPortal.Models
{
    public partial class TokenRequest
    {
        public string? userName { get; set; }
        public string? password { get; set; }
        public string? channel { get; set; }
    }


    public class TokenResponse
    {
        public Authenticateuserviewmodel? authenticateUserViewModel { get; set; }
        public bool success { get; set; }
        public string? message { get; set; }
    }

    public class Authenticateuserviewmodel
    {
        public string? bearerToken { get; set; }
        public DateTime expiryPeriod { get; set; }
    }
}
