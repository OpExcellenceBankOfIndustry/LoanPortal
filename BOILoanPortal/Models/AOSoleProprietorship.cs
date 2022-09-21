using Microsoft.AspNetCore.Components.Forms;
using System;

namespace BOILoanPortal.Models
{
    public class AOSoleProprietorship
    {
        public string UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? RefNumber { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? OtherNames { get; set; }
        public string? MaidenName { get; set; }
        public String? DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? HomeAddress { get; set; }
        public IBrowserFile? ProofOfAddress { get; set; }
        public string? ProofOfAddressName { get; set; }
        public string? State { get; set; }
        public string? LGA { get; set; }
        public string? TownCity { get; set; }
        public string? HighestEducationalQualification { get; set; }
        public string? BVN { get; set; }
        public string? NIN { get; set; }
        public string? TIN { get; set; }
        public string? PercentageOwnership { get; set; }
        public string? Nationality { get; set; }
        public IBrowserFile? CERPAC { get; set; }
        public string? CERPACName { get; set; }
        public String? CERPACIssueDate { get; set; }
        public String? CERPACExpiryDate { get; set; }
        public string? IdentificationType { get; set; }
        public string? IdentificationNumber { get; set; }
        public IBrowserFile? IdentificationFile { get; set; }
        public string? IdentificationFileName { get; set; }
        public String? IdentificationIssueDate { get; set; }
        public String? IdentificationExpiryDate { get; set; }
        public IBrowserFile? PassportPhotograph { get; set; }
        public string? PassportPhotographName { get; set; }
        public string? Occupation { get; set; }
        public string? JobTitle { get; set; }
        public IBrowserFile? UploadCV { get; set; }
        public string? UploadCVName { get; set; }
        public string? AnyPoliticalAffilationWithAPoliticalOfficeHolder { get; set; }
        public string? IndicatePoliticalOfficerRelationship { get; set; }
        public string? ContestedPoliticalOffice { get; set; }
        public string? IndicatePoliticalOffice { get; set; }
        public List<AODetailsOfNextOfKin>? kins { get; set; }
        public List<AOAccountDetailsOfOwner> accts { get; set; }
    }
}
