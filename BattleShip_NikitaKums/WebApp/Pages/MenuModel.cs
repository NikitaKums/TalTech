using System;
using System.ComponentModel.DataAnnotations;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class MenuModel : PageModel
    {    
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        [BindProperty]
        public string Name { get; set; }
        
        public string FailedToGenerateBoard { get; set; }
        
        public IActionResult OnPost(string submit)
        {
            if (submit.Equals("PlayGame"))
                return RedirectToPage("ShipPlacingChoice", new {name = Name});
            if (submit.Equals("Settings"))
                return RedirectToPage("Settings");
            if (submit.Equals("Saves"))
                return RedirectToPage("Saves");
            return Page();
        }
    }
}