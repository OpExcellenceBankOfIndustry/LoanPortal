//using Blazored.Modal.Services;
using BOILoanPortal.Models;
using BOILoanPortal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Text;

namespace BOILoanPortal.Pages
{
    public partial class CompanyInfo
    {
        //[Parameter]
        //public AOCompanyInformation coyInfo { get; set; } = new();

        [Parameter]
        public CustomerInfo info { get; set; } = new();

        [Parameter]
        public AOCompanyInformation coyInfo { get; set; } = new();

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAccountOpeningService? _Acct { get; set; }

        [Inject]
        public IJSRuntime? _JsRuntime { get; set; }

        [Inject]
        public IUtilityService? _util { get; set; }

        [Inject]
        public AppState? AppState { get; set; }

        [Inject]
        IValidationService? ValidateServ { get; set; }

        [Inject]
        private IMemoryCache? _memoryCache { get; set; }
        public Country country { get; set; } = new();
        public List<Resp> States { get; set; } = new();
        public List<Resp>? Lgas { get; set; } = new();
        public List<Resp>? cities { get; set; } = new();

        public List<Resp> coyType { get; set; } = new List<Resp>();

        public bool HaveCoyPrem = false;

        public AuthenticatedUser loginUser = new();
        [Parameter] public string? id { get; set; }
        //[Parameter] public string? userid { get; set; }

        protected override async Task OnInitializedAsync()
        {
            loginUser = _memoryCache.Get<AuthenticatedUser>($"login_{id}");

            //if (loginUser == null)
            //{
            //    NavigationManager.NavigateTo($"{NavigationManager.BaseUri}Register", true);
            //    //_NavigationManager?.NavigateTo($"/register");
            //    //string returnUrl = Url.Content("~/register");

            //    //return LocalRedirect(returnUrl);
            //}

            if (!string.IsNullOrEmpty(id))
            {
                coyInfo = _memoryCache.Get<AOCompanyInformation>($"coy-{id}");
                if (coyInfo == null)
                    coyInfo = new AOCompanyInformation();
                else
                {
                    coyInfo.State = Convert.ToString(States.Single(s => s?.id == Convert.ToInt32(coyInfo.State))?.id);
                    coyInfo.LGA = Convert.ToString(Lgas.Single(l => l?.id == Convert.ToInt32(coyInfo.LGA))?.id);
                }
            }

            States = await _util.GetStates();
            country = await _util.Countries();
            coyType = await _util.GetCompanyType();

            

        }

        public async Task GetLga(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                Lgas = await _util.GetLGA(Value);
            }
            coyInfo.State = Value;
        }

        public async Task GetCity(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                cities = await _util.GetCity(Value);
            }
            coyInfo.LGA = Value;
        }

        public void DoesCoyHavePrem(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                coyInfo.CompanyOwnsBusinessPremises = e.Value.ToString();
                if (coyInfo.CompanyOwnsBusinessPremises.Trim().ToUpper() == "YES")
                {
                    HaveCoyPrem = true;
                    StateHasChanged();
                }
                else
                {
                    HaveCoyPrem = false;
                    StateHasChanged();
                }
            }
        }

        public async Task AddressProofChange(InputFileChangeEventArgs e)
        {
            var file = e.File;
            coyInfo.ProofOfCompanyAddress = file;
            coyInfo.ProofOfCompanyAddressName = file.Name;
        }

        public async Task SaveCoyInfo()
        {
            coyInfo.State = States.Single(s => s?.id == Convert.ToInt32(coyInfo.State))?.name;
            coyInfo.LGA = Lgas.Single(l => l?.id == Convert.ToInt32(coyInfo.LGA))?.name;

            coyInfo.UserId = Convert.ToString(loginUser.userDetail.id);
            coyInfo.UserEmail = loginUser.userDetail.email;

            SetParameters();

            //_NavigationManager?.NavigateTo($"/regulatoryInfo");

            var res = await _Acct.InsertCompanyInfo(coyInfo);

            //coyInfo.CompanyType
        }

        public async Task SubmitCoyInfoForm()
        {
            loginUser = _memoryCache.Get<AuthenticatedUser>($"login");
            coyInfo.State = States.Single(s => s?.id == Convert.ToInt32(coyInfo.State))?.name;
            coyInfo.LGA = Lgas.Single(l => l?.id == Convert.ToInt32(coyInfo.LGA))?.name;

            coyInfo.UserId = Convert.ToString(loginUser.userDetail.id);
            coyInfo.UserEmail = loginUser.userDetail.email;

            SetParameters();

            var res = await _Acct.InsertCompanyInfo(coyInfo);

            _memoryCache.Set($"coytype_{id}", coyInfo.CompanyType, TimeSpan.FromMinutes(30));
            coyInfo.RefNumber = res.refNumber;
            info.RefNumber = res.refNumber;
            id = coyInfo.UserId;
            _memoryCache.Set($"coy-{id}", coyInfo, TimeSpan.FromMinutes(30));
            _memoryCache.Set($"info_{id}", info, TimeSpan.FromMinutes(30));


            NavigationManager?.NavigateTo($"/regulatoryInfo/{id}", true);
        }

        private void SetParameters()
        {
            info.UserId = coyInfo.UserId;
            info.UserEmail = string.IsNullOrEmpty(coyInfo.UserEmail) ? "" : coyInfo.UserEmail;
            info.RefNumber = string.IsNullOrEmpty(coyInfo.RefNumber) ? "" : coyInfo.RefNumber;
            info.CompanyName = string.IsNullOrEmpty(coyInfo.CompanyName) ? "" : coyInfo.CompanyName;
            info.CompanyShortName = string.IsNullOrEmpty(coyInfo.CompanyShortName) ? "" : coyInfo.CompanyShortName;
            info.CompanyType = string.IsNullOrEmpty(coyInfo.CompanyType) ? "" : coyInfo.CompanyType;
            info.CompanyAddress = string.IsNullOrEmpty(coyInfo.CompanyAddress) ? "" : coyInfo.CompanyAddress;
            info.Landmark = string.IsNullOrEmpty(coyInfo.Landmark) ? "" : coyInfo.Landmark;
            info.State = string.IsNullOrEmpty(coyInfo.State) ? "" : coyInfo.State;
            info.LGA = string.IsNullOrEmpty(coyInfo.LGA) ? "" : coyInfo.LGA;
            info.TownCity = string.IsNullOrEmpty(coyInfo.TownCity) ? "" : coyInfo.TownCity;
            info.PhoneNumber = coyInfo.PhoneNumber;
            info.EmailAddress = string.IsNullOrEmpty(coyInfo.EmailAddress) ? "" : coyInfo.EmailAddress;
            info.Website = string.IsNullOrEmpty(coyInfo.Website) ? "" : coyInfo.Website;
            info.Twitter = string.IsNullOrEmpty(coyInfo.Twitter) ? "" : coyInfo.Twitter;
            info.Facebook = string.IsNullOrEmpty(coyInfo.Facebook) ? "" : coyInfo.Facebook;
            info.Instagram = string.IsNullOrEmpty(coyInfo.Instagram) ? "" : coyInfo.Instagram;
            info.CompanyOwnsBusinessPremises = string.IsNullOrEmpty(coyInfo.CompanyOwnsBusinessPremises) ? "" : coyInfo.CompanyOwnsBusinessPremises;
            info.ProofOfCompanyAddress = coyInfo.ProofOfCompanyAddress ?? null;
            info.ProofOfCompanyAddressName = coyInfo.ProofOfCompanyAddressName;

            //_memoryCache.Set($"info", info, TimeSpan.FromMinutes(30));

            //string serValue = JsonConvert.SerializeObject(info);
            //string value = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(serValue));
            //return value;
            //_NavigationManager?.NavigateTo($"/regulatoryInfo");
        }

        //public void GetRegDate(ChangeEventArgs e)
        //{
        //    if (e.Value is not null)
        //    {
        //        coyInfo.testdate = Convert.ToDateTime(e.Value);
        //    }
        //}
    }
}
