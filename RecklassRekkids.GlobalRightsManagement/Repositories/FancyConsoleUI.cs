using System;
using System.Collections.Generic;
using System.Text;
using RecklassRekkids.GlobalRightsManagement.Extensions;
using RecklassRekkids.GlobalRightsManagement.Models;

namespace RecklassRekkids.GlobalRightsManagement.Repositories
{
    public class FancyConsoleUI : IFancyUI
    {
        public void Display(IEnumerable<MusicContract> contracts)
        {
            Console.WriteLine("Artist|Title|Usages|StartDate|EndDate");
            foreach (var c in contracts)
            {
                Console.WriteLine($"{c.Artist}|{c.Title}|{string.Join(',', c.Usages)}|{c.StartDate.FormatDate()}|{c.EndDate.FormatDate()}");
            }
        }

        public void DisplayError(string error, Exception ex = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            if (ex != null)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ResetColor();
        }
    }
}
