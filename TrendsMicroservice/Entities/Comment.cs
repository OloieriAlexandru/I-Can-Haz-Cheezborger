using System;
using System.Collections.Generic;

namespace Entities
{
    public class Comment : UserCreatedEntity
    {
        public string Text { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public Guid PostId { get; set; }

        public Post Post { get; set; }

        public bool ApprovedImage { get; set; }

        public bool ApprovedText { get; set; }

        public ICollection<CommentReact> Reacts { get; set; }
    }
}
