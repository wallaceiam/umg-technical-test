using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using RecklassRekkids.GlobalRightsManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecklassRekkids.GlobalRightsManagement.Repositories
{
    public class DistributionPartnerContractFileRepository : IRepository<DistributionPartnerContract>
    {
        private readonly GRMApplicationOptions _options;
        public DistributionPartnerContractFileRepository(IOptions<GRMApplicationOptions> options)
        {
            _options = options?.Value ?? 
                throw new ArgumentNullException(nameof(options));
        }

        public IEnumerable<DistributionPartnerContract> GetAll()
        {
            using (var streamReader = File.OpenText(_options.DistributionPartnerContractsFileName))
            {
                using (var csvReader = new CsvReader(streamReader))
                {
                    csvReader.Configuration.IgnoreBlankLines = false;
                    csvReader.Configuration.Delimiter = _options.CsvCharacterSeparator;
                    csvReader.Configuration.RegisterClassMap<DistributionPartnerContractClassMap>();

                    csvReader.Read();
                    csvReader.ReadHeader();
                    csvReader.ValidateHeader<DistributionPartnerContract>();

                    return csvReader.GetRecords<DistributionPartnerContract>().ToList();
                }
            }
        }

        public class DistributionPartnerContractClassMap : ClassMap<DistributionPartnerContract>
        {
            public DistributionPartnerContractClassMap()
            {
                Map(m => m.Partner).Name("Partner");
                Map(m => m.Usage).Name("Usage");
            }
        }
    }
}
