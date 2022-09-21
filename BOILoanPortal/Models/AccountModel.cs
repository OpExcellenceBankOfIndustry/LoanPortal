using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BOILoanPortal.Models
{
    public enum Tier
    {
        Tier1 = 1,
        Tier2 = 2,
        Tier3 = 3
    }

    public class IndividualOwnershipFiles
    {
        public object? ProofOfAddress { get; set; }
        public object? CERPAC { get; set; }
        public object? IdentificationFile { get; set; }
        public object? PassportPhotograph { get; set; }
        public object? UploadCV { get; set; }
    }

    public class TextTab
    {
        public DataTable TextTabData { get; set; }
        public DataTable dtBranches { get; set; }
    }

    public class CustomerInfo
    {
        // coy info
        public string? UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? RefNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyShortName { get; set; }
        public string? CompanyType { get; set; }

        public string? CompanyAddress { get; set; }
        public string? Landmark { get; set; }

        public string? State { get; set; }

        public string? LGA { get; set; }

        public string? TownCity { get; set; }

        public Int64 PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Website { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? CompanyOwnsBusinessPremises { get; set; }
        public IBrowserFile? ProofOfCompanyAddress { get; set; }
        public string? ProofOfCompanyAddressName { get; set; }
        public bool HaveCoyPrem { get; set; }

        //Regulatory Info
        //[Required(ErrorMessage = "RC Number is required")]
        public string? CompanyRegistrationNumber { get; set; }
        //[Required(ErrorMessage = "Date of Incorporation is required")]
        public DateTime DateOfIncorporationRegistration { get; set; }
        //[Required(ErrorMessage = "Date Business Started is required")]
        public DateTime DateBusinessStarted { get; set; }
        //[Required(ErrorMessage = "TIN is required")]
        public string? TIN { get; set; }
        public string? SCUMLNumber { get; set; }
        //[Required(ErrorMessage = "Certificate of Incorporation is required")]
        public IBrowserFile? CertificateOfIncorporationRegistration { get; set; }
        //[Required(ErrorMessage = "Form CO2 is required")]
        public IBrowserFile? FormCO2AllotmentOfShares { get; set; }
        //[Required(ErrorMessage = "Form CO7 is required")]
        public IBrowserFile? FormCO7ParticularsOfDirectors { get; set; }
        public IBrowserFile? Memart { get; set; }
        public IBrowserFile? BoardResolution { get; set; }

        //filename
        public string? CertificateOfIncorporationName { get; set; }
        public string? FormCO2AllotmentOfSharesName { get; set; }
        public string? FormCO7ParticularsOfDirectorsName { get; set; }
        public string? MemartName { get; set; }
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
        public DateTime JoinedDate { get; set; }
        public string? PresentThreatenedLitigationWithThirdParty { get; set; }
        public string? ThirdPartyName { get; set; }
        public string? SuitNumber { get; set; }
        public string? IsYourCompanyQuotedOnAnyStockExchange { get; set; }
        public string? IndicateStockSymbol { get; set; }
        public string? StockExchange { get; set; }
        public bool companyQuoted { get; set; }
        public bool litigation { get; set; }
        public bool membership { get; set; }
        public string ShareholderType { get; set; }
        //Ind details
        public List<AOOwnershipInformationIndividual>? Inds { get; set; }
        // Corp details
        public List<AOOwnershipInformationCooperate>? Corps { get; set; }

        //Sole Proprietorship
        public string? SP_Title { get; set; }
        public string? SP_FirstName { get; set; }
        public string? SP_Surname { get; set; }
        public string? SP_OtherNames { get; set; }
        public string? SP_MaidenName { get; set; }
        public DateTime SP_DateOfBirth { get; set; }
        public string? SP_PlaceOfBirth { get; set; }
        public string? SP_Gender { get; set; }
        public string? SP_MaritalStatus { get; set; }
        public string? SP_PhoneNumber { get; set; }
        public string? SP_EmailAddress { get; set; }
        public string? SP_HomeAddress { get; set; }
        public IBrowserFile? SP_ProofOfAddress { get; set; }
        public string? SP_ProofOfAddressName { get; set; }
        public string? SP_State { get; set; }
        public string? SP_LGA { get; set; }
        public string? SP_TownCity { get; set; }
        public string? SP_HighestEducationalQualification { get; set; }
        public string? SP_BVN { get; set; }
        public string? SP_NIN { get; set; }
        public string? SP_TIN { get; set; }
        public string? SP_PercentageOwnership { get; set; }
        public string? SP_Nationality { get; set; }
        public IBrowserFile? SP_CERPAC { get; set; }
        public string? SP_CERPACName { get; set; }
        public DateTime SP_CERPACIssueDate { get; set; }
        public DateTime SP_CERPACExpiryDate { get; set; }
        public string? SP_IdentificationType { get; set; }
        public string? SP_IdentificationNumber { get; set; }
        public IBrowserFile? SP_IdentificationFile { get; set; }
        public string? SP_IdentificationFileName { get; set; }
        public DateTime SP_IdentificationIssueDate { get; set; }
        public DateTime SP_IdentificationExpiryDate { get; set; }
        public IBrowserFile? SP_PassportPhotograph { get; set; }
        public string? SP_PassportPhotographName { get; set; }
        public string? SP_Occupation { get; set; }
        public string? SP_JobTitle { get; set; }
        public IBrowserFile? SP_UploadCV { get; set; }
        public string? SP_UploadCVName { get; set; }
        public string? SP_AnyPoliticalAffilationWithAPoliticalOfficeHolder { get; set; }
        public string? SP_IndicatePoliticalOfficerRelationship { get; set; }
        public string? SP_ContestedPoliticalOffice { get; set; }
        public string? SP_IndicatePoliticalOffice { get; set; }
        public string? HowDoYouKnowAboutBOI { get; set; }
        public string? AnyRelationshipWithAnyBOIEmployeeOrAnyOfItsDirectors { get; set; }
        public string? NameOfEmployeeDirector { get; set; }
        public string? Relationship { get; set; }
        public bool RelationshipExists { get; set; }

        public List<AODetailsOfNextOfKin>? kins { get; set; }
        public List<AOAccountDetailsOfOwner> accts { get; set; }
    }

    //public class IndividualOwnership
    //{
    //    //indiv detail
    //    public string Title { get; set; }
    //    public string FirstName { get; set; }
    //    public string Surname { get; set; }
    //    public string OtherNames { get; set; }
    //    public string MaidenName { get; set; }
    //    public DateTime DateOfBirth { get; set; }
    //    public string PlaceOfBirth { get; set; }
    //    public string Gender { get; set; }
    //    public string MaritalStatus { get; set; }
    //    public string Ind_PhoneNumber { get; set; } //
    //    public string Ind_EmailAddress { get; set; } //
    //    public string HomeAddress { get; set; }
    //    public string Position { get; set; }
    //    public string ProofOfAddress { get; set; }
    //    public string Ind_State { get; set; } //
    //    public string Ind_LGA { get; set; } //
    //    public string Ind_TownCity { get; set; } //
    //    public string HighestEducationalQualification { get; set; }
    //    public string BVN { get; set; }
    //    public string NIN { get; set; }
    //    public string Ind_TIN { get; set; } //
    //    public string Ind_PercentageOwnership { get; set; }
    //    public string Nationality { get; set; }
    //    public string CERPAC { get; set; }
    //    public DateTime CERPACIssueDate { get; set; }
    //    public DateTime CERPACExpiryDate { get; set; }
    //    public string IdentificationType { get; set; }
    //    public string IdentificationNumber { get; set; }
    //    public string IdentificationFile { get; set; }
    //    public DateTime IdentificationIssueDate { get; set; }
    //    public DateTime IdentificationExpiryDate { get; set; }
    //    public string PassportPhotograph { get; set; }
    //    public string Occupation { get; set; }
    //    public string JobTitle { get; set; }
    //    public string UploadCV { get; set; }
    //    public string AnyPoliticalAffilationWithAPoliticalOfficeHolder { get; set; }
    //    public string StatePoliticalOfficerRelationship { get; set; }
    //    public string ContestedPoliticalOffice { get; set; }
    //    public string StatePoliticalOffice { get; set; }
    //}

    //public class CorporateOwnership
    //{
    //    // Corp details
    //    public string NameOfShareholdingCompany { get; set; }
    //    public string RegistrationNumber { get; set; }
    //    public string Corp_PercentageOwnership { get; set; }
    //    public string Corp_State { get; set; }
    //    public string Corp_LGA { get; set; }
    //    public string Corp_TownCity { get; set; }
    //    public string Corp_PhoneNumber { get; set; }
    //    public string Corp_EmailAddress { get; set; }
    //    public string Corp_Website { get; set; }
    //    public string Corp_Twitter { get; set; }
    //    public string Corp_Facebook { get; set; }
    //    public string Corp_Instagram { get; set; }
    //    public string Corp_CompanyAddress { get; set; }
    //    public string ContactPersonName { get; set; }
    //}

    

}
