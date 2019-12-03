using System;
using Domain.Identity;

namespace Domain
{
    public class Comment : BaseEntity
    {
        public string CommentTitle { get; set; }
        public string CommentBody { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}