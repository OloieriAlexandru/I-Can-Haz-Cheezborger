using Microsoft.AspNetCore.Components;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class UserComunityBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        public IEnumerable<UserGetAllDto> Users { get; set; }
        public IEnumerable<UserGetAllDto> UsersList { get; set; }

        public int PAGESIZE = 5;
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Users = await UserService.GetAll();
                UsersList = Users.Take(PAGESIZE).ToList();
                TotalPages = (int)Math.Ceiling(Users.Count() / (decimal)PAGESIZE);

                StateHasChanged();
            }
        }

        public void UpdateList(int pageNumber)
        {
            CurrentPage = pageNumber;
            UsersList = Users.Skip(pageNumber * PAGESIZE).Take(PAGESIZE).ToList();
        }

        public void NavigateTo(string direction)
        {
            if (direction == "prev" && CurrentPage != 0)
                CurrentPage -= 1;
            if (direction == "next" && CurrentPage != TotalPages - 1)
                CurrentPage += 1;
            if (direction == "first")
                CurrentPage = 0;
            if (direction == "last")
                CurrentPage = TotalPages - 1;

            UpdateList(CurrentPage);
        }

        protected void GoToUserPage(Guid userId)
        {
            NavigationManager.NavigateTo("/users/" + userId);
        }

     
    }
}
