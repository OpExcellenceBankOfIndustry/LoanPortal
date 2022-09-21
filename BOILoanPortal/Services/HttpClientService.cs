using BOILoanPortal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BOILoanPortal.Services
{
    public interface IHttpClientService
    {
        Task<IRestResponse> CallApiAsync(string BaseUrl, string resource, Method Method, Dictionary<string, string> HeaderParams, Dictionary<string, string> FormParams, List<Tuple<string, string, byte[]>>? FileParams = null, Dictionary<string, string>? QueryParams = null, string PostBody = "");
        Task<IRestResponse> CallApiAsync2(ArrayList entity, string BaseUrl, string resource, Dictionary<string, string> HeaderParams, List<Dictionary<string, string>> FormParams, List<Tuple<string, string, byte[]>>? FileParams = null);
        Task<IRestResponse> DoWebRequest(string BaseUrl, string RelativeUrl, string HttpMethod, object? requestObject = null, Dictionary<string, string> headers = null, string AccessToken = null);
        Task<HttpResponseMessage> ExecuteGetHttpRequest(string endpoint, string? AccessToken = null);
        Task<HttpResponseMessage> ExecutePostHttpRequest(object request, string endpoint, string AccessToken = null);
        string? GetBearerToken();
        string? GetToken();
        Task<HttpResponseMessage?> PostHttpClient(string? endpoint, string? AccessToken = "", Dictionary<string, dynamic>? headers = null);
        Task<HttpResponseMessage> PostHttpClientGenerics(string? endpoint, string accessToken, string contentType, HttpContent content);
        Task<IRestResponse> upload(string content, string filename, byte[] file);
        //Task HttpContextSignout();
    }

    public class HttpClientService : IHttpClientService
    {
        //protected readonly HttpClient _httpClient;
        private readonly ILogger<HttpClientService> _logger;
        private readonly IConfiguration _config;
        private readonly string BaseURL = String.Empty;
        private string Login = String.Empty;
        private string Username = String.Empty;
        private string Password = String.Empty;
        private readonly IMemoryCache _memoryCache;
        //private readonly HttpContext HttpContext;
        public HttpClientService(ILogger<HttpClientService> logger, IConfiguration config, 
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _config = config;
            BaseURL = Convert.ToString(_config["Endpoints:BaseUrl"]);
            Login = _config["Endpoints:Login"].ToString();
            Username = _config["AppSettings:UserName"].ToString();
            Password = _config["AppSettings:Password"].ToString();
            _memoryCache = memoryCache;
            //HttpContext = httpContext;
        }

        public async Task<HttpResponseMessage> PostHttpClientGenerics(string? endpoint, string accessToken, string contentType, HttpContent content)
        {
            HttpResponseMessage? response = new HttpResponseMessage();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", contentType);
                    if (!string.IsNullOrEmpty(accessToken))
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");

                    response = await httpClient.PostAsync(endpoint, content);

                    return response;
                }


                //using (var _httpClient = new HttpClient())
                //{
                //    MultipartFormDataContent form = new MultipartFormDataContent();
                //    String headerValue = "form-data; name=\"file\"; filename=\"" + fileName + "\"";
                //    byte[] bytes = Encoding.UTF8.GetBytes(headerValue);
                //    headerValue = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                //    fileContent.Headers.Add("Content-Disposition", headerValue);
                //    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                //    form.Add(fileContent);
                //    form.Add(new StringContent(metadataValue), metadataKey);
                //    _httpClient.Timeout = TimeSpan.FromMinutes(15);
                //    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);
                //    logger.LogInformation($"HttpUtils: UploadFileByMultipart() url:{url}, request param: {metadataValue} reference: {traceLogId}");
                //    var response = await _httpClient.PostAsync(url, form).Result.Content.ReadAsStringAsync();
                //    logger.LogInformation("HttpUtils: UploadFileByMultipart() end");
                //    return response;
                //}
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error occurred  - {ex.Message} || {ex.InnerException}");
                return response;
            }
        }


        public async Task<HttpResponseMessage?> PostHttpClient(string? endpoint, string? AccessToken = "", Dictionary<string, dynamic?>? headers = null)
        {
            HttpResponseMessage? response = new HttpResponseMessage();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var formdata = new MultipartFormDataContent();

                    if (headers != null && headers.Count > 0)
                    {
                        foreach (KeyValuePair<string, dynamic?> item in headers)
                        {
                            formdata.Add(new StringContent(item.Value), item.Key);
                        }
                    }
                    if (!string.IsNullOrEmpty(AccessToken))
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {AccessToken}");

                    response = await httpClient.PostAsync(endpoint, formdata);

                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error occurred  - {ex.Message} || {ex.InnerException}");
                return response;
            }


        }
        public async Task<HttpResponseMessage> ExecutePostHttpRequest(object request, string endpoint, string AccessToken = null)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                _logger.LogInformation($"request endpoint - ", endpoint);
                var httpClient = new HttpClient();
                //string AccessToken = GetToken();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(AccessToken))
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {AccessToken}");

                _logger.LogInformation($"API Request for {endpoint} : {JsonConvert.SerializeObject(request)}");

                response = await httpClient.PostAsJsonAsync(endpoint, request);
                _logger.LogInformation($"API response for {endpoint} : {JsonConvert.SerializeObject(response)}");
            }
            catch (Exception ex)
            {
                //return response;
                _logger.LogInformation($"Error occurred  - {ex.Message} || {ex.InnerException}");
                _logger.LogInformation($"API response for {endpoint} : {JsonConvert.SerializeObject(response)}");
            }
            return response;
        }

        public async Task<HttpResponseMessage> ExecuteGetHttpRequest(string endpoint, string? AccessToken = null)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(AccessToken))
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {AccessToken}");
                response = await httpClient.GetAsync(endpoint);
                _logger.LogInformation($"API response: {JsonConvert.SerializeObject(response)}");
            }
            catch (Exception ex)
            {
                //return response;
                _logger.LogInformation("Error occurred  - " + ex.Message + "||" + ex.InnerException);
                _logger.LogInformation($"API response: {JsonConvert.SerializeObject(response)}");
            }
            return response;
        }

        public async Task<IRestResponse> DoWebRequest(string BaseUrl, string RelativeUrl, string HttpMethod, object? requestObject = null, Dictionary<string, string> headers = null, string AccessToken = null)// where T : new()
        {
            IRestResponse result = null;
            //var result = new TResponse();
            //string AccessToken = GetToken();

            string ApiUrl = String.Concat(BaseUrl, RelativeUrl);
            _logger.LogInformation($"\n\nCall to Endpoint Url => {ApiUrl} at {DateTime.Now}");
            try
            {
                //bypass all server certificate issues...
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                RestClient restClient = new RestClient($"{ApiUrl}");
                Method method = Method.GET;
                if (HttpMethod != null)
                {
                    if (HttpMethod.ToLower().Equals("get"))
                    {
                        method = Method.GET;
                    }
                    if (HttpMethod.ToLower().Equals("post") && requestObject != null)
                    {
                        method = Method.POST;

                    }
                    if (HttpMethod.ToLower().Equals("put") && requestObject != null)
                    {
                        method = Method.PUT;
                    }
                }
                RestRequest restRequest = new RestRequest(method);
                if (headers != null && headers.Count > 0)
                {
                    foreach (KeyValuePair<string, string> item in headers)
                    {
                        restRequest.AddHeader(item.Key, item.Value);
                    }
                }
                if (requestObject != null)
                {
                    restRequest.AddJsonBody(requestObject);
                }

                IRestResponse response = await restClient.ExecuteAsync(restRequest);
                _logger.LogInformation($"\nApi Request Execution Done!");


                HttpStatusCode httpStatusCode = response.StatusCode;
                int numericStatusCode = (int)httpStatusCode;
                _logger.LogInformation($"\nStatus Code that came from the Response for API Call: {numericStatusCode}");

                if (numericStatusCode == 200)
                {
                    _logger.LogInformation($"\n\n{method.ToString()} REQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => SUCCESS");
                    result = response;
                    _logger.LogInformation($"\n\nAPI Fetched Successfully {JsonConvert.DeserializeObject(Convert.ToString(result.Content))} at {DateTime.Now}\n");
                }
                else if (response.ContentType == "text/html" || (numericStatusCode == 401 || numericStatusCode == 404 || numericStatusCode == 502 || numericStatusCode == 504))
                {

                    _logger.LogInformation($"\n\nOops! An Error Occurred while calling API Endpoint: {ApiUrl} \nError StatusCode: {numericStatusCode.ToString()}\nError Status Description: {response.StatusDescription} at {DateTime.Now}\n");
                    result = response;
                }
                else
                {
                    _logger.LogInformation($"\n\nOops! An Unknown Error Occurred while calling Token Endpoint: {ApiUrl} at {DateTime.Now}\n");
                    result = response;
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                //return result;
                _logger.LogError($"ERROR OCCURRED REQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => FAILED EXCEPTION => {ex.Message}");
                //result = ex.Message.ToString();
            }

            return result;
        }

        public string? GetToken()
        {
            string? token = string.Empty;

            token = _memoryCache.Get<string>("token");

            if (string.IsNullOrEmpty(token))
            {
                TokenResponse? tokenResponse = new TokenResponse();
                _logger.LogInformation($"\n\nAT {nameof(GetBearerToken)} now to fetch Auth Token from AuthEndpoint: ");
                string Url = string.Concat(BaseURL, Login);
                _logger.LogInformation($"\n\nToken URL => {Url}");

                TokenRequest requestBody = new TokenRequest { userName = Username, password = Password, channel = "BOI" };

                HttpResponseMessage response = ExecutePostHttpRequest(requestBody, Url).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseBody);
                    token = tokenResponse?.authenticateUserViewModel?.bearerToken;

                    _memoryCache.Set("token", token, TimeSpan.FromMinutes(30));

                    return token;
                }
            }

            return token;
        }

        public string? GetBearerToken()
        {
            string? token = string.Empty;
            _logger.LogInformation($"\n\nAT {nameof(GetBearerToken)} now to fetch Auth Token from AuthEndpoint: ");
            string Url = string.Concat(BaseURL, Login);
            _logger.LogInformation($"\n\nToken URL => {Url}");
            RestClient client = new RestClient(Url);
            client.Timeout = 10;
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            TokenRequest requestBody = new TokenRequest { userName = Username, password = Password, channel = "BOI" };
            request.AddJsonBody(requestBody);

            _logger.LogInformation($"Attempting to Execute Prepared Token Request now...");
            IRestResponse response = client.Execute(request);
            _logger.LogInformation($"Token Request Execution Done!");


            HttpStatusCode httpStatusCode = response.StatusCode;
            int numericStatusCode = (int)httpStatusCode;
            _logger.LogInformation($"Status Code that came from the Response for Token: {numericStatusCode}");

            TokenResponse? tokenResponse = new TokenResponse();

            if (numericStatusCode == 200)
            {
                tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Content);
                token = tokenResponse?.authenticateUserViewModel?.bearerToken;
                //tokenResponse.setAccessToken(tokenResponse.Token);
                _logger.LogInformation($"\n\nBearer Token Fetched Successfully {JsonConvert.SerializeObject(tokenResponse)} at {DateTime.Now}\n");
            }
            else if (response.ContentType == "text/html" || (numericStatusCode == 401 || numericStatusCode == 404 || numericStatusCode == 502 || numericStatusCode == 504))
            {

                _logger.LogInformation($"\n\nOops! An Error Occurred while calling Token Endpoint: {Url} \nError StatusCode: {numericStatusCode.ToString()}\nError Status Description: {response.StatusDescription} at {DateTime.Now}\n");

            }
            else
            {
                _logger.LogInformation($"\n\nOops! An Unknown Error Occurred while calling Token Endpoint: {Url} at {DateTime.Now}\n");

            }

            Console.WriteLine("=====================================" + tokenResponse);
            _logger.LogInformation($"\n\nToken Response => {JsonConvert.SerializeObject(tokenResponse)}\n");

            return token;
        }

        public async Task<IRestResponse> CallApiAsync(string BaseUrl, String resource, Method Method,
            Dictionary<String, String> HeaderParams, Dictionary<String, String> FormParams,
            List<Tuple<string, string, byte[]>>? FileParams = null, Dictionary<String, String>? QueryParams = null, String PostBody = "")
        {
            IRestResponse response = new RestResponse();

            try
            {
                RestClient restClient = new RestClient(BaseUrl);
                RestRequest request = new RestRequest(resource, Method);
                var req = new List<RestRequest>();

                //UpdateParamsForAuth(QueryParams, HeaderParams, AuthSettings);

                // add default header, if any
                //foreach (KeyValuePair<string, string> defaultHeader in this.defaultHeaderMap)
                //    request.AddHeader(defaultHeader.Key, defaultHeader.Value);

                // add header parameter, if any
                if (HeaderParams != null)
                {
                    foreach (KeyValuePair<string, string> param in HeaderParams)
                        request.AddHeader(param.Key, param.Value);
                }

                // add query parameter, if any
                if (QueryParams != null)
                {
                    foreach (KeyValuePair<string, string> param in QueryParams)
                        request.AddQueryParameter(param.Key, param.Value);
                }

                // add form parameter, if any
                if (FormParams != null)
                {
                    foreach (KeyValuePair<string, string> param in FormParams)
                    {
                        request.AddParameter(param.Key, param.Value);
                    }



                    //foreach (KeyValuePair<string, string> param in FormParams)
                    //{
                    //    request.AddParameter(param.Key, param.Value);
                    //}
                }


                // add file parameter, if any
                if (FileParams != null)
                {
                    foreach (var param in FileParams)
                        //foreach (KeyValuePair<string, byte[]> param in FileParams)
                        //request.AddFile("file", param.Value, param.Key);
                        request.AddFile(param.Item1, param.Item3, param.Item2);
                }

                //if (PostBody != null)
                //{
                //    request.AddParameter("application/json", PostBody, ParameterType.RequestBody); // http body (model) parameter
                //}

                response = await restClient.ExecuteAsync(request);

                _logger.LogInformation($"\nApi Request Execution Done!");


                HttpStatusCode httpStatusCode = response.StatusCode;
                int numericStatusCode = (int)httpStatusCode;
                _logger.LogInformation($"\nStatus Code that came from the Response for API Call: {numericStatusCode}");

                if (numericStatusCode == 200)
                {
                    _logger.LogInformation($"\n\nREQUEST MADE TO ENDPOINT => {resource} STATUS => SUCCESS");
                    _logger.LogInformation($"\n\nAPI Fetched Successfully {JsonConvert.DeserializeObject(Convert.ToString(response.Content))} at {DateTime.Now}\n");
                }
                else if (response.ContentType == "text/html" || (numericStatusCode == 401 || numericStatusCode == 404 || numericStatusCode == 502 || numericStatusCode == 504))
                {
                    _logger.LogInformation($"\n\nOops! An Error Occurred while calling API Endpoint: {resource} \nError StatusCode: {numericStatusCode.ToString()}\nError Status Description: {response.StatusDescription} at {DateTime.Now}\n");
                }
                else
                {
                    _logger.LogInformation($"\n\nOops! An Unknown Error Occurred while calling Token Endpoint: {resource} at {DateTime.Now}\n");
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured - {0}, || {1}, || {2}", ex.Source, ex.Message, ex.InnerException);
                return response;
            }



        }


        public async Task<IRestResponse> CallApiAsync2(ArrayList entity, string BaseUrl, String resource, Dictionary<String, String> HeaderParams,
            List<Dictionary<String, String>> FormParams, List<Tuple<string, string, byte[]>>? FileParams = null)
        {
            IRestResponse response = new RestResponse();

            try
            {
                RestClient restClient = new RestClient(BaseUrl);
                RestRequest request = new RestRequest(resource, Method.POST);

                //UpdateParamsForAuth(QueryParams, HeaderParams, AuthSettings);

                // add default header, if any
                //foreach (KeyValuePair<string, string> defaultHeader in this.defaultHeaderMap)
                //    request.AddHeader(defaultHeader.Key, defaultHeader.Value);

                // add header parameter, if any
                if (HeaderParams != null)
                {
                    foreach (KeyValuePair<string, string> param in HeaderParams)
                        request.AddHeader(param.Key, param.Value);
                }

                // add query parameter, if any
                //if (QueryParams != null)
                //{
                //    foreach (KeyValuePair<string, string> param in QueryParams)
                //        request.AddQueryParameter(param.Key, param.Value);
                //}

                // add form parameter, if any
                if (FormParams != null)
                {
                    foreach (var param in entity)
                    {
                        request.AddObject(param);
                    }
                }


                // add file parameter, if any
                if (FileParams != null)
                {
                    foreach (var param in FileParams)
                        //foreach (KeyValuePair<string, byte[]> param in FileParams)
                        //request.AddFile("file", param.Value, param.Key);
                        request.AddFile(param.Item1, param.Item3, param.Item2);
                        //request.AddObject(param);
                }

                //if (PostBody != null)
                //{
                //    request.AddParameter("application/json", PostBody, ParameterType.RequestBody); // http body (model) parameter
                //}

                response = await restClient.ExecuteAsync(request);

                _logger.LogInformation($"\nApi Request Execution Done!");


                HttpStatusCode httpStatusCode = response.StatusCode;
                int numericStatusCode = (int)httpStatusCode;
                _logger.LogInformation($"\nStatus Code that came from the Response for API Call: {numericStatusCode}");

                if (numericStatusCode == 200)
                {
                    _logger.LogInformation($"\n\nREQUEST MADE TO ENDPOINT => {resource} STATUS => SUCCESS");
                    _logger.LogInformation($"\n\nAPI Fetched Successfully {JsonConvert.DeserializeObject(Convert.ToString(response.Content))} at {DateTime.Now}\n");
                }
                else if (response.ContentType == "text/html" || (numericStatusCode == 401 || numericStatusCode == 404 || numericStatusCode == 502 || numericStatusCode == 504))
                {
                    _logger.LogInformation($"\n\nOops! An Error Occurred while calling API Endpoint: {resource} \nError StatusCode: {numericStatusCode.ToString()}\nError Status Description: {response.StatusDescription} at {DateTime.Now}\n");
                }
                else
                {
                    _logger.LogInformation($"\n\nOops! An Unknown Error Occurred while calling Token Endpoint: {resource} at {DateTime.Now}\n");
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured - {0}, || {1}, || {2}", ex.Source, ex.Message, ex.InnerException);
                return response;
            }



        }



        public async Task<IRestResponse> upload(String content, String filename, byte[] file)
        {
            RestClient client = new RestClient();

            RestRequest request = new RestRequest("/api/upload.json", Method.POST);
            //request.AddParameter("source", Constants.SOURCE);
            request.AddParameter("content", content);
            request.AddFile("file", file, filename);

            IRestResponse response = await client.ExecuteAsync(request);

            return response;
        }





        private static readonly Encoding encoding = Encoding.UTF8;
        public static HttpWebResponse MultipartFormPost(string postUrl, string userAgent, Dictionary<string, object> postParameters, string headerkey, string headervalue)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            return PostForm(postUrl, userAgent, contentType, formData, headerkey, headervalue);
        }
        private static HttpWebResponse PostForm(string postUrl, string userAgent, string contentType, byte[] formData, string headerkey, string headervalue)
        {
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            // Set up the request properties.
            request.Method = "POST";
            request.ContentType = contentType;
            request.UserAgent = userAgent;
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;

            // You could add authentication here as well if needed:
            // request.PreAuthenticate = true;
            // request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;

            //Add header if needed
            request.Headers.Add(headerkey, headervalue);

            // Send the form data to the request.
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            return request.GetResponse() as HttpWebResponse;
        }

        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {

                if (needsCLRF)
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is FileParameter) // to check if parameter if of file type
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));
                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

        //public async Task HttpContextSignout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //}


        public class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }

    }
}
