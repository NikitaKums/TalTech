using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class DrinkInOrderVM
    {
        public DrinkInOrder DrinkInOrder { get; set; }
        
        public SelectList DrinkSelectList { get; set; }
        public SelectList OrderSelectList { get; set; }

    }
}