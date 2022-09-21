namespace BOILoanPortal.Models
{

    public class BVNResponse
    {
        public int bvnId { get; set; }
        public string? id { get; set; }
        public string? idNumber { get; set; }
        public DateTime requestedDate { get; set; }
        public string? status { get; set; }
        public bool dataValidation { get; set; }
        public bool selfieValidation { get; set; }
        public string? firstName { get; set; }
        public object? middleName { get; set; }
        public string? lastName { get; set; }
        public string? image { get; set; }
        public string? enrollmentBranch { get; set; }
        public string? enrollmentInstitution { get; set; }
        public string? mobile { get; set; }
        public DateTime dateOfBirth { get; set; }
        public bool isConsent { get; set; }
        public bool shouldRetrivedNin { get; set; }
        public string? businessId { get; set; }
        public string? type { get; set; }
        public string? gender { get; set; }
        public DateTime requestedAt { get; set; }
        public string? country { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime lastModifiedAt { get; set; }
    }

    public class NINResponse
    {
        public int ninId { get; set; }
        public string? id { get; set; }
        public string? idNumber { get; set; }
        public DateTime requestedDate { get; set; }
        public string? status { get; set; }
        public bool dataValidation { get; set; }
        public bool selfieValidation { get; set; }
        public string? firstName { get; set; }
        public string? middleName { get; set; }
        public string? lastName { get; set; }
        public string? image { get; set; }
        public object? signature { get; set; }
        public string? mobile { get; set; }
        public object? email { get; set; }
        public string? birthState { get; set; }
        public string? nokState { get; set; }
        public string? religion { get; set; }
        public string? birthLGA { get; set; }
        public string? birthCountry { get; set; }
        public DateTime dateOfBirth { get; set; }
        public bool isConsent { get; set; }
        public string? businessId { get; set; }
        public string? type { get; set; }
        public string? gender { get; set; }
        public DateTime requestedAt { get; set; }
        public string? country { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime lastModifiedAt { get; set; }
        public string? town { get; set; }
        public string? lga { get; set; }
        public string? state { get; set; }
        public string? addressLine { get; set; }
    }

    public class PVCResponse
    {
        public int pvcId { get; set; }
        public string? id { get; set; }
        public string? idNumber { get; set; }
        public DateTime requestedDate { get; set; }
        public string? status { get; set; }
        public bool dataValidation { get; set; }
        public bool selfieValidation { get; set; }
        public string? firstName { get; set; }
        public string? middleName { get; set; }
        public string? lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public bool isConsent { get; set; }
        public string? businessId { get; set; }
        public string? type { get; set; }
        public DateTime requestedAt { get; set; }
        public string? country { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime lastModifiedAt { get; set; }
    }

    public class IntlPstResponse
    {
        public int inpId { get; set; }
        public string? id { get; set; }
        public string? idNumber { get; set; }
        public DateTime requestedDate { get; set; }
        public string? status { get; set; }
        public bool dataValidation { get; set; }
        public bool selfieValidation { get; set; }
        public string? firstName { get; set; }
        public string? middleName { get; set; }
        public string? lastName { get; set; }
        public DateTime issuedDate { get; set; }
        public DateTime expiredDate { get; set; }
        public string? stateOfIssuance { get; set; }
        public bool notifyWhenIdExpire { get; set; }
        public string? image { get; set; }
        public string? signature { get; set; }
        public string? issuedAt { get; set; }
        public string? mobile { get; set; }
        public DateTime dateOfBirth { get; set; }
        public bool isConsent { get; set; }
        public string? businessId { get; set; }
        public string? type { get; set; }
        public string? gender { get; set; }
        public DateTime requestedAt { get; set; }
        public string? country { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime lastModifiedAt { get; set; }
        public string? reason { get; set; }
    }

    public class DriversLicenceResponse
    {
        public int ninId { get; set; }
        public string? id { get; set; }
        public string? idNumber { get; set; }
        public DateTime requestedDate { get; set; }
        public string? status { get; set; }
        public bool dataValidation { get; set; }
        public bool selfieValidation { get; set; }
        public string? firstName { get; set; }
        public string? middleName { get; set; }
        public string? lastName { get; set; }
        public string? image { get; set; }
        public string? signature { get; set; }
        public string? mobile { get; set; }
        public string? email { get; set; }
        public string? birthState { get; set; }
        public string? nokState { get; set; }
        public string? religion { get; set; }
        public string? birthLGA { get; set; }
        public string? birthCountry { get; set; }
        public DateTime dateOfBirth { get; set; }
        public bool isConsent { get; set; }
        public string? businessId { get; set; }
        public string? type { get; set; }
        public string? gender { get; set; }
        public DateTime requestedAt { get; set; }
        public string? country { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime lastModifiedAt { get; set; }
        public string? town { get; set; }
        public string? lga { get; set; }
        public string? state { get; set; }
        public string? addressLine { get; set; }
    }

    public class CACResponse
    {
        public string? id { get; set; }
        public string? status { get; set; }
        public string? businessId { get; set; }
        public string? parentId { get; set; }
        public bool isConsent { get; set; }
        public string? type { get; set; }
        public string? searchTerm { get; set; }
        public string? name { get; set; }
        public string? registrationNumber { get; set; }
        public string? tin { get; set; }
        public DateTime requestedDate { get; set; }
        public string? jtbTin { get; set; }
        public string? taxOffice { get; set; }
        public string? email { get; set; }
        public string? companyStatus { get; set; }
        public string? phone { get; set; }
        public DateTime requestedAt { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime lastModifiedAt { get; set; }
        public string? typeOfEntity { get; set; }
        public string? activity { get; set; }
        public DateTime registrationDate { get; set; }
        public string? address { get; set; }
        public string? state { get; set; }
        public string? lga { get; set; }
        public string? city { get; set; }
        public string? websiteEmail { get; set; }
        public List<Keypersonnel>? keyPersonnel { get; set; }
        public string? branchAddress { get; set; }
        public string? headOfficeAddress { get; set; }
        public string? objectives { get; set; }
    }

    public class Keypersonnel
    {
        public int id { get; set; }
        public DateTime requestedDate { get; set; }
        public string? type { get; set; }
        public string? typeNumber { get; set; }
        public string? companyName { get; set; }
        public string? name { get; set; }
        public string? designation { get; set; }
    }

    //public class Rootobject
    //{
    //    public string id { get; set; }
    //    public string status { get; set; }
    //    public string businessId { get; set; }
    //    public string parentId { get; set; }
    //    public bool isConsent { get; set; }
    //    public string type { get; set; }
    //    public string searchTerm { get; set; }
    //    public string name { get; set; }
    //    public string registrationNumber { get; set; }
    //    public string tin { get; set; }
    //    public DateTime requestedDate { get; set; }
    //    public string jtbTin { get; set; }
    //    public string taxOffice { get; set; }
    //    public string email { get; set; }
    //    public string companyStatus { get; set; }
    //    public string phone { get; set; }
    //    public DateTime requestedAt { get; set; }
    //    public DateTime createdAt { get; set; }
    //    public DateTime lastModifiedAt { get; set; }
    //    public string typeOfEntity { get; set; }
    //    public string activity { get; set; }
    //    public DateTime registrationDate { get; set; }
    //    public string address { get; set; }
    //    public string state { get; set; }
    //    public string lga { get; set; }
    //    public string city { get; set; }
    //    public string websiteEmail { get; set; }
    //    public Keypersonnel[] keyPersonnel { get; set; }
    //    public string branchAddress { get; set; }
    //    public string headOfficeAddress { get; set; }
    //    public string objectives { get; set; }
    //}

    //public class Keypersonnel
    //{
    //    public int id { get; set; }
    //    public DateTime requestedDate { get; set; }
    //    public string type { get; set; }
    //    public string typeNumber { get; set; }
    //    public string companyName { get; set; }
    //    public string name { get; set; }
    //    public string designation { get; set; }
    //}

}
