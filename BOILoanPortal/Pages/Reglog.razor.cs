using BOILoanPortal.Models;
using BOILoanPortal.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static BOILoanPortal.Models.UserRegistrationModel;

namespace BOILoanPortal.Pages
{
    public partial class Reglog
    {

        #region Model reqion
        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "RCNumber is required")]
        public string? RCNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Business Name is required")]
        public string? BusinessName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Business location is required")]
        public string? BusinessLocation { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Business Type is required")]
        public string? BusinessType { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string? PhoneNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "RegistrationDate is required")]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Confirm Password is required")]
        //[Compare("RegisterModel.Password", ErrorMessage = "The fields Password and PasswordConfirmation should be equals")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        #endregion

        [Inject]
        public ILoginService _loginService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        public IUtilityService? _util { get; set; }

        public List<Resp> States { get; set; } = new();

        [Inject]
        ProtectedSessionStorage ProtectedSessionStore { get; set; }
        public AuthenticatedUser loginUser { get; set; } = new();

        private bool showAuthenticationError = false;
        private string authenticationErrorText = "";

        public bool loading;

        protected override async Task OnInitializedAsync()
        {
            States = await _util.GetStates();
        }

        public async void OnPostRegisterAsync()
        {
            if (!Password.Equals(ConfirmPassword))
            {
                authenticationErrorText = "Password and ConfirmPassword not match!";
                StateHasChanged();
            }

            RegisterUserRequest request = new RegisterUserRequest
            {
                businessName = BusinessName,
                email = Email,
                businessLocation = BusinessLocation,
                businessType = BusinessType,
                phoneNumber = PhoneNumber,
                rcNumber = RCNumber,
                registeredDate = Convert.ToDateTime(RegistrationDate.ToString("yyyy-MM-ddTHH:mm:ss.FFFZ")),
                roleName = "Customer",
                password = Password,
                confirmPassword = ConfirmPassword
            };
            //2020-03-02T08:26:31.888Z
            var response = await _loginService.RegisterUser(request);

            if (response.success == false)
            {
                //_toastService.ShowError($"{response.userDetailViewModel.errorMessage}", "ERROR");
                authenticationErrorText = response.userDetailViewModel.errorMessage;
                StateHasChanged();
            }
            else
            {
                authenticationErrorText = ($"{response.message}. An email has been sent to your email address to activate your account");
                StateHasChanged();
            }

            //string returnUrl = Url.Content("~/");
            //return LocalRedirect(returnUrl);
            //loadDetailsPage = true;
            //_NavigationManager.NavigateTo("customer/create", true);
            //_Acct.RegisterUser();
        }

        public async void OnPostLoginAsync()
        {
            //async Task<IActionResult> 
            
            if (string.IsNullOrEmpty(Password))
            {
                authenticationErrorText = "Kindly input password";
                StateHasChanged();
            }

            try
            {
                // Clear the existing external cookie
                //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }

            AuthenticateUser user = new AuthenticateUser
            {
                email = Email,
                password = Password
            };

            loginUser = await _loginService.LoginUser(user);

            if (loginUser.success == false)
            {
                //_toastService.ShowError($"{response.loginViewModel.errorMessage}", "ERROR");
                //ViewBag.Message = 
                authenticationErrorText = loginUser.userDetail.errorMessage;
                StateHasChanged();
            }

            //UriByPage = linkGenerator.GetUriByPage(this.HttpContext, "/index");

            string id = Convert.ToString(loginUser.userDetail.id);
            //_memoryCache.Set($"login_{id}", response, TimeSpan.FromMinutes(30));

            var response = JsonConvert.SerializeObject(loginUser);

            ///HttpContext.Session.SetString("login", response);

            await ProtectedSessionStore.SetAsync("login", response);

            //await _JsRuntime.InvokeVoidAsync("localStorage.setItem", "login", response);

            try
            {
                Claim[] claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, $"Olusola Awopetu"),
                    new Claim(ClaimTypes.Email, loginUser.userDetail.email),
                    //new Claim(ClaimTypes.GroupSid, userInfo.department)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                NavigationManager.NavigateTo("/", true);

                //string returnUrl = Url.Content($"~/index/{id}");
                //return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                authenticationErrorText = "Login failed, please try again.";
                StateHasChanged();
            }


            //string returnUrl = Url.Content("~/companyInfo");
            //return LocalRedirect(returnUrl);
        }
    }
}
