using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace BOILoanPortal.Models
{
    public class AORegulatoryInformation
    {
        //public long Id { get; set; }
        public string? UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? RefNumber { get; set; }
        [Required(ErrorMessage = "RC Number is required")]
        public string? CompanyRegistrationNumber { get; set; }
        [Required(ErrorMessage = "Date of Incorporation is required")]
        //[DisplayFormat(DataFormatString = "{yyyy/MM/dd}")]
        public String? DateOfIncorporationRegistration { get; set; }
        [Required(ErrorMessage = "Date Business Started is required")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy/MM/dd}")]
        public String? DateBusinessStarted { get; set; }
        [Required(ErrorMessage = "TIN is required")]
        public string? TIN { get; set; }
        public string? SCUMLNumber { get; set; }
        [Required(ErrorMessage = "Certificate of Incorporation is required")]
        public IBrowserFile? CertificateOfIncorporationRegistration { get; set; }
        public string? CertificateOfIncorporationRegistrationName { get; set; }
        [Required(ErrorMessage = "Form CO2 is required")]
        public IBrowserFile? FormCO2AllotmentOfShares { get; set; }
        public string? FormCO2AllotmentOfSharesName { get; set; }
        [Required(ErrorMessage = "Form CO7 is required")]
        public IBrowserFile? FormCO7ParticularsOfDirectors { get; set; }
        public string? FormCO7ParticularsOfDirectorsName { get; set; }

        [Required(ErrorMessage = "Memart is required")]
        public IBrowserFile? Memart { get; set; }
        public string? MemartName { get; set; }
        [Required(ErrorMessage = "Board Resolution is required")]
        public IBrowserFile? BoardResolution { get; set; }
        public string? BoardResolutionName { get; set; }

        public string? AuthorisedShareCapital { get; set; }
        public string? PaidUpCapital { get; set; }
        public string? AnnualTurnover { get; set; }
        public string? CurrentNumberOfEmployees { get; set; }
        public string? ParentCompany { get; set; }
        public string? SubsidiariesAffiliates { get; set; }
        public string? NatureOfBusiness { get; set; }
        public string? PreviousBusinessEngagedIn { get; set; }
        public string? OrganizationMembership { get; set; }
        public string? NameOfOrganization { get; set; }
        public string? MembershipNumber { get; set; }
        public String? JoinedDate { get; set; }
        public string? PresentThreatenedLitigationWithThirdParty { get; set; }
        public string? ThirdPartyName { get; set; }
        public string? SuitNumber { get; set; }
        public string? IsYourCompanyQuotedOnAnyStockExchange { get; set; }
        public string? IndicateStockSymbol { get; set; }
        public string? StockExchange { get; set; }
    }
}
