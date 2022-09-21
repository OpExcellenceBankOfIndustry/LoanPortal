using Microsoft.AspNetCore.Components.Forms;
using System;
using System.ComponentModel.DataAnnotations;

namespace BOILoanPortal.Models
{
    public class AOOwnershipInformationIndividual
    {
        public string UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? RefNumber { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string? Surname { get; set; }
        public string? OtherNames { get; set; }
        public string? MaidenName { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        public String? DateOfBirth { get; set; }
        [Required(ErrorMessage = "Place of Birth is required")]
        public string? PlaceOfBirth { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Marital Status is required")]
        public string? MaritalStatus { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public Int64 PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email Address is required")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Home Address is required")]
        public string? HomeAddress { get; set; }
        [Required(ErrorMessage = "Position is required")]
        public string? Position { get; set; }
        [Required(ErrorMessage = "Proof of Address is required")]
        public IBrowserFile? ProofOfAddress { get; set; }
        public string? ProofOfAddressName { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string? State { get; set; }
        [Required(ErrorMessage = "LGA is required")]
        public string? LGA { get; set; }
        [Required(ErrorMessage = "Town/City is required")]
        public string? TownCity { get; set; }
        [Required(ErrorMessage = "Highest Educational Qualification is required")]
        public string? HighestEducationalQualification { get; set; }
        [Required(ErrorMessage = "BVN is required")]
        public string? BVN { get; set; }
        [Required(ErrorMessage = "NIN is required")]
        public string? NIN { get; set; }
        [Required(ErrorMessage = "TIN is required")]
        public string? TIN { get; set; }
        [Required(ErrorMessage = "Percentage Ownership is required")]
        public string? PercentageOwnership { get; set; }
        [Required(ErrorMessage = "Nationality is required")]
        public string? Nationality { get; set; }
        public IBrowserFile? CERPAC { get; set; }
        public string? CERPACName { get; set; }
        public String? CERPACIssueDate { get; set; }
        public String? CERPACExpiryDate { get; set; }
        [Required(ErrorMessage = "Identification Type is required")]
        public string? IdentificationType { get; set; }
        [Required(ErrorMessage = "Identification Number is required")]
        public string? IdentificationNumber { get; set; }
        [Required(ErrorMessage = "Identification card is required")]
        public IBrowserFile? IdentificationFile { get; set; }
        public string? IdentificationFileName { get; set; }
        [Required(ErrorMessage = "ID Issue Date is required")]
        public String? IdentificationIssueDate { get; set; }
        public String? IdentificationExpiryDate { get; set; }
        [Required(ErrorMessage = "Passport Photograph is required")]
        public IBrowserFile? PassportPhotograph { get; set; }
        public string? PassportPhotographName { get; set; }
        [Required(ErrorMessage = "Occupation is required")]
        public string? Occupation { get; set; }
        public string? JobTitle { get; set; }
        [Required(ErrorMessage = "CV is required")]
        public IBrowserFile? UploadCV { get; set; }
        public string? UploadCVName { get; set; }
        [Required(ErrorMessage = "Political Affilation is required")]
        public string? AnyPoliticalAffilationWithAPoliticalOfficeHolder { get; set; }
        public string? IndicatePoliticalOfficerRelationship { get; set; }
        public string? ContestedPoliticalOffice { get; set; }
        public string? IndicatePoliticalOffice { get; set; }
    }
}
