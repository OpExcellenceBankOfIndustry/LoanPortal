//using Blazored.LocalStorage;
using BOILoanPortal.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BOILoanPortal.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        //private readonly HttpClient _httpClient;
        private readonly AuthenticationState _anonymous;
        public AppState appState;
        private dynamic? token;

        public AuthStateProvider(ILocalStorageService localStorage, AppState appState)
        {
            _localStorage = localStorage;
            this.appState = appState;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItem<string>("authToken");

            //await Task.Delay(1500);

            if (string.IsNullOrWhiteSpace(token))
            {
                return _anonymous;
            }

            //var anonymous = new ClaimsIdentity();
            //return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));

            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token),
                    "jwtAuthType")));

            //return null;
        }

        public void NotifyUserAuthentication(AuthenticatedUser userDetail)
        {
            //_localStorage.SetItem("authToken", userDetail.userDetail.jwtToken);
            //_localStorage.SetItem("user", userDetail);
            var authenticatedUser = new ClaimsPrincipal(
                    new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(userDetail.userDetail.jwtToken),
                    "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
