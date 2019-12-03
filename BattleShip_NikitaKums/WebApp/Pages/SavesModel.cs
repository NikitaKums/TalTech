using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaveSystem;

namespace WebApp.Pages
{
    public class SavesModel : PageModel
    {
        public List<List<string>> SaveHeaders { get; set; } = new List<List<string>>();

        public string Error { get; set; }
        
        public IActionResult OnGet()
        {
            SaveHeaders = GameSaveRead.GetGameInformation();
            return Page();
        }

        public IActionResult OnPost(string[] radio, string[] indexRadio)
        {
            SaveHeaders = GameSaveRead.GetGameInformation();
            if (radio.Length == 0 || indexRadio.Length == 0)
            {
                return Page();
            }
            var choice = radio[0];

            if (choice.Equals("Continue"))
            {
                var chosenSave = GameSaveRead.LoadSaveFromDb(int.Parse(indexRadio[0]));
                if (chosenSave[0].HitPoints > 0 && chosenSave[1].HitPoints > 0)
                {
                    return RedirectToPage("PlayGame", "FirstEntry", new {index = int.Parse(indexRadio[0])});    
                }
                ModelState.AddModelError("Error", "Game has already been won.");
                return Page();
            }

            if (choice.Equals("Delete"))
            {
                GameSaveRead.DeleteSave(int.Parse(indexRadio[0]));
            }
            // just in case
            SaveHeaders = GameSaveRead.GetGameInformation();
            return Page();
        }
    }
}