using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklassRekkids.GlobalRightsManagement
{
    public class GRMApplicationOptions
    {
        public string DistributionPartnerContractsFileName { get; internal set; }
        public string MusicContractsFileName { get; internal set; }
        public string CsvCharacterSeparator { get; internal set; }
    }
}
