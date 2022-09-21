using Blazored.Modal;
using Blazored.Modal.Services;
using BOILoanPortal.Models;
using BOILoanPortal.Services;
using BOILoanPortal.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;

namespace BOILoanPortal.Pages
{
    public partial class RelatedPartyInformation
    {
        [Parameter]
        public CustomerInfo info { get; set; } = new();

        [Parameter]
        public AORelatedPartyInformation relatedPartyInfo { get; set; } = new();

        [Inject]
        public NavigationManager _NavigationManager { get; set; }

        [Inject]
        public IAccountOpeningService _Acct { get; set; }

        [Inject]
        public IJSRuntime _JsRuntime { get; set; }

        [Inject]
        public IUtilityService _util { get; set; }

        [Inject]
        private IMemoryCache? _memoryCache { get; set; }

        public bool RelationshipExists = false;
        [Parameter] public string? id { get; set; }

        [CascadingParameter] public IModalService modal { get; set; } = default!;
        string _message = "";

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(id))
            {
                relatedPartyInfo = _memoryCache.Get<AORelatedPartyInformation>($"rpl_{id}");
                if (relatedPartyInfo == null)
                    relatedPartyInfo = new AORelatedPartyInformation();
            }

            await base.OnInitializedAsync();

        }

        public async Task BackToOwenership()
        {
            var coytype = _memoryCache.Get<string>($"coytype_{id}");
            if (coytype.ToLower() == "sole proprietorship")
                _NavigationManager?.NavigateTo($"/soleproprietorship/{id}");
            else
                _NavigationManager?.NavigateTo($"/ownership/{id}");
        }


        public async Task SaveRelatedPartyInfo()
        {

        }

        public async Task SubmitRelatedPartyInfoForm()
        {
            info = _memoryCache.Get<CustomerInfo>($"info_{id}");
            info.HowDoYouKnowAboutBOI = relatedPartyInfo.HowDoYouKnowAboutBOI;
            info.AnyRelationshipWithAnyBOIEmployeeOrAnyOfItsDirectors = relatedPartyInfo.AnyRelationshipWithAnyBOIEmployeeOrAnyOfItsDirectors;
            info.NameOfEmployeeDirector = relatedPartyInfo.NameOfEmployeeDirector;
            info.Relationship = relatedPartyInfo.Relationship;
            info.RelationshipExists = RelationshipExists;

            relatedPartyInfo.UserId = info.UserId;
            relatedPartyInfo.UserEmail = info.UserEmail;
            relatedPartyInfo.RefNumber = info.RefNumber;

            _memoryCache.Set($"info_{id}", info, TimeSpan.FromMinutes(30));

            _memoryCache.Set($"rpl_{id}", relatedPartyInfo, TimeSpan.FromMinutes(30));

            var res = await _Acct.InsertRelatedPartyInfo(relatedPartyInfo);

            if (res.success)
            {

                _NavigationManager.NavigateTo($"/confirm/{id}");
            }
            else
            {
                _message = res.message;
                var parameters = new ModalParameters
                    {
                        { nameof(DisplayMessage.Message), _message }
                    };

                modal.Show<DisplayMessage>("Alert", parameters);
                _message = "";
                return;
            }

            
        }

        public async Task HaveRelationship(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                relatedPartyInfo.AnyRelationshipWithAnyBOIEmployeeOrAnyOfItsDirectors = e.Value.ToString();
                if (relatedPartyInfo.AnyRelationshipWithAnyBOIEmployeeOrAnyOfItsDirectors.Trim().ToUpper() == "YES")
                {
                    RelationshipExists = true;
                    StateHasChanged();
                }
                else
                {
                    RelationshipExists = false;
                    StateHasChanged();
                }
            }
        }
    }
}
