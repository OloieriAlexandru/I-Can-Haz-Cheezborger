using System;

namespace Models.Posts
{
    public class PostPatchDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string MediaPath { get; set; }

        public bool ApprovedImage { get; set; }

        public bool ApprovedText { get; set; }
    }
}
