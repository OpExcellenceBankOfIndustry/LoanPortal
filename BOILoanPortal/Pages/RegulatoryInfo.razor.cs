using Blazored.Modal;
using Blazored.Modal.Services;
using BOILoanPortal.Models;
using BOILoanPortal.Services;
using BOILoanPortal.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace BOILoanPortal.Pages
{
    public partial class RegulatoryInfo
    {
        //[Parameter]
        //public AORegulatoryInformation reqInfo { get; set; } = new AORegulatoryInformation();

        //[Parameter]
        //public CustomerInfo? reqInfo { get; set; } = new();

        [Parameter]
        public AORegulatoryInformation? regInfo { get; set; } = new();

        [Parameter]
        public CustomerInfo info { get; set; } = new();

        [Inject]
        public NavigationManager? _NavigationManager { get; set; }

        [Inject]
        public IAccountOpeningService? _Acct { get; set; }

        [Inject]
        public IJSRuntime? _JsRuntime { get; set; }

        [Inject]
        public IUtilityService? _util { get; set; }

        [Inject]
        IValidationService? ValidateServ { get; set; }

        [Inject]
        public AppState? AppState { get; set; }

        [Inject]
        private IMemoryCache? _memoryCache { get; set; }

        [Parameter] public string? id { get; set; }

        public List<Resp> turnover { get; set; } = new();

        public bool membership = false;
        public bool litigation = false;
        public bool companyQuoted = false;

        [CascadingParameter] public IModalService Modal { get; set; } = default!;

        string _message = "";

        protected override async Task OnInitializedAsync()
        {
            //var dd = AppState?.SelectedCoyInfo;

            //AppState.OnChange += StateHasChanged;

            //if (!string.IsNullOrEmpty(value))
            //{
            //    value = Encoding.UTF8.GetString(Convert.FromBase64String(value));
            //    coyInfo = JsonConvert.DeserializeObject<AOCompanyInformation>(value);
            //}

            

            if (!string.IsNullOrEmpty(id))
            {
                info = _memoryCache.Get<CustomerInfo>($"info_{id}");

                regInfo = _memoryCache.Get<AORegulatoryInformation>($"reg-{id}");
                if (regInfo == null)
                    regInfo = new AORegulatoryInformation();
                else
                {
                    membership = info.membership;
                    litigation = info.litigation;
                    companyQuoted = info.companyQuoted;
                }
            }



            




            //if (info != null)
            //{
            //    _memoryCache.Set("info", info, TimeSpan.FromMinutes(30));
            //}

            //regInfo.DateBusinessStarted = DateTime.UtcNow;
            //regInfo.JoinedDate = DateTime.UtcNow;
            //regInfo.DateOfIncorporationRegistration = DateTime.UtcNow;
            turnover = await _util.GetAnnualTurnover();

            //country = await _util.Countries();
            //coyType = await _util.GetCompanyType();
        }

        public async Task CertificateOfIncorporationRegistration(InputFileChangeEventArgs e)
        {
            var file = e.File;
            regInfo.CertificateOfIncorporationRegistration = file;
            regInfo.CertificateOfIncorporationRegistrationName = file.Name;
        }

        public async Task FormCO2AllotmentOfShares(InputFileChangeEventArgs e)
        {
            var file = e.File;
            regInfo.FormCO2AllotmentOfShares = file;
            regInfo.FormCO2AllotmentOfSharesName = file.Name;
        }

        public async Task FormCO7ParticularsOfDirectors(InputFileChangeEventArgs e)
        {
            var file = e.File;
            regInfo.FormCO7ParticularsOfDirectors = file;
            regInfo.FormCO7ParticularsOfDirectorsName = file.Name;
        }

        public async Task Memart(InputFileChangeEventArgs e)
        {
            var file = e.File;
            regInfo.Memart = file;
            regInfo.MemartName = file.Name;
        }

        public async Task BoardResolution(InputFileChangeEventArgs e)
        {
            var file = e.File;
            regInfo.BoardResolution = file;
            regInfo.BoardResolutionName = file.Name;
        }
        public async Task SaveRegInfo()
        {

        }

        public void BanktoCoyInfo()
        {
            _NavigationManager?.NavigateTo($"/companyInfo/{id}");
        }

        public async Task SubmitRegInfoForm()
        {
            bool cacOkay = false, tinOkay = false;
            

            //await SetParameters();

            if (Convert.ToDateTime(regInfo?.DateOfIncorporationRegistration).Date >= DateTime.Now.Date)
            {
                _message = "Date of Incorporation cannot be a future date!";
                var parameters = new ModalParameters
                {
                    { nameof(DisplayMessage.Message), _message }
                };

                Modal.Show<DisplayMessage>("Alert", parameters);
                _message = "";
                return;
            }

            if (Convert.ToDateTime(regInfo.DateBusinessStarted).Date >= DateTime.Now.Date)
            {
                _message = "Date business started cannot be a future date!";
                var parameters = new ModalParameters
                {
                    { nameof(DisplayMessage.Message), _message }
                };

                Modal.Show<DisplayMessage>("Alert", parameters);
                _message = "";
                return;
            }

            var resp = await ValidateServ.GetCACDetails(regInfo.CompanyRegistrationNumber);
            if (resp != null)
            {
                if (Convert.ToDateTime(resp.registrationDate).Date == Convert.ToDateTime(regInfo.DateOfIncorporationRegistration).Date
                    && resp.name == info.CompanyName)
                {
                    cacOkay = true;
                }

            }

            var tin = await ValidateServ.GetTINDetails(regInfo.TIN);
            if (resp != null)
            {
                if (Convert.ToDateTime(tin.registrationDate).Date == Convert.ToDateTime(regInfo.DateOfIncorporationRegistration).Date
                    && tin.name == info.CompanyName)
                {
                    tinOkay = true;
                }

            }

            if (tinOkay == false && cacOkay == false)
            {
                _message = "Company Name and Registration Date mismatch!";
                var parameters = new ModalParameters
                {
                    { nameof(DisplayMessage.Message), _message }
                };

                Modal.Show<DisplayMessage>("Alert", parameters);
                _message = "";
            }
            else
            {
                regInfo.JoinedDate = regInfo.JoinedDate ?? DateTime.MinValue.ToString("yyyy-MM-dd");

                await SetParameters();

                var res = await _Acct.InsertRegulatoryInfo(regInfo, info);

                _memoryCache.Set($"reg-{id}", regInfo, TimeSpan.FromMinutes(30));

                if (res.success)
                {

                    var coytype = _memoryCache.Get<string>($"coytype_{id}");
                    if (coytype.ToLower() == "sole proprietorship")
                        _NavigationManager?.NavigateTo($"/soleproprietorship/{id}");
                    else
                        _NavigationManager?.NavigateTo($"/ownership/{id}");
                }
                else
                {
                    _message = res.message;
                    var parameters = new ModalParameters
                    {
                        { nameof(DisplayMessage.Message), _message }
                    };

                    Modal.Show<DisplayMessage>("Alert", parameters);
                    _message = "";
                    return;
                }
            }
        }

        public void MembershipOfAnyOrg(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                regInfo.OrganizationMembership = e.Value.ToString();
                if (regInfo.OrganizationMembership?.Trim().ToUpper() == "YES")
                {
                    membership = true;
                    StateHasChanged();
                }
                else
                {
                    membership = false;
                    StateHasChanged();
                }
            }
        }

        public void LitigationWithThirdParty(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                regInfo.PresentThreatenedLitigationWithThirdParty = e.Value.ToString();
                if (regInfo.PresentThreatenedLitigationWithThirdParty?.Trim().ToUpper() == "YES")
                {
                    litigation = true;
                    StateHasChanged();
                }
                else
                {
                    litigation = false;
                    StateHasChanged();
                }
            }
        }

        public void GetRegDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                regInfo.DateOfIncorporationRegistration = (String)e.Value;
            }
        }

        public void GetBizStartDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                regInfo.DateBusinessStarted = (String)e.Value;
            }
        }

        public void GetJoinedDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                regInfo.JoinedDate = (String)e.Value;
            }
        }

        public void IsCompanyQuoted(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                regInfo.IsYourCompanyQuotedOnAnyStockExchange = e.Value.ToString();
                if (regInfo.IsYourCompanyQuotedOnAnyStockExchange?.Trim().ToUpper() == "YES")
                {
                    companyQuoted = true;
                    StateHasChanged();
                }
                else
                {
                    companyQuoted = false;
                    StateHasChanged();
                }
            }
        }

        private async Task SetParameters()
        {
            
            info.CompanyRegistrationNumber = regInfo.CompanyRegistrationNumber;
            info.DateOfIncorporationRegistration = Convert.ToDateTime(regInfo.DateOfIncorporationRegistration);
            info.DateBusinessStarted = Convert.ToDateTime(regInfo.DateBusinessStarted);
            info.TIN = regInfo.TIN;
            info.SCUMLNumber = regInfo.SCUMLNumber;
            info.CertificateOfIncorporationRegistration = regInfo.CertificateOfIncorporationRegistration;
            info.CertificateOfIncorporationName = regInfo.CertificateOfIncorporationRegistrationName;
            info.FormCO2AllotmentOfShares = regInfo.FormCO2AllotmentOfShares;
            info.FormCO2AllotmentOfSharesName = regInfo.FormCO2AllotmentOfSharesName;
            info.FormCO7ParticularsOfDirectors = regInfo.FormCO7ParticularsOfDirectors;
            info.FormCO7ParticularsOfDirectorsName = regInfo.FormCO7ParticularsOfDirectorsName;
            info.Memart = regInfo.Memart;
            info.MemartName = regInfo.MemartName;
            info.BoardResolution = regInfo.BoardResolution;
            info.BoardResolutionName = regInfo.BoardResolutionName;
            info.AuthorisedShareCapital = regInfo.AuthorisedShareCapital;
            info.PaidUpCapital = regInfo.PaidUpCapital;
            info.AnnualTurnover = regInfo.AnnualTurnover;
            info.CurrentNumberOfEmployees = regInfo.CurrentNumberOfEmployees;
            info.ParentCompany = regInfo.ParentCompany;
            info.SubsidiariesAffiliates = regInfo.SubsidiariesAffiliates;
            info.NatureOfBusiness = regInfo.NatureOfBusiness;
            info.PreviousBusinessEngagedIn = regInfo.PreviousBusinessEngagedIn;
            info.OrganizationMembership = regInfo.OrganizationMembership;
            info.NameOfOrganization = regInfo.NameOfOrganization;
            info.MembershipNumber = regInfo.MembershipNumber;
            info.JoinedDate = Convert.ToDateTime(regInfo.JoinedDate);
            info.PresentThreatenedLitigationWithThirdParty = regInfo.PresentThreatenedLitigationWithThirdParty;
            info.ThirdPartyName = regInfo.ThirdPartyName;
            info.SuitNumber = regInfo.SuitNumber;
            info.IsYourCompanyQuotedOnAnyStockExchange = regInfo.IsYourCompanyQuotedOnAnyStockExchange;
            info.IndicateStockSymbol = regInfo.IndicateStockSymbol;
            info.StockExchange = regInfo.StockExchange;
            info.membership = membership;
            info.litigation = litigation;
            info.companyQuoted = companyQuoted;


            _memoryCache.Set($"info_{id}", info, TimeSpan.FromMinutes(30));

            //string serValue = JsonConvert.SerializeObject(info);
            //string value = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(serValue));
            //return value;
            //_NavigationManager?.NavigateTo($"/regulatoryInfo");
        }
    }
}
