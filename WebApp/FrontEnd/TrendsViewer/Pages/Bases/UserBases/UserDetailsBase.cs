using Microsoft.AspNetCore.Components;
using Models.Trends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class UserDetailsBase : ComponentBase
    {
        [Inject]
        public ITrendService TrendService { get; set; }

        [Inject]
        public IAuthService AuthService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<TrendGetAllDto> Trends { get; set; }


    }
}
