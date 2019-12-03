using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaveSystem;

namespace WebApp
{
    public class ShipPlacingChoiceModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }
        
        public IActionResult OnGet(string name)
        {
            Name = name;
            return Page();
        }

        public IActionResult OnPost(string submit)
        {
            var player = new Player(Name);
            var computer = new Computer();
            computer.GenerateRandomBoardWithShips();
            
            if (submit.Equals("Random"))
            {
                player.GenerateRandomBoardWithShips();
                return RedirectToPage("PlayGame", "FirstEntry", new {index = GameSaveRead.Save(player, computer)});
            }
            return RedirectToPage("PlaceShips", new {index = GameSaveRead.Save(player, computer)});
        }
    }
}