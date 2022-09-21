namespace BOILoanPortal.Models
{
    public class AOOwnership
    {
        public string ShareholderType { get; set; }
        //Ind details
        public List<AOOwnershipInformationIndividual>? Inds { get; set; }
        // Corp details
        public List<AOOwnershipInformationCooperate>? Corps { get; set; }
    }
}
