//using Blazored.LocalStorage;
using BOILoanPortal.Models;
using BOILoanPortal.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static BOILoanPortal.Models.UserRegistrationModel;

namespace BOILoanPortal.Services
{
    public interface ILoginService
    {
        Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request);
        Task<ForgetPasswordResponse> ForgetPassword(ForgetPasswordRequest request);
        Task<AuthenticatedUser> LoginUser(AuthenticateUser request);
        Task<RegisterUserResponse> RegisterUser(RegisterUserRequest registerUser);
        Task Logout();
    }

    public class LoginService : ILoginService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientService _httpService;
        private readonly ILogger<LoginService> _logger;
        private readonly IMemoryCache _memoryCache;
        private static string BaseUrl = String.Empty;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public LoginService(IHttpClientService httpService, ILogger<LoginService> logger, IMemoryCache memoryCache, IConfiguration config, 
            AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _httpService = httpService;
            _logger = logger;
            _memoryCache = memoryCache;
            _config = config;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            BaseUrl = _config["Endpoints:BaseUrl"];
        }

        public async Task<AuthenticatedUser> LoginUser(AuthenticateUser request)
        {
            //string responseContent = "";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            
            string userLogin = _config["Endpoints:Authenticate"];
            AuthenticatedUser loginDetails = new();

            string AccessToken = ""; //_httpService.GetToken();

            headers.Add("Content-Type", "application/json");
            headers.Add("Authorization", $"Bearer {AccessToken}");

            //string endpoint = string.Concat(BaseUrl, userLogin);

            IRestResponse responseContent = await _httpService.DoWebRequest(BaseUrl, userLogin, "POST", request, headers);

            //HttpResponseMessage response = await _httpService.ExecutePostHttpRequest(request, endpoint).ConfigureAwait(false);

            //if (responseContent.StatusCode == HttpStatusCode.OK)
            //{
                string responseBody = responseContent.Content;
                loginDetails = JsonConvert.DeserializeObject<AuthenticatedUser>(responseBody);

                if (loginDetails.success.Equals(true))
                {
                    
                    ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(loginDetails);
                    
                }
            //}

            return loginDetails;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItem("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            //_client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request)
        {
            string userChangePassword = _config["Endpoints:ChangePassword"];
            ChangePasswordResponse? changePasswordResponse = new();
            string endpoint = string.Concat(BaseUrl, userChangePassword);

            HttpResponseMessage response = await _httpService.ExecutePostHttpRequest(request, endpoint, "").ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                changePasswordResponse = JsonConvert.DeserializeObject<ChangePasswordResponse>(responseBody);

                if (changePasswordResponse.success.Equals(true))
                {
                    return changePasswordResponse;
                }
            }

            return changePasswordResponse;
        }

        public async Task<ForgetPasswordResponse> ForgetPassword(ForgetPasswordRequest request)
        {
            string userForgotPassword = _config["Endpoints:ForgetPassword"];
            ForgetPasswordResponse forgotPasswordResponse = new();
            string endpoint = string.Concat(BaseUrl, userForgotPassword);

            HttpResponseMessage response = await _httpService.ExecutePostHttpRequest(request, endpoint, "").ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                forgotPasswordResponse = JsonConvert.DeserializeObject<ForgetPasswordResponse>(responseBody);

                if (forgotPasswordResponse.success.Equals(true))
                {
                    return forgotPasswordResponse;
                }
            }

            return forgotPasswordResponse;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest registerUser)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string userRegister = _config["Endpoints:Register"];
            RegisterUserResponse registerResponse = new();
            string endpoint = string.Concat(BaseUrl, userRegister);

            string AccessToken = _httpService.GetToken();
            //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

            HttpResponseMessage response = await _httpService.ExecutePostHttpRequest(registerUser, endpoint, AccessToken).ConfigureAwait(false);
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                registerResponse = JsonConvert.DeserializeObject<RegisterUserResponse>(responseBody);

                if (!registerResponse.success.Equals(true))
                {
                    return registerResponse;
                }
            }

            return registerResponse;
        }
    }
}
