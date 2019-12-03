using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class DefectCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.Defect Defect { get; set; }
        public SelectList ShopSelectList { get; set; }
    }
}