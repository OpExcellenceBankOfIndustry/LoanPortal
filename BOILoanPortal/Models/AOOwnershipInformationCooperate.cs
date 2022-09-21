using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace BOILoanPortal.Models
{
    public class AOOwnershipInformationCooperate
    {
        public string? UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? RefNumber { get; set; }
        [Required(ErrorMessage = "Name of Shareholding Company is required")]
        public string? NameOfShareholdingCompany { get; set; }
        [Required(ErrorMessage = "Registration Number is required")]
        public string? RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Percentage Ownership is required")]
        public string? PercentageOwnership { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string? State { get; set; }
        [Required(ErrorMessage = "LGA is required")]
        public string? LGA { get; set; }
        [Required(ErrorMessage = "Town City is required")]
        public string? TownCity { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email Address is required")]
        public string? EmailAddress { get; set; }
        public string? Website { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        [Required(ErrorMessage = "Company Address is required")]
        public string? CompanyAddress { get; set; }
        [Required(ErrorMessage = "Contact Person Name is required")]
        public string? ContactPersonName { get; set; }
        [Required(ErrorMessage = "Certificate of Incorporation is required")]
        public IBrowserFile? CertificateOfIncorporationRegistration { get; set; }
        public string? CertificateOfIncorporationRegistrationName { get; set; }
    }
}
