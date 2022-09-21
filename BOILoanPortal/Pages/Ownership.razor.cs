using Blazored.Modal;
using Blazored.Modal.Services;
using BOILoanPortal.Models;
using BOILoanPortal.Services;
using BOILoanPortal.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;
using System.Collections;
using System.Text;

namespace BOILoanPortal.Pages
{
    public partial class Ownership
    {
        [Parameter]
        public CustomerInfo? info { get; set; } = new();

        [Parameter] public AOOwnership? ownershipInfo { get; set; }
        public AOOwnershipInformationIndividual? ioInfo { get; set; } = new();
        public AOOwnershipInformationCooperate? coInfo { get; set; } = new();

        public List<AOOwnershipInformationIndividual>? Inds { get; set; } = new();
        public List<AOOwnershipInformationCooperate>? Corps { get; set; } = new();

        [Inject]
        public NavigationManager? _NavigationManager { get; set; }

        [Inject]
        public IAccountOpeningService? _Acct { get; set; }

        [Inject]
        public IJSRuntime? _JsRuntime { get; set; }

        [Inject]
        public IUtilityService? _util { get; set; }

        [Parameter]
        public string? shareholderType { get; set; }

        public bool indType { get; set; } = false;
        public bool corpType { get; set; } = false;

        [CascadingParameter] public IModalService modal { get; set; } = default!;
        dynamic? indOwnership = null;
        dynamic? corpOwnership = null;


        string _message = "";

        [Parameter] public string? id { get; set; }

        [Inject]
        private IMemoryCache? _memoryCache { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(id))
            {
                ownershipInfo = _memoryCache.Get<AOOwnership>($"owner-{id}");
                if (ownershipInfo == null)
                    ownershipInfo = new AOOwnership();
            }
            else
            {
                indType = true;
                corpType = true;
            }



            


            //ownershipInfo = _memoryCache.Get<AOOwnership>("info");
            await base.OnInitializedAsync();
        }

        public async Task SelectShareholderType(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                shareholderType = e.Value.ToString();
                if (shareholderType.Trim().ToLower() == "individual")
                {
                    indType = true;
                    //corpType = false;
                    StateHasChanged();
                }
                else if (shareholderType.Trim().ToLower() == "corporate")
                {
                    //indType = false;
                    corpType = true;
                    StateHasChanged();
                }
                ownershipInfo.ShareholderType = shareholderType;
            }
        }

        public async Task SaveOwnershipInfo()
        {

        }

        public async Task SubmitOwnershipInfo()
        {
            info = _memoryCache.Get<CustomerInfo>($"info_{id}");
            if (string.IsNullOrEmpty(ownershipInfo.ShareholderType))
            {
                _message = "Kindly select Shareholder type!";
                var parameters = new ModalParameters
                {
                    { nameof(DisplayMessage.Message), _message }
                };

                modal.Show<DisplayMessage>("Alert", parameters);
                _message = "";
                return;
            }
            info.ShareholderType = ownershipInfo.ShareholderType;
            info.Inds = ownershipInfo.Inds;
            info.Corps = ownershipInfo.Corps;


            _memoryCache.Set($"owner-{id}", ownershipInfo, TimeSpan.FromMinutes(30));

            _memoryCache.Set($"info_{id}", info, TimeSpan.FromMinutes(30));

            InsertInfoResponse res = new();
            InsertInfoResponse resp = new();

            if (ownershipInfo.Inds is not null)
            {
                res = await _Acct.InsertIndOwnershipInfo(ownershipInfo.Inds);
            }

            if (ownershipInfo.Corps is not null)
            {
                resp = await _Acct.InsertCorpOwnershipInfo(ownershipInfo.Corps);
            }

            if (res.success && resp.success)
            {

                _NavigationManager?.NavigateTo($"/relatedparty/{id}");
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(res.message);
                builder.Append(resp.message);
                _message = builder.ToString();
                var parameters = new ModalParameters
                    {
                        { nameof(DisplayMessage.Message), _message }
                    };

                modal.Show<DisplayMessage>("Alert", parameters);
                _message = "";
                return;
            }

            _NavigationManager?.NavigateTo($"/relatedparty/{id}");
        }


        async Task ShowIndividualOwnershipInformationModal()
        {
            var options = new ModalOptions
            {
                //ContentScrollable = true, 
                DisableBackgroundCancel = true,
                //UseCustomLayout = true,
            };
            var messageForm = modal.Show<IndividualOwnershipInformation>("", options);
            var result = await messageForm.Result;

            if (!result.Cancelled)
                indOwnership = result?.Data;
            ioInfo = indOwnership;
            ioInfo.UserId = info.UserId;
            ioInfo.UserEmail = info.UserEmail;
            ioInfo.RefNumber = info.RefNumber;
            Inds.Add(ioInfo);
            ownershipInfo.Inds = Inds;
        }

        async Task ShowCorporateOwnershipInformationModal()
        {
            var options = new ModalOptions
            {
                //ContentScrollable = true,
                DisableBackgroundCancel = true,
                //UseCustomLayout = true,
            };
            var messageForm = modal.Show<CorporateOwnershipInformation>("", options);
            var result = await messageForm.Result;

            if (!result.Cancelled)
                corpOwnership = result?.Data;
            coInfo = corpOwnership;
            coInfo.UserId = info.UserId;
            coInfo.UserEmail = info.UserEmail;
            coInfo.RefNumber = info.RefNumber;
            Corps.Add(coInfo);
            ownershipInfo.Corps = Corps;
        }
    
    
    }
}
