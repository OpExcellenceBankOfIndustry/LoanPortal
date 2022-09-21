using Blazored.Modal;
using Blazored.Modal.Services;
using BOILoanPortal.Models;
using BOILoanPortal.Services;
using BOILoanPortal.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BOILoanPortal.Pages
{
    public partial class Login
    {
        [Parameter]
        public LoginModel login { get; set; } = new();

        [Inject]
        public NavigationManager? _NavigationManager { get; set; }

        [Inject]
        public ILoginService? _loginService { get; set; }

        public bool isError = false;

        string errorMsg = string.Empty;
        public bool loading { get; set; } = false;

        public List<Resp> states { get; set; } = new();

        [Inject]
        ILocalStorageService? ProtectedSessionStore { get; set; }
        public AuthenticatedUser loginUser { get; set; } = new();

        [Inject]
        public IUtilityService? _util { get; set; }

        [Inject] 
        IAlertService AlertService { get; set; }

        [CascadingParameter] public IModalService Modal { get; set; } = default!;

        [Inject]
        public AppState? appState { get; set; }
        string _message = "";
        protected override void OnInitialized()
        {
            appState.OnStateChange += StateHasChanged;
        }

        //protected override void OnAfterRender(bool firstRender)
        //{

        //}

        public async Task OnValidLoginSubmit()
        {
            loading = true;

            AuthenticateUser user = new AuthenticateUser
            {
                email = login.Email,
                password = login.Password
            };

            loginUser = await _loginService.LoginUser(user);

            if (loginUser.success == false)
            {
                AlertService.Error(loginUser.userDetail.errorMessage);
                loading = false;
                StateHasChanged();

                //Modal.Show<DisplayMessage>("Alert", _util.GetModalParameter(loginUser.userDetail.errorMessage));
                //StateHasChanged();
            }

            string id = Convert.ToString(loginUser.userDetail.id);

            appState.SetParameters(loginUser);

            ProtectedSessionStore.SetItem("authToken", loginUser.userDetail.jwtToken);   

            var response = JsonConvert.SerializeObject(loginUser);

            try
            {




                //Claim[] claims = new Claim[]
                //{
                //    new Claim(ClaimTypes.Name, loginUser.userDetail.businessName),
                //    new Claim(ClaimTypes.Email, loginUser.userDetail.email),
                //    new Claim(ClaimTypes.Sid, Convert.ToString(loginUser.userDetail.id))
                //};

                //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                _NavigationManager?.NavigateTo($"/");
            }
            catch (Exception ex)
            {
                AlertService.Error(ex.Message);
                loading = false;
                StateHasChanged();
                //_message = $"Login failed, please try again. {ex.Message}";
                //Modal.Show<DisplayMessage>("Alert", _util.GetModalParameter(_message));
                //_message = "";
                //StateHasChanged();
            }
        }

        public void Dispose()
        {
            appState.OnStateChange -= StateHasChanged;
        }
    }
}
