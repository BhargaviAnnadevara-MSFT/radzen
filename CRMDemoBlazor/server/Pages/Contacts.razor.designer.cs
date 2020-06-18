﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using RadzenCrm.Models.Crm;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RadzenCrm.Models;

namespace RadzenCrm.Pages
{
    public partial class ContactsComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }


        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }


        [Inject]
        protected CrmService Crm { get; set; }

        protected RadzenGrid<RadzenCrm.Models.Crm.Contact> grid0;

        IEnumerable<RadzenCrm.Models.Crm.Contact> _getContactsResult;
        protected IEnumerable<RadzenCrm.Models.Crm.Contact> getContactsResult
        {
            get
            {
                return _getContactsResult;
            }
            set
            {
                if(!object.Equals(_getContactsResult, value))
                {
                    _getContactsResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        String _contactFilter;
        protected String contactFilter
        {
            get
            {
                return _contactFilter;
            }
            set
            {
                if(!object.Equals(_contactFilter, value))
                {
                    _contactFilter = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }

        }
        protected async System.Threading.Tasks.Task Load()
        {
            var crmGetContactsResult = await Crm.GetContacts();
            getContactsResult = crmGetContactsResult;

            contactFilter = "";
        }

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args)
        {
            var crmGetContactsResult = await Crm.GetContacts(new Query() { Filter = $@"i => i.Email.Contains(""{contactFilter}"") || i.Company.Contains(""{contactFilter}"") || i.FirstName.Contains(""{contactFilter}"") || i.LastName.Contains(""{contactFilter}"")" });
            getContactsResult = crmGetContactsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddContact>("Add Contact", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(RadzenCrm.Models.Crm.Contact args)
        {
            var dialogResult = await DialogService.OpenAsync<EditContact>("Edit Contact", new Dictionary<string, object>() { {"Id", args.Id} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, RadzenCrm.Models.Crm.Contact data)
        {
            try
            {
                var crmDeleteContactResult = await Crm.DeleteContact(data.Id);
                if (crmDeleteContactResult != null) {
                    grid0.Reload();
}
            }
            catch (System.Exception crmDeleteContactException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Contact");
            }
        }
    }
}
