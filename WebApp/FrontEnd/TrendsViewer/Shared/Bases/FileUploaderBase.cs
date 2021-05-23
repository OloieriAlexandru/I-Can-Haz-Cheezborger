using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TrendsViewer.Pages
{
    public class FileUploaderBase : ComponentBase
    {
        [Parameter]
        public EventCallback<string> OnImageSelected { get; set; }

        public string ImageType { get; set; }

        protected string DropClass { get; set; } = string.Empty;

        private const int maxFileSize = 1048576;

        protected void HandleDragEnter()
        {
            DropClass = "drop-area-drug";
        }
        protected void HandleDragLeave()
        {
            DropClass = string.Empty;
        }

        protected async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            DropClass = string.Empty;
            try
            {
                using var inputFileStream = e.File.OpenReadStream(maxFileSize);
                using var memoryStream = new MemoryStream();
                await inputFileStream.CopyToAsync(memoryStream);
                await OnImageSelected.InvokeAsync($"data:{e.File.ContentType};base64,{Convert.ToBase64String(memoryStream.ToArray())}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
