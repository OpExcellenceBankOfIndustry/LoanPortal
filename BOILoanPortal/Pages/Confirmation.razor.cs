using BOILoanPortal.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;
using System.Globalization;

namespace BOILoanPortal.Pages
{
    public partial class Confirmation
    {
        [Parameter] 
        public CustomerInfo confirmInfo { get; set; } = new();
        [Inject]
        private IMemoryCache? _memoryCache { get; set; }
        [Inject]
        public NavigationManager? _NavigationManager { get; set; }
        [Inject]
        public IJSRuntime _JsRuntime { get; set; }

        [Parameter] public string? id { get; set; }

        public bool IsSP = false;
        public bool litigation = false;
        public bool companyQuoted = false;

        protected override async Task OnInitializedAsync()
        {
            confirmInfo = _memoryCache.Get<CustomerInfo>($"info_{id}");

            if (confirmInfo == null)
            {
                confirmInfo = new CustomerInfo();
            }
            else
            {
                if (confirmInfo.CompanyType.ToLower().Equals("sole proprietorship"))
                {
                    IsSP = true;
                }
            }

            //int digits = 100;
            //System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //string formattedCurrency = digits.ToString("C");
            //Console.WriteLine(formattedCurrency);

            //string str = "1,000,000.00";
            //int digits1 = int.Parse(str, NumberStyles.Number, new CultureInfo("en-US"));
            //Console.WriteLine(digits.ToString("N", new CultureInfo("de-DE")));


            //sole proprietorship
        }

        public async Task BackToRelatedPartyInfo()
        {
            _NavigationManager.NavigateTo($"/relatedPartyInfo/{id}");
        }

        public async Task SubmitValidatedForm()
        {
            _memoryCache.Remove($"info_{id}");

            await _JsRuntime.InvokeVoidAsync("alert", "Account opening request submitted successfully");

           _NavigationManager.NavigateTo($"/index/{id}");
        }
    }
}
