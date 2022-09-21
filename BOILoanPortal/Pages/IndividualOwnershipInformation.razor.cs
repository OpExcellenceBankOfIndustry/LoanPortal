using Blazored.Modal;
using Blazored.Modal.Services;
using BOILoanPortal.Models;
using BOILoanPortal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace BOILoanPortal.Pages
{
    public partial class IndividualOwnershipInformation
    {
        [Parameter]
        public AOOwnershipInformationIndividual ioInfo { get; set; } = new();

        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        [Parameter]
        public CustomerInfo info { get; set; } = new();

        [Inject]
        public IUtilityService _util { get; set; }

        protected bool IsSelected { get; set; }
        protected bool AnyPoliticalAffiliate { get; set; }

        protected bool HoldPoliticalOffice { get; set; }
        [Parameter] public EventCallback<bool> OnShareholderTypeSelection { get; set; }

        public List<Resp> title { get; set; } = new();
        public List<Resp> States { get; set; } = new();
        public List<Resp>? Lgas { get; set; } = new();
        public List<Resp>? cities { get; set; } = new();

        public Country country { get; set; } = new();

        public List<Resp> education { get; set; } = new();
        public List<Resp> mStatus { get; set; } = new();
        public List<Resp> idType { get; set; } = new();

        public bool hideCERPACInd { get; set; } = true;

        [Inject]
        private IMemoryCache? _memoryCache { get; set; }

        protected override async Task OnInitializedAsync()
        {
            title = await _util.GetTitle();
            States = await _util.GetStates();
            education = await _util.GetEducationalQualification();
            country = await _util.Countries();
            idType = await _util.GetIdType();
            mStatus = await _util.GetMaritalStatus();

            //info = _memoryCache.Get<CustomerInfo>($"info");

            //ioInfo.DateOfBirth = DateTime.UtcNow;
            //ioInfo.IdentificationIssueDate = DateTime.UtcNow;
            //ioInfo.IdentificationIssueDate = DateTime.UtcNow;
            //ioInfo.CERPACIssueDate = DateTime.UtcNow;
            //ioInfo.CERPACExpiryDate = DateTime.UtcNow;
        }

        //UploadCV, PassportPhotograph, ProofOfAddress

        public async Task ProofOfAddress(InputFileChangeEventArgs e)
        {
            var file = e.File;
            ioInfo.ProofOfAddress = file;
            ioInfo.ProofOfAddressName = file.Name;
        }

        public async Task PassportPhotograph(InputFileChangeEventArgs e)
        {
            var file = e.File;
            ioInfo.PassportPhotograph = file;
            ioInfo.PassportPhotographName = file.Name;
        }

        public async Task UploadCV(InputFileChangeEventArgs e)
        {
            var file = e.File;
            ioInfo.UploadCV = file;
            ioInfo.UploadCVName = file.Name;
        }

        public async Task IdentificationFile(InputFileChangeEventArgs e)
        {
            var file = e.File;
            ioInfo.IdentificationFile = file;
            ioInfo.IdentificationFileName = file.Name;
        }

        public async Task GetCERPAC(InputFileChangeEventArgs e)
        {
            var file = e.File;
            ioInfo.CERPAC = file;
            ioInfo.CERPACName = file.Name;
        }

        public async Task GetLga(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                Lgas = await _util.GetLGA(Value);
            }
            ioInfo.State = Value;
        }

        public async Task GetCity(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                cities = await _util.GetCity(Value);
            }
            ioInfo.LGA = Value;
        }

        public async Task OnNationalityChange(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                if (!Value.ToLower().Equals("nigeria"))
                    hideCERPACInd = true;
                else
                    hideCERPACInd = false;
            }
            ioInfo.Nationality = Value;
        }

        protected async Task CheckBoxChanged(ChangeEventArgs e)
        {
            IsSelected = (bool)e.Value;
            await OnShareholderTypeSelection.InvokeAsync(IsSelected);
        }

        public void AnyPoliticalAffiliation(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                ioInfo.AnyPoliticalAffilationWithAPoliticalOfficeHolder = e.Value.ToString();
                if (ioInfo.AnyPoliticalAffilationWithAPoliticalOfficeHolder.Trim().ToUpper() == "YES")
                {
                    AnyPoliticalAffiliate = true;
                    StateHasChanged();
                }
                else
                {
                    AnyPoliticalAffiliate = false;
                    StateHasChanged();
                }
            }
        }

        public void HoldAnyPoliticalOffice(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                ioInfo.AnyPoliticalAffilationWithAPoliticalOfficeHolder = e.Value.ToString();
                if (ioInfo.AnyPoliticalAffilationWithAPoliticalOfficeHolder.Trim().ToUpper() == "YES")
                {
                    HoldPoliticalOffice = true;
                    StateHasChanged();
                }
                else
                {
                    HoldPoliticalOffice = false;
                    StateHasChanged();
                }
            }
        }

        public async Task SubmitIndividualOwnershipInformation()
        {
            //ioInfo.UserId = info.UserId;
            //ioInfo.UserEmail = info.UserEmail;  
            //ioInfo.RefNumber = info.RefNumber;
            ioInfo.CERPACIssueDate = ioInfo.CERPACIssueDate ?? DateTime.MinValue.ToString("yyyy-MM-dd");
            ioInfo.CERPACExpiryDate = ioInfo.CERPACExpiryDate ?? DateTime.MinValue.ToString("yyyy-MM-dd");

            //ioInfo.DateOfBirth = DateTime.ParseExact(Convert.ToString(ioInfo.DateOfBirth), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //ioInfo.IdentificationIssueDate = DateTime.ParseExact(Convert.ToString(ioInfo.IdentificationIssueDate), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //ioInfo.IdentificationExpiryDate = DateTime.ParseExact(Convert.ToString(ioInfo.IdentificationExpiryDate), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //ioInfo.CERPACIssueDate = DateTime.ParseExact(Convert.ToString(ioInfo.CERPACIssueDate), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //ioInfo.CERPACExpiryDate = DateTime.ParseExact(Convert.ToString(ioInfo.CERPACExpiryDate), "yyyy-MM-dd", CultureInfo.InvariantCulture);

            await BlazoredModal.CloseAsync(ModalResult.Ok(ioInfo));
            //Inds.Add(ioInfo);
            //ioInfo = new AOOwnershipInformationIndividual();
        }

        public void GetBirthDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                ioInfo.DateOfBirth = (String)e.Value;
            }
        }

        public void GetCERPACIssueDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                ioInfo.CERPACIssueDate = (String)e.Value;
            }
        }

        public void GetCERPACExpiryDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                ioInfo.CERPACExpiryDate = (String)e.Value;
            }
        }

        public void GetIdIssueDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                ioInfo.IdentificationIssueDate = (String)e.Value;
            }
        }

        public void GetIdExpiryDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                ioInfo.IdentificationExpiryDate = (String)e.Value;
            }
        }
    }
}
