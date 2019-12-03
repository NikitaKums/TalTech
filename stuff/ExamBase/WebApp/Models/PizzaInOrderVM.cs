using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class PizzaInOrderVM
    {
        public PizzaInOrder PizzaInOrder { get; set; }
        public SelectList PizzaSelectList { get; set; }
        public SelectList OrderSelectList { get; set; }
    }
}