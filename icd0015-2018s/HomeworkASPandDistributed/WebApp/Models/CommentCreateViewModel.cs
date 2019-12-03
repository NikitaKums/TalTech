using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class CommentCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.Comment Comment { get; set; }
        public SelectList ProductSelectList { get; set; }
        public SelectList ShopSelectList { get; set; }
    }
}