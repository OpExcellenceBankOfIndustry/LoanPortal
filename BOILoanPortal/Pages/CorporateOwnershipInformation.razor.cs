using Blazored.Modal;
using Blazored.Modal.Services;
using BOILoanPortal.Models;
using BOILoanPortal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Caching.Memory;

namespace BOILoanPortal.Pages
{
    public partial class CorporateOwnershipInformation
    {
        [Parameter]
        public AOOwnershipInformationCooperate coInfo { get; set; } = new();

        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
        public List<Resp> States { get; set; } = new();
        public List<Resp>? Lgas { get; set; } = new();
        public List<Resp>? cities { get; set; } = new();

        [Inject]
        public IUtilityService _util { get; set; }


        [Parameter]
        public CustomerInfo info { get; set; } = new();

        [Inject]
        private IMemoryCache? _memoryCache { get; set; }

        protected override async Task OnInitializedAsync()
        {
            info = _memoryCache.Get<CustomerInfo>($"info");
            States = await _util.GetStates();
            await base.OnInitializedAsync();
        }

        public async Task GetLga(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                Lgas = await _util.GetLGA(Value);
            }
            coInfo.State = Value;
        }

        public async Task GetCity(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                cities = await _util.GetCity(Value);
            }
            coInfo.LGA = Value;
        }

        public async Task CertificateOfIncorporationRegistration(InputFileChangeEventArgs e)
        {
            var file = e.File;
            coInfo.CertificateOfIncorporationRegistration = file;
            coInfo.CertificateOfIncorporationRegistrationName = file.Name;
        }
        public async Task SubmitCorporateOwnershipInformation()
        {
            //coInfo.UserId = info.UserId;
            //coInfo.UserEmail = info.UserEmail;
            //coInfo.RefNumber = info.RefNumber;

            await BlazoredModal.CloseAsync(ModalResult.Ok(coInfo));

            //Corps.Add(coInfo);
            //coInfo = new AOOwnershipInformationCooperate();
            //StateHasChanged();
        }
    }
}
