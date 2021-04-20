﻿using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services;

namespace TrendsViewer.Pages
{
    public class PostCreateBase : ComponentBase
    {
        [Inject]
        public IPostService PostService { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }

        public CreatePostModel CreatePostModel { get; set; }

        private PostCreateDto Post { get; set; }

        public PostCreateBase()
        {
            CreatePostModel = new CreatePostModel();
            Post = new PostCreateDto();
        }

        protected async Task HandleValidSubmit()
        {
            Mapper.Map(CreatePostModel, Post);

            await PostService.CreatePost(Guid.Parse(Id), Post);
            NavigationManager.NavigateTo("/trends/{Id}");
        }

    }
}