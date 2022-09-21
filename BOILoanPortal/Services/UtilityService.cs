using Blazored.Modal;
using BOILoanPortal.Models;
using BOILoanPortal.Shared;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BOILoanPortal.Services
{
    public interface IUtilityService
    {
        Task<Country> Countries();
        Task<List<Resp>> GetAnnualTurnover();
        Task<List<Resp>> GetBanks();
        Task<List<Resp>> GetCompanyType();
        Task<List<Resp>> GetEducationalQualification();
        Task<List<Resp>> GetIdType();
        Task<List<Resp>?> GetLGA(string StateID);
        Task<List<Resp>> GetMaritalStatus();
        Task<List<Resp>> GetStakeholderCapacity();
        Task<List<Resp>> GetStates();
        Task<List<Resp>> GetTitle();
        Task<List<Resp>?> GetCity(string LgaId);
        ModalParameters GetModalParameter(string message);
    }

    public class UtilityService : IUtilityService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientService _httpService;
        private readonly ILogger<UtilityService> _logger;
        private readonly IMemoryCache _memoryCache;
        private static string BaseUrl = String.Empty;

        public UtilityService(IHttpClientService httpService, ILogger<UtilityService> logger, IMemoryCache memoryCache, IConfiguration config)
        {
            _httpService = httpService;
            _logger = logger;
            _memoryCache = memoryCache;
            _config = config;
            BaseUrl = _config["Endpoints:BaseUrl"];
        }

        public async Task<Country> Countries()
        {
            Country countries = new();
            countries = _memoryCache.Get<Country>("countries");

            if (countries == null)
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();
                string getCountries = _config["Endpoints:Country"];
                string endpoint = string.Concat(BaseUrl, getCountries);

                string AccessToken = ""; // _httpService.GetToken();

                //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

                HttpResponseMessage response = await _httpService.ExecuteGetHttpRequest(endpoint, AccessToken).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    countries = JsonConvert.DeserializeObject<Country>(responseBody);

                    _memoryCache.Set("countries", countries, TimeSpan.FromMinutes(30));
                }
            }

            return countries;
        }
        public async Task<List<Resp>> GetStates()
        {
            List<Resp>? state = new();

            state = _memoryCache.Get<List<Resp>>("state");

            if (state == null)
            {

                Dictionary<string, string> headers = new Dictionary<string, string>();
                string getState = _config["Endpoints:State"];

                string endpoint = string.Concat(BaseUrl, getState);

                string AccessToken = ""; // _httpService.GetToken();
                                         //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

                HttpResponseMessage response = await _httpService.ExecuteGetHttpRequest(endpoint, AccessToken).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    state = JsonConvert.DeserializeObject<List<Resp>>(responseBody);

                    _memoryCache.Set("state", state, TimeSpan.FromMinutes(30));
                }
            }

            return state;
        }

        public async Task<List<Resp>?> GetLGA(string StateID)
        {
            List<Resp>? lga = new();

            lga = _memoryCache.Get<List<Resp>>($"{StateID}");

            if (lga == null)
            {

                Dictionary<string, string> headers = new Dictionary<string, string>();
                string getLga = _config["Endpoints:LGA"];
                string endpoint = string.Concat(BaseUrl, getLga, $"/{StateID}");

                string AccessToken = ""; // _httpService.GetToken();
                                         //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

                HttpResponseMessage response = await _httpService.ExecuteGetHttpRequest(endpoint, AccessToken).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    lga = JsonConvert.DeserializeObject<List<Resp>?>(responseBody);

                    _memoryCache.Set($"{StateID}", lga, TimeSpan.FromMinutes(30));
                }
            }

            return lga;
        }

        public async Task<List<Resp>?> GetCity(string LgaId)
        {
            List<Resp>? city = new();

            //city = _memoryCache.Get<Cities>($"{LgaId}");

            if (city.Count == 0)
            {

                Dictionary<string, string> headers = new Dictionary<string, string>();
                string getcity = _config["Endpoints:City"];

                string endpoint = string.Concat(BaseUrl, getcity, $"/{LgaId}");

                string AccessToken = ""; // _httpService.GetToken();
                                         //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

                HttpResponseMessage response = await _httpService.ExecuteGetHttpRequest(endpoint, AccessToken).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    city = JsonConvert.DeserializeObject<List<Resp>?>(responseBody);

                    //_memoryCache.Set($"{LgaId}", city, TimeSpan.FromMinutes(30));
                }
            }

            return city;
        }

        public async Task<List<Resp>> GetBanks()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string getBanks = _config["Endpoints:Banks"];
            //Banks banks = new();
            string endpoint = string.Concat(BaseUrl, getBanks);

            string AccessToken = ""; // _httpService.GetToken();
            //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

            HttpResponseMessage response = await _httpService.ExecuteGetHttpRequest(endpoint, AccessToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var banks = JsonConvert.DeserializeObject<List<Resp>>(responseBody);

                return banks;
            }
            else
                return null;


        }

        public async Task<List<Resp>> GetCompanyType()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string GetCompanyType = _config["Endpoints:GetCompanyType"];
            //Banks banks = new();
            string endpoint = string.Concat(BaseUrl, GetCompanyType);

            string AccessToken = ""; // _httpService.GetToken();
            //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

            HttpResponseMessage response = await _httpService.ExecuteGetHttpRequest(endpoint, AccessToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var coyType = JsonConvert.DeserializeObject<List<Resp>>(responseBody);

                return coyType;
            }
            else
                return null;
        }

        public async Task<List<Resp>> GetMaritalStatus()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string GetMaritalStatus = _config["Endpoints:MaritalStatus"];
            //Banks banks = new();
            string endpoint = string.Concat(BaseUrl, GetMaritalStatus);

            string AccessToken = ""; // _httpService.GetToken();
            //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

            HttpResponseMessage response = await _httpService.ExecuteGetHttpRequest(endpoint, AccessToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var mStatus = JsonConvert.DeserializeObject<List<Resp>>(responseBody);

                return mStatus;
            }
            else
                return null;
        }

        public async Task<List<Resp>> GetAnnualTurnover()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string GetAnnualTurnover = _config["Endpoints:AnnualTurnover"];
            //Banks banks = new();
            string endpoint = string.Concat(BaseUrl, GetAnnualTurnover);

            string AccessToken = ""; // _httpService.GetToken();
            //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

            HttpResponseMessage response = await _httpService.ExecuteGetHttpRequest(endpoint, AccessToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var aTurnover = JsonConvert.DeserializeObject<List<Resp>>(responseBody);

                return aTurnover;
            }
            else
                return null;
        }

        public async Task<List<Resp>> GetIdType()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string GetIdType = _config["Endpoints:IdType"];
            //Banks banks = new();
            string endpoint = string.Concat(BaseUrl, GetIdType);

            string AccessToken = ""; // _httpService.GetToken();
            //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

            HttpResponseMessage response = await _httpService.ExecuteGetHttpRequest(endpoint, AccessToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var idType = JsonConvert.DeserializeObject<List<Resp>>(responseBody);

                return idType;
            }
            else
                return null;
        }

        public async Task<List<Resp>> GetStakeholderCapacity()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string GetStakeholderCapacity = _config["Endpoints:StakeholderCapacity"];
            //Banks banks = new();
            string endpoint = string.Concat(BaseUrl, GetStakeholderCapacity);

            string AccessToken = ""; // _httpService.GetToken();
            //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

            HttpResponseMessage response = await _httpService.ExecuteGetHttpRequest(endpoint, AccessToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var stakeholderCapacities = JsonConvert.DeserializeObject<List<Resp>>(responseBody);

                return stakeholderCapacities;
            }
            else
                return null;
        }

        public async Task<List<Resp>> GetEducationalQualification()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string GetEducationalQualification = _config["Endpoints:EducationalQualification"];
            //Banks banks = new();
            string endpoint = string.Concat(BaseUrl, GetEducationalQualification);

            string AccessToken = ""; // _httpService.GetToken();
            //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

            HttpResponseMessage response = await _httpService.ExecuteGetHttpRequest(endpoint, AccessToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var educationalQualifications = JsonConvert.DeserializeObject<List<Resp>>(responseBody);

                return educationalQualifications;
            }
            else
                return null;
        }

        public async Task<List<Resp>> GetTitle()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string GetTitle = _config["Endpoints:Title"];
            //Banks banks = new();
            string endpoint = string.Concat(BaseUrl, GetTitle);

            string AccessToken = ""; // _httpService.GetToken();
            //IRestResponse response = await _httpService.DoWebRequest(BaseUrl, userRegister, "POST", registerUser, headers);

            HttpResponseMessage response = await _httpService.ExecuteGetHttpRequest(endpoint, AccessToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var titles = JsonConvert.DeserializeObject<List<Resp>>(responseBody);

                return titles;
            }
            else
                return null;
        }

        public ModalParameters GetModalParameter(string message)
        {
            return new ModalParameters
            {
                { nameof(DisplayMessage.Message), message }
            };
        }
    }
}
