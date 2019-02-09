using RecklassRekkids.GlobalRightsManagement.Models;
using System;
using System.Collections.Generic;

namespace RecklassRekkids.GlobalRightsManagement.Repositories
{
    public interface IFancyUI
    {
        void Display(IEnumerable<MusicContract> contracts);
        void DisplayError(string error, Exception ex = null);
    }
}
