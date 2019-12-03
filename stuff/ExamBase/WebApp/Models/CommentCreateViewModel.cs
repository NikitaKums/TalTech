using Domain;

namespace WebApp.Models
{
    public class CommentCreateViewModel
    {
        public Comment Comment { get; set; }
        public int AppUserId { get; set; }
    }
}