using Blazored.FluentValidation;
using FUNFOX.NET5.Application.Features.Classes.Commands.AddEdit;
using FUNFOX.NET5.Application.Requests;
using FUNFOX.NET5.Client.Extensions;
using FUNFOX.NET5.Client.Infrastructure.Managers.Catalog.Product;
using FUNFOX.NET5.Domain.Entities.Catalog;
using FUNFOX.NET5.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Client.Pages.Catalog
{
    public partial class AddEditClassModal
    {
        [Inject] private IClassManager ProductManager { get; set; }

        [Parameter] public AddEditClassCommand AddEditClassModel { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private string SelectedStartTime { get; set; }
        private string SelectedEndTime { get; set; }
        public List<string> ClassLevels { get; set; } = new List<string> { "Beginner", "Intermediate", "Advanced" };

        public List<string> Frequency { get; set; } = new List<string> { "Daily", "Weekly", "Monthly" };


        // Define your time slots here
        private List<string> TimeSlots { get; set; } = new List<string>
    {
        "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"
    };
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
       
        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            AddEditClassModel.StartTime = SelectedStartTime;
            AddEditClassModel.EndTime = SelectedEndTime;

            if (string.IsNullOrEmpty(SelectedStartTime) || string.IsNullOrEmpty(SelectedEndTime))
            {
                // Handle the case where start time or end time is not selected
                _snackBar.Add("Please select both start time and end time.", Severity.Error);
                return;
            }

            

           
            var response = await ProductManager.SaveAsync(AddEditClassModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            await LoadImageAsync();
         }

        

        private async Task LoadImageAsync()
        {
            var data = await ProductManager.GetProductImageAsync(AddEditClassModel.Id);
            if (data.Succeeded)
            {
                var imageData = data.Data;
               if (!string.IsNullOrEmpty(imageData))
                {
                    AddEditClassModel.ImageDataURL = imageData;
                }
            }
        }

        private void DeleteAsync()
        {
            AddEditClassModel.ImageDataURL = null;
            AddEditClassModel.UploadRequest = new UploadRequest();
        }
     
        private IBrowserFile _file;

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                AddEditClassModel.ImageDataURL = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                AddEditClassModel.UploadRequest = new UploadRequest { Data = buffer, UploadType = Application.Enums.UploadType.Product, Extension = extension };
            }
        }

       
    }
}