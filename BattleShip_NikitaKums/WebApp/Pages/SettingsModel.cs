using System;
using System.ComponentModel.DataAnnotations;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class SettingsModel : PageModel
    {
        [BindProperty]
        [Range(5, 100)]
        public int BoardSize { get; set; }
        
        [BindProperty]
        [Range(0, 1)]
        public int CanTouch { get; set; }
        
        [BindProperty]
        [Range(0, 20)]
        public int CarrierAmount { get; set; }
        
        [BindProperty]
        [Range(0, 20)]
        public int BattleshipAmount { get; set; }
        
        [BindProperty]
        [Range(0, 20)]
        public int SubmarineAmount { get; set; }
        
        [BindProperty]
        [Range(0, 20)]
        public int CruiserAmount { get; set; }
        
        [BindProperty]
        [Range(0, 20)]
        public int PatrolAmount { get; set; }

        public ActionResult OnPost(string submit)
        {
            if (!submit.Equals("Cancel"))
            {
                Options.ChangeOption("Board size", BoardSize);
                Options.ChangeOption("Can touch", CanTouch);
                Options.ChangeOption("Carrier amount", CarrierAmount);
                Options.ChangeOption("Battleship amount", BattleshipAmount);
                Options.ChangeOption("Submarine amount", SubmarineAmount);
                Options.ChangeOption("Cruiser amount", CruiserAmount);
                Options.ChangeOption("Patrol amount", PatrolAmount);
            }            
            return RedirectToPage("Menu");
        }
    }
}
