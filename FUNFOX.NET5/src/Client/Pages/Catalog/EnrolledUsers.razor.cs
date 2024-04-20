using FUNFOX.NET5.Application.Features.Products.Queries.GetEnrolledUsers;
using FUNFOX.NET5.Application.Requests.Catalog;
using FUNFOX.NET5.Client.Infrastructure.Managers.Catalog.Product;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FUNFOX.NET5.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using FUNFOX.NET5.Client.Extensions;
using FUNFOX.NET5.Application.Models.Chat;
using Microsoft.JSInterop;
using FUNFOX.NET5.Application.Features.Products.Commands.EnrollUser;
using FUNFOX.NET5.Shared.Constants.Application;
using FUNFOX.NET5.Application.Features.Products.Commands.Delete_Enrollment;

namespace FUNFOX.NET5.Client.Pages.Catalog
{
    public partial  class EnrolledUsers
    {

        private IEnumerable<GetEnrolledUsersResponse> _pagedData { get; set; }
       [Inject] private ISnackbar Snackbar { get; set; }
        public DeleteEnrollmentCommand deleteEnrollmentCommand = new DeleteEnrollmentCommand("",0);
        [Inject] private IClassManager ClassManager { get; set; }
        private int _totalItems;
        private int _currentPage;
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;
        private MudTable<GetEnrolledUsersResponse> _table;
        [Inject]
        NavigationManager NavigationManager { get; set; }
        // Define a class to represent the enrolled user data
        private ClaimsPrincipal _currentUser;
        private bool _canCreateClasses;
        private bool _canEditClasses;
        private bool _canDeleteClasses;
        private bool _canExportClasses;
        private bool _canSearchClasses;
        private bool _canEnrollInClasses;
        private bool _canViewEnrolledUsers;
        [CascadingParameter] private HubConnection HubConnection { get; set; }
       

        int _classId;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canViewEnrolledUsers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Classes.EnrolledUser)).Succeeded;
            _canCreateClasses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Classes.Create)).Succeeded;
            _canEditClasses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Classes.Edit)).Succeeded;
            _canDeleteClasses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Classes.Delete)).Succeeded;
            _canExportClasses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Classes.Export)).Succeeded;
            _canSearchClasses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Classes.Search)).Succeeded;
            _canEnrollInClasses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BasicUserPermissions.EnrollInClasses)).Succeeded;

            // Retrieve the class ID from the URL parameters
           
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();

                Console.WriteLine("Hub Connection Started");
            }
            _jsRuntime.InvokeVoidAsync("console.log", _classId);
            // Call the GetEnrolledUsers method to fetch enrolled users based on the class ID

            // Map GetEnrolledUsersResponse objects to EnrolledUser objects

        }
        private async Task<TableData<GetEnrolledUsersResponse>> ServerReload(TableState state )
        {
            var uri = new Uri(NavigationManager.Uri);
            var classId = int.Parse(uri.Query.Substring(1).Split('=')[1]); // Assuming the URL format is /enrolled-users?classId=<classId>

            _jsRuntime.InvokeVoidAsync("console. server reload", classId);
            if (!string.IsNullOrWhiteSpace(_searchString))
            {
                state.Page = 0;
            }


            await LoadData(state.Page, state.PageSize, state, classId);
            return new TableData<GetEnrolledUsersResponse> { TotalItems = _totalItems, Items = _pagedData };

        }
        private async Task LoadData(int pageNumber, int pageSize, TableState state, int classId)
        {
            string[] orderings = null;
            if (!string.IsNullOrEmpty(state.SortLabel))
            {
                orderings = state.SortDirection != SortDirection.None ? new[] { $"{state.SortLabel} {state.SortDirection}" } : new[] { $"{state.SortLabel}" };
            }

            var request = new GetEnrolledUsersRequest { PageSize = pageSize, PageNumber = pageNumber + 1, SearchString = _searchString, Orderby = orderings };
            var response = await ClassManager.GetEnrolledUsers(classId, request);
            if (response.Succeeded)
            {
                _totalItems = response.TotalCount;
                _currentPage = response.CurrentPage;
                _pagedData = response.Data ?? Enumerable.Empty<GetEnrolledUsersResponse>();
            }
            else
            {
                _pagedData = Enumerable.Empty<GetEnrolledUsersResponse>();
                foreach (var message in response.Messages)
                {
                    Snackbar.Add(message, Severity.Error);
                }
            }
        }

        private void OnSearch(string text)
        {
            _searchString = text;
            _table.ReloadServerData();
        }

        private async Task ExportToExcel()
        {
            var response = await ClassManager.EnrolledUsersExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Messages,
                    FileName = $"{nameof(EnrolledUsers).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Enrolled Users File exported"]
                    : _localizer["Filtered Enrolled Users File exported"], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task DeleteEnrollment(int classId, string userId)
        {

          
            deleteEnrollmentCommand.ClassId = classId;
            deleteEnrollmentCommand.UserId = userId;

            _jsRuntime.InvokeAsync<object>("console.log", deleteEnrollmentCommand);
            var response = await ClassManager.DeleteEnrollment(deleteEnrollmentCommand);
            if (response.Succeeded)
            {
                OnSearch("");
                _snackBar.Add(response.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
            }
            else
            {
                OnSearch("");
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }


    }
}
