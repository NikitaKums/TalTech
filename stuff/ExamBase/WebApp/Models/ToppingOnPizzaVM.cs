using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class ToppingOnPizzaVM
    {
        public ToppingOnPizza ToppingOnPizza { get; set; }
        
        public SelectList ToppingSelectList { get; set; }
        public SelectList PizzaSelectList { get; set; }
    }
}