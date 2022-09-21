using Blazored.Modal;
using Blazored.Modal.Services;
using BOILoanPortal.Models;
using BOILoanPortal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;

namespace BOILoanPortal.Pages
{
    public partial class AccountDetails
    {
        [Parameter] public AOAccountDetailsOfOwner? acct { get; set; } = new();

        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        [Parameter]
        public CustomerInfo info { get; set; } = new();

        [Inject]
        private IMemoryCache? _memoryCache { get; set; }

        [Inject]
        public IUtilityService? _util { get; set; }
        public List<Resp> banks { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            info = _memoryCache.Get<CustomerInfo>($"info");
            //BlazoredModal.SetTitle("Enter a Message");
            banks = await _util.GetBanks();
        }

        public async Task SubmitForm()
        {
            //acct.UserId = info.UserId;
            //acct.UserEmail = info.UserEmail;
            //acct.RefNumber = info.RefNumber;
            await BlazoredModal.CloseAsync(ModalResult.Ok(acct));
        }

        
        //async Task Cancel() => await BlazoredModal.CancelAsync();
    }
}
