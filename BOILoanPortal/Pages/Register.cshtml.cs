using BOILoanPortal.Models;
using BOILoanPortal.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static BOILoanPortal.Models.UserRegistrationModel;

namespace BOILoanPortal.Pages
{
    public class Register : PageModel
    {
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

        [Inject]
        public NavigationManager _NavigationManager { get; set; }

        [Inject]
        public IAccountOpeningService _Acct { get; set; }

        [Inject]
        public ILoginService _loginService { get; set; }

        [Inject]
        public IJSRuntime _JsRuntime { get; set; }

        public bool isError = false;

        string errorMsg = string.Empty;
        public bool loading { get; set; } = false;

        public List<Resp> states { get; set; } = new();

        [Inject] 
        ProtectedSessionStorage ProtectedSessionStore { get; set; }
        public AuthenticatedUser loginUser { get; set; } = new();

        [Inject]
        public IUtilityService _util { get; set; }

        private readonly IMemoryCache _memoryCache;

        [Inject]
        public AppState appState { get; set; }

        public Register(IAccountOpeningService Acct, ILoginService loginService, NavigationManager NavigationManager,
            IJSRuntime JsRuntime, IUtilityService util, IMemoryCache memoryCache, ProtectedSessionStorage protectedSessionStore)
        {
            _Acct = Acct;
            _loginService = loginService;
            _NavigationManager = NavigationManager;
            _JsRuntime = JsRuntime;
            _util = util;
            _memoryCache = memoryCache;
            ProtectedSessionStore = protectedSessionStore;
        }

        public async Task OnGetAsync()
        {
            //try
            //{
            //    // Clear the existing external cookie
            //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties());
            //}
            //catch { }

            states = await _util.GetStates();
        }

        public async Task<IActionResult> OnPostRegisterAsync()
        {
            loading = true;
            if (!Password.Equals(ConfirmPassword))
            {
                ViewData["ErrorMessage"] = "Password and ConfirmPassword not match!";
                return Page();
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
                ViewData["ErrorMessage"] = response.userDetailViewModel.errorMessage;
                return Page();
            }
            else
            {
                ViewData["ErrorMessage"] = ($"{response.message}. An email has been sent to your email address to activate your account");
                return Page();
            }

            //string returnUrl = Url.Content("~/");
            //return LocalRedirect(returnUrl);
            //loadDetailsPage = true;
            //_NavigationManager.NavigateTo("customer/create", true);
            //_Acct.RegisterUser();
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            //async Task<IActionResult> 

            loading = true;


            if (string.IsNullOrEmpty(Password))
            {
                ViewData["ErrorMessage"] = "Kindly input password";
                return Page();
            }

            try
            {
                // Clear the existing external cookie
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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
                ViewData["ErrorMessage"] = loginUser.userDetail.errorMessage;
                return Page();
            }

            string id = Convert.ToString(loginUser.userDetail.id);
            _memoryCache.Set($"login", loginUser, TimeSpan.FromMinutes(30));

            var response = JsonConvert.SerializeObject(loginUser);

            try
            {
                Claim[] claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, $"Olusola Awopetu"),
                    new Claim(ClaimTypes.Email, loginUser.userDetail.email),
                    new Claim(ClaimTypes.Sid, Convert.ToString(loginUser.userDetail.id))
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                string returnUrl = Url.Content($"/");
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Login failed, please try again. {ex.Message}";
                return Page();
            }


            //string returnUrl = Url.Content("~/companyInfo");
            //return LocalRedirect(returnUrl);
        }

        public void Dispose()
        {

        }
    }
}
