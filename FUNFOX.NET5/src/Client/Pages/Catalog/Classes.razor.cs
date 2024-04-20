using FUNFOX.NET5.Application.Features.Classes.Commands.AddEdit;
using FUNFOX.NET5.Application.Features.Classes.Queries.GetAllPaged;
using FUNFOX.NET5.Application.Features.Products.Commands.EnrollUser;
using FUNFOX.NET5.Application.Features.Products.Queries.GetEnrolledUsers;
using FUNFOX.NET5.Application.Requests.Catalog;
using FUNFOX.NET5.Client.Extensions;
using FUNFOX.NET5.Client.Infrastructure.Managers.Catalog.Product;
using FUNFOX.NET5.Shared.Constants.Application;
using FUNFOX.NET5.Shared.Constants.Permission;
using FUNFOX.NET5.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Client.Pages.Catalog
{
    public partial class Classes
    {
        [Inject] private IClassManager ClassManager { get; set; }


        [Inject]
        NavigationManager NavigationManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [Parameter] public EnrollUserCommand EnrollUserCommand { get; set; } = new();
        private IEnumerable<GetAllPagedClassesResponse> _pagedData;
        public List<GetEnrolledUsersResponse> EnrolledUsers { get; set; }

        private MudTable<GetAllPagedClassesResponse> _table;
        private char _firstLetterOfName;
        private int _totalItems;
        private int _currentPage;
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateClasses;
        private bool _canEditClasses;
        private bool _canDeleteClasses;
        private bool _canExportClasses;
        private bool _canSearchClasses;
        private bool _canEnrollInClasses;
        private bool _canViewEnrolledUsers;
        private bool _loaded;
        private List<GetAllPagedClassesResponse> ClassesData;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateClasses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Classes.Create)).Succeeded;
            _canEditClasses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Classes.Edit)).Succeeded;
            _canDeleteClasses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Classes.Delete)).Succeeded;
            _canExportClasses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Classes.Export)).Succeeded;
            _canSearchClasses = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Classes.Search)).Succeeded;
            _canEnrollInClasses = (await _authorizationService.AuthorizeAsync(_currentUser,Permissions.BasicUserPermissions.EnrollInClasses)).Succeeded;
            _canViewEnrolledUsers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Classes.EnrolledUser)).Succeeded;

            ClassesData = (await GetAllClasses()).ToList();
          
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();

                Console.WriteLine("Hub Connection Started");
            }

            Console.WriteLine("Hub Connection State: " + HubConnection.State);
        }

        private async Task<TableData<GetAllPagedClassesResponse>> ServerReload(TableState state)
        {
            if (!string.IsNullOrWhiteSpace(_searchString))
            {
                state.Page = 0;
            }
            
            
            await LoadData(state.Page, state.PageSize, state);
            return new TableData<GetAllPagedClassesResponse> { TotalItems = _totalItems, Items = _pagedData };
        
        }
        private async Task<IEnumerable<GetAllPagedClassesResponse>> GetAllClasses()
        {
            var request = new GetAllPagedClassesRequest { PageSize = int.MaxValue, PageNumber = 1 }; // Fetch all classes without pagination
            var response = await ClassManager.GetClassesAsync(request);
            if (response.Succeeded)
            {
                return response.Data ?? Enumerable.Empty<GetAllPagedClassesResponse>();
            }
            else
            {
                // Handle error response
                _snackBar.Add("Failed to fetch classes data.", Severity.Error);
                return Enumerable.Empty<GetAllPagedClassesResponse>();
            }
        }
        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {
            string[] orderings = null;
            if (!string.IsNullOrEmpty(state.SortLabel))
            {
                orderings = state.SortDirection != SortDirection.None ? new[] { $"{state.SortLabel} {state.SortDirection}" } : new[] { $"{state.SortLabel}" };
            }

            var request = new GetAllPagedClassesRequest { PageSize = pageSize, PageNumber = pageNumber + 1, SearchString = _searchString, Orderby = orderings };
            var response = await ClassManager.GetClassesAsync(request);
            if (response.Succeeded)
            {
                _totalItems = response.TotalCount;
                _currentPage = response.CurrentPage;
                _pagedData = response.Data ?? Enumerable.Empty<GetAllPagedClassesResponse>();
            }
            else
            {
                _pagedData = Enumerable.Empty<GetAllPagedClassesResponse>();
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
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
            var response = await ClassManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(Classes).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Classes exported"]
                    : _localizer["Filtered Classes exported"], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private  async Task UserEnroll(int classId )
        {

            var userid =_currentUser.GetUserId();
            if (string.IsNullOrEmpty(userid))
            {
                _snackBar.Add(_localizer["User Not Found"], Severity.Error);
                return;
            }
            EnrollUserCommand.classId = classId;
            EnrollUserCommand.userId = userid;
            var response = await ClassManager.UserEnroll(EnrollUserCommand);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        private async Task ViewEnrolledUsers(int id = 0)
        {
            NavigationManager.NavigateTo($"/enrolled-users?classId={id}");
            // Open the modal
           
        }
       
        private async Task InvokeModal(int id = 0)
        {
            if (id != 0 && _canViewEnrolledUsers)
            {
                // Call the method to view enrolled users
                await ViewEnrolledUsers(id);
            }
            var parameters = new DialogParameters();
            if (id != 0)
            {
                var product = _pagedData.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    parameters.Add(nameof(AddEditClassModal.AddEditClassModel), new AddEditClassCommand
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Frequency = product.Frequency,
                        StartTime = product.StartTime,
                        EndTime = product.EndTime,
                        Level = product.Level,
                        MaxClassSize = product.Maxclasssize,
                       });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditClassModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                OnSearch("");
            }
        }

        private async Task Delete(int id)
        {
            string deleteContent = _localizer["Delete Content"];
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await ClassManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    OnSearch("");
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
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
}