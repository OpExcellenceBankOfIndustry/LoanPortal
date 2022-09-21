using Blazored.Modal;
using Blazored.Modal.Services;
using BOILoanPortal.Models;
using BOILoanPortal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace BOILoanPortal.Pages
{
    public partial class NextOfKin
    {
        [Parameter]
        public AODetailsOfNextOfKin kin { get; set; } = new();

        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
        public List<Resp> title { get; set; } = new();
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
            //info = _memoryCache.Get<CustomerInfo>($"info_{id}");
            title = await _util.GetTitle();
            States = await _util.GetStates();
            //kin.DateOfBirth = DateTime.UtcNow;
        }

        public async Task GetLga(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                Lgas = await _util.GetLGA(Value);
            }
            kin.State = Value;
        }

        public async Task GetCity(ChangeEventArgs e)
        {
            var Value = e.Value.ToString();
            if (Value is not null || Value != string.Empty)
            {
                cities = await _util.GetCity(Value);
            }
            kin.LGA = Value;
        }

        public async Task SubmitForm()
        {            
            await BlazoredModal.CloseAsync(ModalResult.Ok(kin));
        }

        public void GetBirthDate(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                kin.DateOfBirth = (String)e.Value;
            }
        }
    }
}
