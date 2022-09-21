using Blazored.Modal;
using Blazored.Modal.Services;
using BOILoanPortal.Models;
using BOILoanPortal.Services;
using BOILoanPortal.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;
using System.Globalization;

namespace BOILoanPortal.Pages
{
    public partial class SoleProprietorship
    {
        [Parameter]
        public CustomerInfo? info { get; set; } = new();

        [Parameter] public AOSoleProprietorship solProInfo { get; set; }

        [Parameter]
        public AOAccountDetailsOfOwner? acct { get; set; } = new();

        public List<AOAccountDetailsOfOwner>? _accts { get; set; } = new();

        [Parameter]
        public AODetailsOfNextOfKin? kin { get; set; } = new();

        public List<AODetailsOfNextOfKin>? _kins { get; set; } = new();

        [Inject]
        public NavigationManager? _NavigationManager { get; set; }

        [Inject]
        public IAccountOpeningService? _Acct { get; set; }

        [Inject]
        public IJSRuntime? _JsRuntime { get; set; }

        [Inject]
        public IUtilityService? _util { get; set; }

        [Inject]
        private IMemoryCache? _memoryCache { get; set; }
        public Country country { get; set; } = new();
        public List<Resp> States { get; set; } = new();
        public List<Resp>? Lgas { get; set; } = new();
        public List<Resp>? cities { get; set; } = new();

        public List<Resp> education { get; set; } = new();
        public List<Resp> mStatus { get; set; } = new();
        public List<Resp> idType { get; set; } = new();
        public List<Resp> title { get; set; } = new();
        public List<Resp> banks { get; set; } = new();

        public bool nextofkin { get; set; }
        public bool acctDetails { get; set; }

        public bool hideCERPAC { get; set; }
        public bool politicalAffilate { get; set; }
        public bool contestedPoliticalOffice { get; set; }
        [Parameter] public string? id { get; set; }

        [CascadingParameter] public IModalService modal { get; set; } = default!;
        dynamic newKinDetails = null;
        dynamic newacctDetails = null;

        string _message = "";
        protected override async Task OnInitializedAsync()
        {

            if (!string.IsNullOrEmpty(id))
            {
                info = _memoryCache.Get<CustomerInfo>($"info_{id}");

                solProInfo = _memoryCache.Get<AOSoleProprietorship>($"solepro-{id}");
                if (solProInfo == null)
                    solProInfo = new AOSoleProprietorship();
                else
                {
                    solProInfo.State = Convert.ToString(States.Single(s => s?.id == Convert.ToInt32(solProInfo.State))?.id);
                    solProInfo.LGA = Convert.ToString(Lgas.Single(l => l?.id == Convert.ToInt32(solProInfo.LGA))?.id);
                }
            }

            //solProInfo = _memoryCache.Get<CustomerInfo>("info");

            States = await _util.GetStates();
            country = await _util.Countries();
            title = await _util.GetTitle();
            mStatus = await _util.GetMaritalStatus();
            banks = await _util.GetBanks();
            education = await _util.GetEducationalQualification();
            idType = await _util.GetIdType();

            //solProInfo.DateOfBirth = DateTime.UtcNow;
            //solProInfo.IdentificationIssueDate = DateTime.UtcNow;
            //solProInfo.CERPACIssueDate = DateTime.UtcNow;
            //solProInfo.CERPACExpiryDate = DateTime.UtcNow;

            await base.OnInitializedAsync();
        }

        public async Task ProofOfAddress(InputFileChangeEventArgs e)
        {
            var file = e.File;
            solProInfo.ProofOfAddress = file;
            solProInfo.ProofOfAddressName = file.Name;
        }

        public async Task PassportPhotograph(InputFileChangeEventArgs e)
        {
            var file = e.File;
            solProInfo.PassportPhotograph = file;
            solProInfo.PassportPhotographName = file.Name;
        }

        public async Task UploadCV(InputFileChangeEventArgs e)
        {
            var file = e.File;
            solProInfo.UploadCV = file;
            solProInfo.UploadCVName = file.Name;
        }

        public async Task IdentificationFile(InputFileChangeEventArgs e)
        {
            var file = e.File;
            solProInfo.IdentificationFile = file;
            solProInfo.IdentificationFileName = file.Name;
        }

        public async Task GetCERPAC(InputFileChangeEventArgs e)
        {
            var file = e.File;
            solProInfo.CERPAC = file;
            solProInfo.CERPACName = file.Name;
        }


        public async Task GetLga(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                Lgas = await _util.GetLGA(Value);
            }
            solProInfo.State = Value;
        }

        public async Task GetCity(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                cities = await _util.GetCity(Value);
            }
            solProInfo.LGA = Value;
        }

        public async Task OnNationalityChange(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                if(!Value.ToLower().Equals("nigeria"))
                    hideCERPAC = true;
                else
                    hideCERPAC = false;
            }
            solProInfo.Nationality = Value;
        }

        public async Task AddNextofkin()
        {
            nextofkin = true;
            StateHasChanged();
        }

        public async Task AddAccountDetails()
        {
            acctDetails = true;
            StateHasChanged();
        }

        public async Task AnyPoliticalAffilationWithAPoliticalOfficeHolder(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                if (Value.ToLower().Equals("yes"))
                    politicalAffilate = true;
                else
                    politicalAffilate = false;
            }
            solProInfo.AnyPoliticalAffilationWithAPoliticalOfficeHolder = Value;
        }

        public async Task ContestedPoliticalOffice(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                if (Value.ToLower().Equals("yes"))
                    contestedPoliticalOffice = true;
                else
                    contestedPoliticalOffice = false;
            }
            solProInfo.ContestedPoliticalOffice = Value;
        }

        public async Task SaveSoleProprietorshipInfo()
        {
            _NavigationManager?.NavigateTo($"/regulatoryInfo/{id}");
        }

        public async Task SubmitSoleProprietorshipInfoForm()
        {
            solProInfo.UserId = info.UserId;
            solProInfo.UserEmail = info.UserEmail;
            solProInfo.RefNumber = info.RefNumber;

            solProInfo.CERPACIssueDate = solProInfo.CERPACIssueDate ?? DateTime.MinValue.ToString("yyyy-MM-dd");
            solProInfo.CERPACExpiryDate = solProInfo.CERPACExpiryDate ?? DateTime.MinValue.ToString("yyyy-MM-dd");

            solProInfo.State = States.Single(s => s?.id == Convert.ToInt32(solProInfo.State))?.name;
            solProInfo.LGA = Lgas.Single(l => l?.id == Convert.ToInt32(solProInfo.LGA))?.name;

            _memoryCache.Set($"solepro-{id}", solProInfo, TimeSpan.FromMinutes(30));

            SetParameters();


            //solProInfo.DateOfBirth = DateTime.ParseExact(Convert.ToString(solProInfo.DateOfBirth), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //solProInfo.IdentificationIssueDate = DateTime.ParseExact(Convert.ToString(solProInfo.IdentificationIssueDate), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //solProInfo.IdentificationExpiryDate = DateTime.ParseExact(Convert.ToString(solProInfo.IdentificationExpiryDate), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //solProInfo.CERPACIssueDate = DateTime.ParseExact(Convert.ToString(solProInfo.CERPACIssueDate), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //solProInfo.CERPACExpiryDate = DateTime.ParseExact(Convert.ToString(solProInfo.CERPACExpiryDate), "yyyy-MM-dd", CultureInfo.InvariantCulture);


            var resAccts = await _Acct.InsertAccountInfo(solProInfo.accts);
            var resKins = await _Acct.InsertNextOfKinInfo(solProInfo.kins);

            var res = await _Acct.InsertSoleProprietorshipInfo(solProInfo);

            if (res.success)
            {

                _NavigationManager?.NavigateTo($"/relatedparty/{id}");
            }
            else
            {
                _message = res.message;
                var parameters = new ModalParameters
                    {
                        { nameof(DisplayMessage.Message), _message }
                    };

                modal.Show<DisplayMessage>("Alert", parameters);
                _message = "";
                return;
            }

            
        }

        async Task ShowAddNextOfKintModal()
        {
            var options = new ModalOptions { 
                //ContentScrollable = true, 
                DisableBackgroundCancel = true,
                //UseCustomLayout = true,
            };
            var messageForm = modal.Show<NextOfKin>("", options);
            var result = await messageForm.Result;

            if (!result.Cancelled)
                newKinDetails = result?.Data;
            kin = newKinDetails;
            kin.UserId = info.UserId;
            kin.UserEmail = info.UserEmail;
            kin.RefNumber = info.RefNumber;
            _kins.Add(kin);
            solProInfo.kins = _kins;
        }

        //public async Task SubmitNextOfKinDetails()
        //{
        //    //_kins = new List<AODetailsOfNextOfKin>();
        //    _kins.Add(kin);
        //    solProInfo.kins = _kins;
        //    nextofkin = false;

        //    kin = new AODetailsOfNextOfKin();

        //    StateHasChanged();
        //}

        async Task ShowAddAccountModal()
        {
            var options = new ModalOptions
            {
                //ContentScrollable = true,
                DisableBackgroundCancel = true,
                //UseCustomLayout = true,
            };
            var messageForm = modal.Show<AccountDetails>("", options);
            var result = await messageForm.Result;

            if (!result.Cancelled)
                newacctDetails = result?.Data;
            acct = newacctDetails;
            acct.UserId = info.UserId;
            acct.UserEmail = info.UserEmail;
            acct.RefNumber = info.RefNumber;
            _accts.Add(acct);
            solProInfo.accts = _accts;
        }

        public void GetBirthDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                solProInfo.DateOfBirth = (String)e.Value;
            }
        }

        public void GetCERPACIssueDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                solProInfo.CERPACIssueDate = (String)e.Value;
            }
        }

        public void GetCERPACExpiryDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                solProInfo.CERPACExpiryDate = (String)e.Value;
            }
        }

        public void GetIdIssueDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                solProInfo.IdentificationIssueDate = (String)e.Value;
            }
        }

        public void GetIdExpiryDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                solProInfo.IdentificationExpiryDate = (String)e.Value;
            }
        }


        private async Task SetParameters()
        {
            info.SP_Title = solProInfo.Title;
            info.SP_FirstName  = solProInfo.FirstName;
            info.SP_Surname = solProInfo.Surname;
            info.SP_OtherNames = solProInfo.OtherNames;
            info.SP_MaidenName = solProInfo.MaidenName;
            info.SP_DateOfBirth = Convert.ToDateTime(solProInfo.DateOfBirth);
            info.SP_PlaceOfBirth = solProInfo.PlaceOfBirth;
            info.SP_Gender = solProInfo.Gender;
            info.SP_MaritalStatus = solProInfo.MaritalStatus;
            info.SP_PhoneNumber = solProInfo.PhoneNumber;
            info.SP_EmailAddress = solProInfo.EmailAddress;
            info.SP_HomeAddress = solProInfo.HomeAddress;
            info.SP_ProofOfAddress = solProInfo.ProofOfAddress;
            info.SP_ProofOfAddressName = solProInfo.ProofOfAddressName;
            info.SP_State = solProInfo.State;
            info.SP_LGA = solProInfo.LGA;
            info.SP_TownCity = solProInfo.TownCity;
            info.SP_HighestEducationalQualification = solProInfo.HighestEducationalQualification; 
            info.SP_BVN = solProInfo.BVN;
            info.SP_NIN = solProInfo.NIN;
            info.SP_TIN = solProInfo.TIN;
            info.SP_PercentageOwnership = solProInfo.PercentageOwnership;
            info.SP_Nationality = solProInfo.Nationality;
            info.SP_CERPAC = solProInfo.CERPAC;
            info.SP_CERPACName = solProInfo.CERPACName;
            info.SP_CERPACIssueDate = Convert.ToDateTime(solProInfo.CERPACIssueDate);
            info.SP_CERPACExpiryDate = Convert.ToDateTime(solProInfo.CERPACExpiryDate);
            info.SP_IdentificationType = solProInfo.IdentificationType;
            info.SP_IdentificationNumber = solProInfo.IdentificationNumber;
            info.SP_IdentificationFile = solProInfo.IdentificationFile;
            info.SP_IdentificationFileName = solProInfo.IdentificationFileName;
            info.SP_IdentificationIssueDate = Convert.ToDateTime(solProInfo.IdentificationIssueDate);
            info.SP_IdentificationExpiryDate = Convert.ToDateTime(solProInfo.IdentificationExpiryDate);
            info.SP_PassportPhotograph = solProInfo.PassportPhotograph;
            info.SP_PassportPhotographName = solProInfo.PassportPhotographName;
            info.SP_Occupation = solProInfo.Occupation;
            info.SP_JobTitle = solProInfo.JobTitle;
            info.SP_UploadCV = solProInfo.UploadCV;
            info.SP_UploadCVName = solProInfo.UploadCVName;
            info.SP_AnyPoliticalAffilationWithAPoliticalOfficeHolder = solProInfo.AnyPoliticalAffilationWithAPoliticalOfficeHolder; 
            info.SP_IndicatePoliticalOfficerRelationship = solProInfo.IndicatePoliticalOfficerRelationship;
            info.SP_ContestedPoliticalOffice = solProInfo.ContestedPoliticalOffice;
            info.SP_IndicatePoliticalOffice = solProInfo.IndicatePoliticalOffice;
            info.accts = solProInfo.accts;
            info.kins = solProInfo.kins;
            //foreach (var acct in _accts)
            //{
            //    info.accts.Add(acct);
            //}

            //foreach (var kin in _kins)
            //{
            //    info.kins.Add(kin);
            //}

            _memoryCache.Set($"info_{id}", info, TimeSpan.FromMinutes(30));

            //string serValue = JsonConvert.SerializeObject(info);
            //string value = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(serValue));
            //return value;
            //_NavigationManager?.NavigateTo($"/regulatoryInfo");
        }
    }
}
