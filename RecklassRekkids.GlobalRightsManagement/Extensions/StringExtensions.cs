using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RecklassRekkids.GlobalRightsManagement.Extensions
{
    public static class StringExtensions
    {
        public static bool IsSameAs(this string a, string b) =>
            string.Compare(a, b, true, CultureInfo.InvariantCulture) == 0;
    }
}
