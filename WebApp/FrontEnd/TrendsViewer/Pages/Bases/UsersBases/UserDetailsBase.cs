using Microsoft.AspNetCore.Components;
using Models.Users;
using System;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class UserDetailsBase : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public IImageService ImageService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }

        public string Image { get; set; }

        public UserGetByIdDto User { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                User = await UserService.GetById(Id);
                StateHasChanged();
            }
        }

        protected void ImageSelected(string image)
        {
            Image = image;
            StateHasChanged();
        }

        protected async Task UpdateUserProfile()
        {
            UserPatchDto userPatch = new UserPatchDto()
            {
                Id = Guid.Parse(Id),
                Image = Image
            };

            await UserService.Patch(userPatch);
        }
    }
}
