using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardUI;
using DAL;
using Domain;
using Domain.Boards;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuSystem.ApplicationMenu.MainMenu.RunMenu();
        }
    }
}