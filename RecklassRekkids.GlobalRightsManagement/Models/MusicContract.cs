using System;
using System.Collections.Generic;
using System.Text;

namespace RecklassRekkids.GlobalRightsManagement.Models
{
    public sealed class MusicContract
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> Usages { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
