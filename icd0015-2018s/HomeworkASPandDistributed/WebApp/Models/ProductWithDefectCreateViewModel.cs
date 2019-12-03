using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class ProductWithDefectCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.ProductWithDefect ProductWithDefect { get; set; }
        
        public SelectList DefectSelectList { get; set; }
        public SelectList ProductSelectList { get; set; }
    }
}