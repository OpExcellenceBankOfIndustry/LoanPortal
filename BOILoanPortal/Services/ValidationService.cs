using BOILoanPortal.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace BOILoanPortal.Services
{
    public interface IValidationService
    {
        Task<BVNResponse?> GetBVNDetails(string BVN);
        Task<CACResponse?> GetCACDetails(string RCNumber);
        Task<IntlPstResponse?> GetINPDetails(string INP, string lastName);
        Task<DriversLicenceResponse?> GetNDLDetails(string NDL);
        Task<NINResponse?> GetNINDetails(string NIN);
        Task<PVCResponse?> GetPVCDetails(string PVC);
        Task<CACResponse?> GetTINDetails(string TIN);
    }

    public class ValidationService : IValidationService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientService _httpService;
        private readonly ILogger<ValidationService> _logger;
        private readonly IMemoryCache _memoryCache;
        private static string BaseUrl = String.Empty;

        public ValidationService(IHttpClientService httpService, ILogger<ValidationService> logger, IMemoryCache memoryCache, IConfiguration config)
        {
            _httpService = httpService;
            _logger = logger;
            _memoryCache = memoryCache;
            _config = config;
            BaseUrl = _config["Endpoints:BaseUrl"];
        }

        public async Task<BVNResponse?> GetBVNDetails(string BVN)
        {
            BVNResponse? bvn = new();

            Dictionary<string, dynamic> headers = new ();
            headers.Add("Content-Type", "multipart/form-data");
            headers.Add("BVN", BVN);
            string getbvn = _config["Endpoints:BVN"];

            string endpoint = string.Concat(BaseUrl, getbvn);

            string AccessToken = ""; // _httpService.GetToken();

            HttpResponseMessage response = await _httpService.PostHttpClient(endpoint, AccessToken, headers);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                bvn = JsonConvert.DeserializeObject<BVNResponse>(responseBody);
            }

            return bvn;
        }

        public async Task<NINResponse?> GetNINDetails(string NIN)
        {
            NINResponse? nin = new();

            Dictionary<string, dynamic> headers = new();
            headers.Add("Content-Type", "multipart/form-data");
            headers.Add("NIN", NIN);
            string getnin = _config["Endpoints:NIN"];

            string endpoint = string.Concat(BaseUrl, getnin);

            string AccessToken = ""; // _httpService.GetToken();

            HttpResponseMessage response = await _httpService.PostHttpClient(endpoint, AccessToken, headers);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                nin = JsonConvert.DeserializeObject<NINResponse>(responseBody);
            }

            return nin;
        }

        public async Task<PVCResponse?> GetPVCDetails(string PVC)
        {
            PVCResponse? pvc = new();

            Dictionary<string, dynamic> headers = new();
            headers.Add("Content-Type", "multipart/form-data");
            headers.Add("PVC", PVC);
            string getpvc = _config["Endpoints:PVC"];

            string endpoint = string.Concat(BaseUrl, getpvc);

            string AccessToken = ""; // _httpService.GetToken();

            HttpResponseMessage response = await _httpService.PostHttpClient(endpoint, AccessToken, headers);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                pvc = JsonConvert.DeserializeObject<PVCResponse>(responseBody);
            }

            return pvc;
        }

        public async Task<IntlPstResponse?> GetINPDetails(string INP, string lastName)
        {
            IntlPstResponse? inp = new();

            Dictionary<string, dynamic> headers = new();
            headers.Add("Content-Type", "multipart/form-data");
            headers.Add("INP", INP);
            headers.Add("lastName", lastName);
            string getpvc = _config["Endpoints:PVC"];

            string endpoint = string.Concat(BaseUrl, getpvc);

            string AccessToken = ""; // _httpService.GetToken();

            HttpResponseMessage response = await _httpService.PostHttpClient(endpoint, AccessToken, headers);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                inp = JsonConvert.DeserializeObject<IntlPstResponse>(responseBody);
            }

            return inp;
        }

        public async Task<DriversLicenceResponse?> GetNDLDetails(string NDL)
        {
            DriversLicenceResponse? ndl = new();

            Dictionary<string, dynamic> headers = new();
            headers.Add("Content-Type", "multipart/form-data");
            headers.Add("NDL", NDL);
            string getpvc = _config["Endpoints:PVC"];

            string endpoint = string.Concat(BaseUrl, getpvc);

            string AccessToken = ""; // _httpService.GetToken();

            HttpResponseMessage response = await _httpService.PostHttpClient(endpoint, AccessToken, headers);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                ndl = JsonConvert.DeserializeObject<DriversLicenceResponse>(responseBody);
            }

            return ndl;
        }

        public async Task<CACResponse?> GetCACDetails(string RCNumber)
        {
            CACResponse? cac = new();

            Dictionary<string, dynamic> headers = new();
            headers.Add("Content-Type", "multipart/form-data");
            headers.Add("CAC", RCNumber);
            string getcac = _config["Endpoints:CAC"];

            string endpoint = string.Concat(BaseUrl, getcac);

            string AccessToken = ""; // _httpService.GetToken();

            HttpResponseMessage response = await _httpService.PostHttpClient(endpoint, AccessToken, headers);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                cac = JsonConvert.DeserializeObject<CACResponse>(responseBody);
            }

            return cac;
        }

        public async Task<CACResponse?> GetTINDetails(string TIN)
        {
            CACResponse? tin = new();

            Dictionary<string, dynamic> headers = new();
            headers.Add("Content-Type", "multipart/form-data");
            headers.Add("TIN", TIN);
            string gettin = _config["Endpoints:TIN"];

            string endpoint = string.Concat(BaseUrl, gettin);

            string AccessToken = ""; // _httpService.GetToken();

            HttpResponseMessage response = await _httpService.PostHttpClient(endpoint, AccessToken, headers);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                tin = JsonConvert.DeserializeObject<CACResponse>(responseBody);
            }

            return tin;
        }

    }
}
