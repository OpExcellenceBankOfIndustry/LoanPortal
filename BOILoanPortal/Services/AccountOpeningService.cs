using BOILoanPortal.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BOILoanPortal.Services
{
    public interface IAccountOpeningService
    {
        Task<InsertInfoResponse?> InsertAccountInfo(List<AOAccountDetailsOfOwner> request);
        Task<InsertInfoResponse?> InsertCompanyInfo(AOCompanyInformation? request);
        Task<InsertInfoResponse?> InsertCorpOwnershipInfo(List<AOOwnershipInformationCooperate> requests);
        Task<InsertInfoResponse?> InsertIndOwnershipInfo(List<AOOwnershipInformationIndividual> requests);
        Task<InsertInfoResponse?> InsertNextOfKinInfo(List<AODetailsOfNextOfKin> request);
        Task<InsertInfoResponse?> InsertRegulatoryInfo(AORegulatoryInformation request, CustomerInfo info);
        Task<InsertInfoResponse?> InsertRelatedPartyInfo(AORelatedPartyInformation request);
        Task<InsertInfoResponse?> InsertSoleProprietorshipInfo(AOSoleProprietorship request);
        void RegisterUser();
    }

    public class AccountOpeningService : IAccountOpeningService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientService? _httpService;
        private readonly ILogger<AccountOpeningService> _logger;
        private static string BaseUrl = "";
        private readonly NavigationManager _NavigationManager;

        public AccountOpeningService(IHttpClientService? httpService, ILogger<AccountOpeningService> logger,
            NavigationManager NavigationManager, IConfiguration config)
        {
            _httpService = httpService;
            _logger = logger;
            _NavigationManager = NavigationManager;
            _config = config;
            BaseUrl = _config["Endpoints:BaseUrl"];
        }

        //T GetObject<T>(params object[] lstArgument)
        //{
        //    return (T)Activator.CreateInstance(typeof(T), lstArgument);
        //}


        public async Task<InsertInfoResponse?> InsertCompanyInfo(AOCompanyInformation? request)
        {
            //List<AOCompanyInformation> req = new();
            //req.Add(request);

            ArrayList req = new ArrayList();
            req.Add(request);

            InsertInfoResponse insertResp = new();
            string? AccessToken = _httpService?.GetToken();
            string? Endpoint = _config["Endpoints:CompanyInfo"];

            //string? Url = string.Concat(BaseUrl, Endpoint);

            Dictionary<string, string?> headerParams = new();
            headerParams.Add("Content-Type", "multipart/form-data");
            headerParams.Add("Authorization", $"Bearer {AccessToken}");

            List<Dictionary<string, string?>> formParams = new();

            Dictionary<string, string?> formParam = new();
            formParam.Add(nameof(request.UserId), request.UserId);
            formParam.Add(nameof(request.UserEmail), request?.UserEmail);
            formParam.Add(nameof(request.CompanyName), request?.CompanyName);
            formParam.Add(nameof(request.CompanyShortName), request?.CompanyShortName);
            formParam.Add(nameof(request.CompanyType), request?.CompanyType);
            formParam.Add(nameof(request.CompanyAddress), request?.CompanyAddress);
            formParam.Add(nameof(request.Landmark), request?.Landmark);
            formParam.Add(nameof(request.State), request?.State);
            formParam.Add(nameof(request.LGA), request?.LGA);
            formParam.Add(nameof(request.TownCity), request?.TownCity);
            formParam.Add(nameof(request.PhoneNumber), Convert.ToString(request?.PhoneNumber));
            formParam.Add(nameof(request.EmailAddress), request?.EmailAddress);
            formParam.Add(nameof(request.Website), request?.Website);
            formParam.Add(nameof(request.Twitter), request?.Twitter);
            formParam.Add(nameof(request.Facebook), request?.Facebook);
            formParam.Add(nameof(request.Instagram), request?.Instagram);
            formParam.Add(nameof(request.CompanyOwnsBusinessPremises), request?.CompanyOwnsBusinessPremises);

            formParams.Add(formParam);

            var fileByte = new byte[request.ProofOfCompanyAddress.Size];
            //await request.ProofOfCompanyAddress.OpenReadStream().ReadAsync(fileByte);
            var fileName = request.ProofOfCompanyAddress.Name;
            var fileContentType = request.ProofOfCompanyAddress.ContentType;

            //Tuple<int, string, string> person = new Tuple<int, string, string>(1, "Steve", "Jobs");
            var fileList = new List<Tuple<string, string, byte[]?>>();
            Tuple<string, string, byte[]?> fileParams = new Tuple<string, string, byte[]?>(nameof(request.ProofOfCompanyAddress), fileName, fileByte);
            fileList.Add(fileParams);

            //IRestResponse responseContent = await _httpService.CallApiAsync(BaseUrl, Endpoint, Method.POST, headerParams, formParams, fileList);
            IRestResponse responseContent = await _httpService.CallApiAsync2(req, BaseUrl, Endpoint, headerParams, formParams, fileList);

            if (responseContent != null)
            {
                if (responseContent.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = responseContent.Content;
                    insertResp = JsonConvert.DeserializeObject<InsertInfoResponse>(responseBody);

                    if (insertResp.success.Equals(true))
                    {
                        return insertResp;
                    }
                }
            }

            return insertResp;
        }

        public async Task<InsertInfoResponse?> InsertRegulatoryInfo(AORegulatoryInformation request, CustomerInfo info)
        {
            InsertInfoResponse insertResp = new();
            try
            {
                request.UserId = info.UserId;
                request.UserEmail = info?.UserEmail;
                request.RefNumber = info?.RefNumber;


                ArrayList req = new ArrayList();
                req.Add(request);

                string? AccessToken = _httpService?.GetToken();
                string? Endpoint = _config["Endpoints:RegulatoryInfo"];

                string? Url = string.Concat(BaseUrl, Endpoint);

                Dictionary<string, string?> headerParams = new();
                headerParams.Add("Content-Type", "multipart/form-data");
                headerParams.Add("Authorization", $"Bearer {AccessToken}");

                //Dictionary<string, string?> formParams = new();

                Dictionary<string, string?> formParams = new();
                formParams.Add(nameof(request.UserId), info.UserId);
                formParams.Add(nameof(request.UserEmail), info?.UserEmail);
                formParams.Add(nameof(request.RefNumber), info?.RefNumber);
                formParams.Add(nameof(request.CompanyRegistrationNumber), request?.CompanyRegistrationNumber);
                formParams.Add(nameof(request.DateOfIncorporationRegistration), Convert.ToString(request?.DateOfIncorporationRegistration));
                formParams.Add(nameof(request.DateBusinessStarted), Convert.ToString(request?.DateBusinessStarted));
                formParams.Add(nameof(request.TIN), request?.TIN);
                formParams.Add(nameof(request.SCUMLNumber), request?.SCUMLNumber);
                formParams.Add(nameof(request.AuthorisedShareCapital), request?.AuthorisedShareCapital);
                formParams.Add(nameof(request.PaidUpCapital), request?.PaidUpCapital);
                formParams.Add(nameof(request.AnnualTurnover), Convert.ToString(request?.AnnualTurnover));
                formParams.Add(nameof(request.CurrentNumberOfEmployees), request?.CurrentNumberOfEmployees);
                formParams.Add(nameof(request.ParentCompany), request?.ParentCompany);
                formParams.Add(nameof(request.SubsidiariesAffiliates), request?.SubsidiariesAffiliates);
                formParams.Add(nameof(request.NatureOfBusiness), request?.NatureOfBusiness);
                formParams.Add(nameof(request.PreviousBusinessEngagedIn), request?.PreviousBusinessEngagedIn);
                formParams.Add(nameof(request.OrganizationMembership), request?.OrganizationMembership);
                formParams.Add(nameof(request.NameOfOrganization), Convert.ToString(request?.NameOfOrganization));
                formParams.Add(nameof(request.MembershipNumber), request?.MembershipNumber);
                formParams.Add(nameof(request.JoinedDate), Convert.ToString(request?.JoinedDate));
                //formParsms.Add(nameof(request.SubsidiariesAffiliates), request?.SubsidiariesAffiliates);
                formParams.Add(nameof(request.PresentThreatenedLitigationWithThirdParty), request?.PresentThreatenedLitigationWithThirdParty);
                formParams.Add(nameof(request.ThirdPartyName), request?.ThirdPartyName);
                formParams.Add(nameof(request.SuitNumber), request?.SuitNumber);
                formParams.Add(nameof(request.IsYourCompanyQuotedOnAnyStockExchange), request?.IsYourCompanyQuotedOnAnyStockExchange);
                formParams.Add(nameof(request.IndicateStockSymbol), request?.IndicateStockSymbol);
                formParams.Add(nameof(request.StockExchange), request?.StockExchange);

                //formParams.Add(formParam);

                //var fileByte = new byte[request.CertificateOfIncorporationRegistration.Size];
                //var fileName = request.CertificateOfIncorporationRegistration.Name;

                var fileList = new List<Tuple<string, string, byte[]?>>();
                Tuple<string, string, byte[]?> COIfile = new Tuple<string, string, byte[]?>(nameof(request.CertificateOfIncorporationRegistration), request.CertificateOfIncorporationRegistration.Name, new byte[request.CertificateOfIncorporationRegistration.Size]);
                fileList.Add(COIfile);
                Tuple<string, string, byte[]?> CO2file = new Tuple<string, string, byte[]?>(nameof(request.FormCO2AllotmentOfShares), request.FormCO2AllotmentOfShares.Name, new byte[request.FormCO2AllotmentOfShares.Size]);
                fileList.Add(CO2file);
                Tuple<string, string, byte[]?> CO7file = new Tuple<string, string, byte[]?>(nameof(request.FormCO7ParticularsOfDirectors), request.FormCO7ParticularsOfDirectors.Name, new byte[request.FormCO7ParticularsOfDirectors.Size]);
                fileList.Add(CO7file);
                Tuple<string, string, byte[]?> memart = new Tuple<string, string, byte[]?>(nameof(request.Memart), request.Memart.Name, new byte[request.Memart.Size]);
                fileList.Add(memart);
                Tuple<string, string, byte[]?> BRfile = new Tuple<string, string, byte[]?>(nameof(request.BoardResolution), request.BoardResolution.Name, new byte[request.BoardResolution.Size]);
                fileList.Add(BRfile);

                IRestResponse responseContent = await _httpService.CallApiAsync(BaseUrl, Endpoint, Method.POST, headerParams, formParams, fileList);

                if (responseContent != null)
                {
                    if (responseContent.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = responseContent.Content;
                        insertResp = JsonConvert.DeserializeObject<InsertInfoResponse>(responseBody);

                        if (insertResp.success.Equals(true))
                        {
                            return insertResp;
                        }
                    }
                    else
                    {
                        InsertInfoFailedResponse insertFailedResp = new();
                        string responseBody = responseContent.Content;
                        insertFailedResp = JsonConvert.DeserializeObject<InsertInfoFailedResponse>(responseBody);
                        //insertResp.success = false;
                        insertResp.message = Convert.ToString(insertFailedResp.errors);

                        return insertResp;

                    }
                }

                return insertResp;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("An error occured - {0}, || {1}, || {2}", ex.Source, ex.Message, ex.InnerException);
                return insertResp;
            }

        }

        public async Task<InsertInfoResponse?> InsertRelatedPartyInfo(AORelatedPartyInformation request)
        {
            InsertInfoResponse insertResp = new InsertInfoResponse();
            try
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();

                string? AccessToken = _httpService?.GetToken();
                string RelatedPartyInfo = _config["Endpoints:RelatedPartyInfo"];

                headers.Add("Content-Type", "application/json");
                headers.Add("Authorization", $"Bearer {AccessToken}");

                //string endpoint = string.Concat(BaseUrl, userLogin);

                IRestResponse responseContent = await _httpService.DoWebRequest(BaseUrl, RelatedPartyInfo, "POST", request, headers);

                //IRestResponse responseContent = await _httpService.CallApiAsync(BaseUrl, Endpoint, Method.POST, headerParams, formParams);

                if (responseContent != null)
                {
                    if (responseContent.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = responseContent.Content;
                        insertResp = JsonConvert.DeserializeObject<InsertInfoResponse>(responseBody);

                        if (insertResp.success.Equals(true))
                        {
                            return insertResp;
                        }
                    }
                }

                return insertResp;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("An error occured - {0}, || {1} || {2}", ex.Source, ex.Message, ex.InnerException);
                return insertResp;
            }

        }

        public async Task<InsertInfoResponse?> InsertAccountInfo(List<AOAccountDetailsOfOwner> request)
        {
            InsertInfoResponse? insertResp = new();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            try
            {
                ArrayList requests = new ArrayList();
                requests.Add(requests);

                string? AccessToken = _httpService?.GetToken();
                string AccountInfo = _config["Endpoints:AccountInfo"];

                headers.Add("Content-Type", "application/json");
                headers.Add("Authorization", $"Bearer {AccessToken}");

                //string endpoint = string.Concat(BaseUrl, userLogin);

                IRestResponse responseContent = await _httpService.DoWebRequest(BaseUrl, AccountInfo, "POST", request, headers);

                if (responseContent != null)
                {
                    if (responseContent.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = responseContent.Content;
                        insertResp = JsonConvert.DeserializeObject<InsertInfoResponse>(responseBody);

                        if (insertResp.success.Equals(true))
                        {
                            return insertResp;
                        }
                    }
                }

                return insertResp;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("An error occured - {0}, || {1}, || {2}", ex.Source, ex.Message, ex.InnerException);
                return insertResp;
            }

        }

        public async Task<InsertInfoResponse?> InsertNextOfKinInfo(List<AODetailsOfNextOfKin> request)
        {
            InsertInfoResponse? insertResp = new InsertInfoResponse();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            try
            {
                ArrayList requests = new ArrayList();
                requests.Add(requests);

                string? AccessToken = _httpService?.GetToken();
                string? NextOfKinInfo = _config["Endpoints:NextOfKinInfo"];

                headers.Add("Content-Type", "application/json");
                headers.Add("Authorization", $"Bearer {AccessToken}");

                //string endpoint = string.Concat(BaseUrl, userLogin);

                IRestResponse responseContent = await _httpService.DoWebRequest(BaseUrl, NextOfKinInfo, "POST", request, headers);

                if (responseContent != null)
                {
                    if (responseContent.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = responseContent.Content;
                        insertResp = JsonConvert.DeserializeObject<InsertInfoResponse>(responseBody);

                        if (insertResp.success.Equals(true))
                        {
                            return insertResp;
                        }
                    }
                }

                return insertResp;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("An error occured - {0}, || {1} || {2}", ex.Source, ex.Message, ex.InnerException);
                return insertResp;
            }

        }

        public async Task<InsertInfoResponse?> InsertSoleProprietorshipInfo(AOSoleProprietorship request)
        {
            InsertInfoResponse insertResp = new();
            try
            {

                string? AccessToken = _httpService?.GetToken();
                string SolePro = _config["Endpoints:SoleProprietorshipInfo"];

                string Endpoint = string.Concat(BaseUrl, SolePro);

                Dictionary<string, string?> headerParams = new();
                headerParams.Add("Content-Type", "multipart/form-data");
                headerParams.Add("Authorization", $"Bearer {AccessToken}");

                Dictionary<string, string?> formParams = new();

                formParams.Add(nameof(request.UserId), request.UserId);
                formParams.Add(nameof(request.UserEmail), request?.UserEmail);
                formParams.Add(nameof(request.RefNumber), request?.RefNumber);
                formParams.Add(nameof(request.Title), request?.Title);
                formParams.Add(nameof(request.FirstName), Convert.ToString(request?.FirstName));
                formParams.Add(nameof(request.Surname), Convert.ToString(request?.Surname));
                formParams.Add(nameof(request.OtherNames), request?.OtherNames);
                formParams.Add(nameof(request.MaidenName), request?.MaidenName);
                formParams.Add(nameof(request.DateOfBirth), Convert.ToString(request?.DateOfBirth));
                formParams.Add(nameof(request.PlaceOfBirth), request?.PlaceOfBirth);
                formParams.Add(nameof(request.Gender), Convert.ToString(request?.Gender));
                formParams.Add(nameof(request.MaritalStatus), request?.MaritalStatus);
                formParams.Add(nameof(request.PhoneNumber), request?.PhoneNumber);
                formParams.Add(nameof(request.EmailAddress), request?.EmailAddress);
                formParams.Add(nameof(request.HomeAddress), request?.HomeAddress);
                formParams.Add(nameof(request.State), request?.State);
                formParams.Add(nameof(request.LGA), request?.LGA);
                formParams.Add(nameof(request.TownCity), Convert.ToString(request?.TownCity));
                formParams.Add(nameof(request.HighestEducationalQualification), request?.HighestEducationalQualification);
                formParams.Add(nameof(request.BVN), request?.BVN);
                formParams.Add(nameof(request.NIN), request?.NIN);
                formParams.Add(nameof(request.TIN), request?.TIN);
                formParams.Add(nameof(request.PercentageOwnership), request?.PercentageOwnership);
                formParams.Add(nameof(request.Nationality), request?.Nationality);
                formParams.Add(nameof(request.CERPACIssueDate), Convert.ToString(request?.CERPACIssueDate));
                formParams.Add(nameof(request.CERPACExpiryDate), Convert.ToString(request?.CERPACExpiryDate));
                formParams.Add(nameof(request.IdentificationType), request?.IdentificationType);
                formParams.Add(nameof(request.IdentificationNumber), request?.IdentificationNumber);
                formParams.Add(nameof(request.IdentificationIssueDate), Convert.ToString(request?.IdentificationIssueDate));
                formParams.Add(nameof(request.IdentificationExpiryDate), Convert.ToString(request?.IdentificationExpiryDate));
                formParams.Add(nameof(request.Occupation), request?.Occupation);
                formParams.Add(nameof(request.JobTitle), request?.JobTitle);
                formParams.Add(nameof(request.AnyPoliticalAffilationWithAPoliticalOfficeHolder), request?.AnyPoliticalAffilationWithAPoliticalOfficeHolder);
                formParams.Add(nameof(request.IndicatePoliticalOfficerRelationship), Convert.ToString(request?.IndicatePoliticalOfficerRelationship));
                formParams.Add(nameof(request.ContestedPoliticalOffice), request?.ContestedPoliticalOffice);
                formParams.Add(nameof(request.IndicatePoliticalOffice), Convert.ToString(request?.IndicatePoliticalOffice));

                //formParams.Add(formParam);

                var fileList = new List<Tuple<string, string, byte[]?>>();
                if (request.ProofOfAddress is not null)
                {
                    Tuple<string, string, byte[]?> POAfile = new Tuple<string, string, byte[]?>(nameof(request.ProofOfAddress), request.ProofOfAddress.Name, new byte[request.ProofOfAddress.Size]);
                    fileList.Add(POAfile);
                }
                if (request.CERPAC is not null)
                {
                    Tuple<string, string, byte[]?> CERPACfile = new Tuple<string, string, byte[]?>(nameof(request.CERPAC), request.CERPAC.Name, new byte[request.CERPAC.Size]);
                    fileList.Add(CERPACfile);
                }
                if (request.IdentificationFile is not null)
                {
                    Tuple<string, string, byte[]?> IDfile = new Tuple<string, string, byte[]?>(nameof(request.IdentificationFile), request.IdentificationFile.Name, new byte[request.IdentificationFile.Size]);
                    fileList.Add(IDfile);
                }
                if (request.PassportPhotograph is not null)
                {
                    Tuple<string, string, byte[]?> PPTfile = new Tuple<string, string, byte[]?>(nameof(request.PassportPhotograph), request.PassportPhotograph.Name, new byte[request.PassportPhotograph.Size]);
                    fileList.Add(PPTfile);
                }
                if (request.UploadCV is not null)
                {
                    Tuple<string, string, byte[]?> CVfile = new Tuple<string, string, byte[]?>(nameof(request.UploadCV), request.UploadCV.Name, new byte[request.UploadCV.Size]);
                    fileList.Add(CVfile);
                }

                IRestResponse responseContent = await _httpService.CallApiAsync(BaseUrl, SolePro, Method.POST, headerParams, formParams, fileList);

                if (responseContent != null)
                {
                    if (responseContent.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = responseContent.Content;
                        insertResp = JsonConvert.DeserializeObject<InsertInfoResponse>(responseBody);

                        if (insertResp.success.Equals(true))
                        {
                            return insertResp;
                        }
                    }
                }

                return insertResp;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("An error occured - {0}, || {1}, || {2}", ex.Source, ex.Message, ex.InnerException);
                return insertResp;
            }

        }

        public async Task<InsertInfoResponse?> InsertCorpOwnershipInfo(List<AOOwnershipInformationCooperate> requests)
        {
            InsertInfoResponse insertResp = new();

            int count = requests.Count;

            int counted = 0;

            IRestResponse? responseContent = null;

            try
            {



                string AccessToken = _httpService.GetToken();
                string Endpoint = _config["Endpoints:CorpOwnershipInfo"];

                Dictionary<string, string?> headerParams = new();
                headerParams.Add("Content-Type", "multipart/form-data");
                headerParams.Add("Authorization", $"Bearer {AccessToken}");

                //List<Dictionary<string, string?>> formParams = new();

                foreach (var request in requests)
                {
                    Dictionary<string, string?> formParams = new();

                    formParams.Add(nameof(request.UserId), request.UserId);
                    formParams.Add(nameof(request.UserEmail), request?.UserEmail);
                    formParams.Add(nameof(request.RefNumber), request?.RefNumber);
                    formParams.Add(nameof(request.NameOfShareholdingCompany), request?.NameOfShareholdingCompany);
                    formParams.Add(nameof(request.RegistrationNumber), Convert.ToString(request?.RegistrationNumber));
                    formParams.Add(nameof(request.PercentageOwnership), Convert.ToString(request?.PercentageOwnership));
                    formParams.Add(nameof(request.State), request?.State);
                    formParams.Add(nameof(request.LGA), request?.LGA);
                    formParams.Add(nameof(request.TownCity), Convert.ToString(request?.TownCity));
                    formParams.Add(nameof(request.PhoneNumber), request?.PhoneNumber);
                    formParams.Add(nameof(request.EmailAddress), request?.EmailAddress);
                    formParams.Add(nameof(request.Website), request?.Website);
                    formParams.Add(nameof(request.Twitter), request?.Twitter);
                    formParams.Add(nameof(request.Facebook), request?.Facebook);
                    formParams.Add(nameof(request.Instagram), request?.Instagram);
                    formParams.Add(nameof(request.CompanyAddress), request?.CompanyAddress);
                    formParams.Add(nameof(request.ContactPersonName), request?.ContactPersonName);

                    List<Tuple<string, string, byte[]?>> fileList = new();
                    if (request.CertificateOfIncorporationRegistration is not null)
                    {
                        Tuple<string, string, byte[]?> RCFile = new Tuple<string, string, byte[]?>(nameof(request.CertificateOfIncorporationRegistration), request.CertificateOfIncorporationRegistration.Name, new byte[request.CertificateOfIncorporationRegistration.Size]);
                        //indFiles.ProofOfAddress = POAfile;
                        fileList.Add(RCFile);
                    }
                    

                    responseContent = await _httpService.CallApiAsync(BaseUrl, Endpoint, Method.POST, headerParams, formParams, fileList);


                    if (responseContent != null)
                    {
                        if (responseContent.StatusCode == HttpStatusCode.OK)
                        {
                            string responseBody = responseContent.Content;
                            insertResp = JsonConvert.DeserializeObject<InsertInfoResponse>(responseBody);

                            if (insertResp.success.Equals(true))
                            {
                                counted++;
                            }
                        }
                    }

                    if (count == counted)
                    {
                        return new InsertInfoResponse
                        {
                            companyInformViewModel = null,
                            message = "Records inserted successfully",
                            refNumber = null,
                            success = true,
                        };
                    }
                }




                //ArrayList requests = new ArrayList();
                //requests.Add(req);

                //string? AccessToken = _httpService?.GetToken();
                //string Endpoint = _config["Endpoints:CorpOwnershipInfo"];

                //Dictionary<string, string?> headerParams = new();
                //string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
                //string contentType = "multipart/form-data; boundary=" + formDataBoundary;
                //headerParams.Add("Content-Type", contentType);
                //headerParams.Add("Authorization", $"Bearer {AccessToken}");

                ////List<Dictionary<string, string?>> formParams = new();

                //foreach (var request in requests)
                //{
                //    Dictionary<string, string?> formParam = new();

                //    formParam.Add(nameof(request.UserId), request.UserId);
                //    formParam.Add(nameof(request.UserEmail), request?.UserEmail);
                //    formParam.Add(nameof(request.RefNumber), request?.RefNumber);
                //    formParam.Add(nameof(request.NameOfShareholdingCompany), request?.NameOfShareholdingCompany);
                //    formParam.Add(nameof(request.RegistrationNumber), Convert.ToString(request?.RegistrationNumber));
                //    formParam.Add(nameof(request.PercentageOwnership), Convert.ToString(request?.PercentageOwnership));
                //    formParam.Add(nameof(request.State), request?.State);
                //    formParam.Add(nameof(request.LGA), request?.LGA);
                //    formParam.Add(nameof(request.TownCity), Convert.ToString(request?.TownCity));
                //    formParam.Add(nameof(request.PhoneNumber), request?.PhoneNumber);
                //    formParam.Add(nameof(request.EmailAddress), request?.EmailAddress);
                //    formParam.Add(nameof(request.Website), request?.Website);
                //    formParam.Add(nameof(request.Twitter), request?.Twitter);
                //    formParam.Add(nameof(request.Facebook), request?.Facebook);
                //    formParam.Add(nameof(request.Instagram), request?.Instagram);
                //    formParam.Add(nameof(request.CompanyAddress), request?.CompanyAddress);
                //    formParam.Add(nameof(request.ContactPersonName), request?.ContactPersonName);

                //    List<Tuple<string, string, byte[]?>> fileList = new();

                //    IRestResponse responseContent = await _httpService.CallApiAsync(BaseUrl, Endpoint, Method.POST, headerParams, formParam, fileList);

                //    if (responseContent != null)
                //    {
                //        if (responseContent.StatusCode == HttpStatusCode.OK)
                //        {
                //            string responseBody = responseContent.Content;

                //            if (responseBody is not null || !string.IsNullOrEmpty(responseBody))
                //            {
                //                insertResp = JsonConvert.DeserializeObject<InsertInfoResponse>(responseBody);

                //                if (insertResp.success.Equals(true))
                //                {
                //                    counted++;
                //                }
                //            }
                //        }
                //    }
                //}

                //if (counted > 0)
                //{
                //    return new InsertInfoResponse
                //    {
                //        companyInformViewModel = null,
                //        message = "Records inserted successfully",
                //        refNumber = null,
                //        success = true,
                //    };
                //}


                return insertResp;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("An error occured - {0}, || {1} || {2}", ex.Source, ex.Message, ex.InnerException);
                return insertResp;
            }

        }

        public async Task<InsertInfoResponse?> InsertIndOwnershipInfo(List<AOOwnershipInformationIndividual> requests)
        {
            InsertInfoResponse insertResp = new();

            int count = requests.Count;

            int counted = 0;

            IRestResponse? responseContent = null;
            try
            {
                //ArrayList requests = new ArrayList();
                //requests.Add(requests);
                               

                string AccessToken = _httpService.GetToken();
                string Endpoint = _config["Endpoints:IndOwnershipInfo"];

                Dictionary<string, string?> headerParams = new();
                headerParams.Add("Content-Type", "multipart/form-data");
                headerParams.Add("Authorization", $"Bearer {AccessToken}");

                //List<Dictionary<string, string?>> formParams = new();

                foreach (var request in requests)
                {
                    Dictionary<string, string?> formParams = new();

                    formParams.Add(nameof(request.UserId), request.UserId);
                    formParams.Add(nameof(request.UserEmail), request?.UserEmail);
                    formParams.Add(nameof(request.RefNumber), request?.RefNumber);
                    formParams.Add(nameof(request.Title), request?.Title);
                    formParams.Add(nameof(request.FirstName), Convert.ToString(request?.FirstName));
                    formParams.Add(nameof(request.Surname), Convert.ToString(request?.Surname));
                    formParams.Add(nameof(request.OtherNames), request?.OtherNames);
                    formParams.Add(nameof(request.MaidenName), request?.MaidenName);
                    formParams.Add(nameof(request.DateOfBirth), Convert.ToString(request?.DateOfBirth));
                    formParams.Add(nameof(request.PlaceOfBirth), request?.PlaceOfBirth);
                    formParams.Add(nameof(request.Gender), Convert.ToString(request?.Gender));
                    formParams.Add(nameof(request.MaritalStatus), request?.MaritalStatus);
                    formParams.Add(nameof(request.PhoneNumber), Convert.ToString(request?.PhoneNumber));
                    formParams.Add(nameof(request.EmailAddress), request?.EmailAddress);
                    formParams.Add(nameof(request.HomeAddress), request?.HomeAddress);
                    formParams.Add(nameof(request.State), request?.State);
                    formParams.Add(nameof(request.LGA), request?.LGA);
                    formParams.Add(nameof(request.TownCity), Convert.ToString(request?.TownCity));
                    formParams.Add(nameof(request.HighestEducationalQualification), request?.HighestEducationalQualification);
                    formParams.Add(nameof(request.BVN), request?.BVN);
                    formParams.Add(nameof(request.NIN), request?.NIN);
                    formParams.Add(nameof(request.TIN), request?.TIN);
                    formParams.Add(nameof(request.PercentageOwnership), request?.PercentageOwnership);
                    formParams.Add(nameof(request.Nationality), request?.Nationality);
                    formParams.Add(nameof(request.CERPACIssueDate), Convert.ToString(request?.CERPACIssueDate));
                    formParams.Add(nameof(request.CERPACExpiryDate), Convert.ToString(request?.CERPACExpiryDate));
                    formParams.Add(nameof(request.IdentificationType), request?.IdentificationType);
                    formParams.Add(nameof(request.IdentificationNumber), request?.IdentificationNumber);
                    formParams.Add(nameof(request.IdentificationIssueDate), Convert.ToString(request?.IdentificationIssueDate));
                    formParams.Add(nameof(request.IdentificationExpiryDate), Convert.ToString(request?.IdentificationExpiryDate));
                    formParams.Add(nameof(request.Occupation), request?.Occupation);
                    formParams.Add(nameof(request.JobTitle), request?.JobTitle);
                    formParams.Add(nameof(request.AnyPoliticalAffilationWithAPoliticalOfficeHolder), request?.AnyPoliticalAffilationWithAPoliticalOfficeHolder);
                    formParams.Add(nameof(request.IndicatePoliticalOfficerRelationship), Convert.ToString(request?.IndicatePoliticalOfficerRelationship));
                    formParams.Add(nameof(request.ContestedPoliticalOffice), request?.ContestedPoliticalOffice);
                    formParams.Add(nameof(request.IndicatePoliticalOffice), Convert.ToString(request?.IndicatePoliticalOffice));

                    List<Tuple<string, string, byte[]?>> fileList = new();
                    if (request.ProofOfAddress is not null)
                    {
                        Tuple<string, string, byte[]?> POAfile = new Tuple<string, string, byte[]?>(nameof(request.ProofOfAddress), request.ProofOfAddress.Name, new byte[request.ProofOfAddress.Size]);
                        //indFiles.ProofOfAddress = POAfile;
                        fileList.Add(POAfile);
                    }
                    if (request.CERPAC is not null)
                    {
                        Tuple<string, string, byte[]?> CERPACfile = new Tuple<string, string, byte[]?>(nameof(request.CERPAC), request.CERPAC.Name, new byte[request.CERPAC.Size]);
                        //indFiles.CERPAC = CERPACfile;
                        fileList.Add(CERPACfile);
                    }
                    if (request.IdentificationFile is not null)
                    {
                        Tuple<string, string, byte[]?> IDfile = new Tuple<string, string, byte[]?>(nameof(request.IdentificationFile), request.IdentificationFile.Name, new byte[request.IdentificationFile.Size]);
                        //indFiles.IdentificationFile = IDfile;
                        fileList.Add(IDfile);
                    }
                    if (request.PassportPhotograph is not null)
                    {
                        Tuple<string, string, byte[]?> PPTfile = new Tuple<string, string, byte[]?>(nameof(request.PassportPhotograph), request.PassportPhotograph.Name, new byte[request.PassportPhotograph.Size]);
                        //indFiles.PassportPhotograph = PPTfile;
                        fileList.Add(PPTfile);
                    }
                    if (request.UploadCV is not null)
                    {
                        Tuple<string, string, byte[]?> CVfile = new Tuple<string, string, byte[]?>(nameof(request.UploadCV), request.UploadCV.Name, new byte[request.UploadCV.Size]);
                        //indFiles.UploadCV = CVfile;
                        fileList.Add(CVfile);
                    }

                    responseContent = await _httpService.CallApiAsync(BaseUrl, Endpoint, Method.POST, headerParams, formParams, fileList);


                    if (responseContent != null)
                    {
                        if (responseContent.StatusCode == HttpStatusCode.OK)
                        {
                            string responseBody = responseContent.Content;
                            insertResp = JsonConvert.DeserializeObject<InsertInfoResponse>(responseBody);

                            if (insertResp.success.Equals(true))
                            {
                                counted++;
                            }
                        }
                    }

                    if(count == counted)
                    {
                        return new InsertInfoResponse
                        {
                            companyInformViewModel = null,
                            message = "Records inserted successfully",
                            refNumber = null,
                            success = true,
                        };
                    }
                }

                return insertResp;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("An error occured - {0}, || {1}, || {2}", ex.Source, ex.Message, ex.InnerException);
                return insertResp;
            }

        }

        public void RegisterUser()
        {
            _NavigationManager.NavigateTo("customer/create", true);
        }


    }
}
