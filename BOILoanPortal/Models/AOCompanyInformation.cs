using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace BOILoanPortal.Models
{
    public class AOCompanyInformation
    {
        public string? UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? RefNumber { get; set; }
        [Required(ErrorMessage = "CompanyName is required")]
        public string? CompanyName { get; set; }
        public string? CompanyShortName { get; set; }
        [Required(ErrorMessage = "CompanyType is required")]
        public string? CompanyType { get; set; }

        [Required(ErrorMessage = "CompanyAddress is required")]
        public string? CompanyAddress { get; set; }
        public string? Landmark { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string? State { get; set; }

        [Required(ErrorMessage = "LGA is required")]
        public string? LGA { get; set; }

        [Required(ErrorMessage = "Town/City is required")]
        public string? TownCity { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [DataType(DataType.PhoneNumber)]
        public Int64 PhoneNumber { get; set; }
        [Required(ErrorMessage = "EmailAddress is required")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }
        public string? Website { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? CompanyOwnsBusinessPremises { get; set; }
        [Required(ErrorMessage = "Proof of Company Address is required")]
        public IBrowserFile? ProofOfCompanyAddress { get; set; }
        public string? ProofOfCompanyAddressName { get; set; }
        public bool HaveCoyPrem { get; set; }
        //public DateTime testdate { get; set; }
    }
}
