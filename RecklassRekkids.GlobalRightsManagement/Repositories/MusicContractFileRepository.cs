using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using RecklassRekkids.GlobalRightsManagement.Extensions;
using RecklassRekkids.GlobalRightsManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecklassRekkids.GlobalRightsManagement.Repositories
{
    public class MusicContractFileRepository : IRepository<MusicContract>
    {
        private readonly GRMOptions _options;
        public MusicContractFileRepository(IOptions<GRMOptions> options)
        {
            _options = options?.Value ??
                throw new ArgumentNullException(nameof(options));
        }

        public IEnumerable<MusicContract> GetAll()
        {
            using (var streamReader = File.OpenText(_options.MusicContractsFileName))
            {
                using (var csvReader = new CsvReader(streamReader))
                {
                    csvReader.Configuration.IgnoreBlankLines = false;
                    csvReader.Configuration.RegisterClassMap<MusicContractClassMap>();
                    csvReader.Configuration.Delimiter = _options.CsvCharacterSeparator;

                    csvReader.Read();
                    csvReader.ReadHeader();
                    csvReader.ValidateHeader<MusicContract>();

                    return csvReader.GetRecords<MusicContract>().ToList();
                }
            }
        }

        public class MusicContractClassMap : ClassMap<MusicContract>
        {
            public MusicContractClassMap()
            {
                Map(m => m.Artist).Name("Artist");
                Map(m => m.Title).Name("Title");
                Map(m => m.Usages)
                    .ConvertUsing(x => x.GetField("Usages").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(u => u.Trim()))
                    .Name("Usages");
                Map(m => m.StartDate)
                    .ConvertUsing(x => {
                        if (!x.GetField("StartDate").TryParseDate(out DateTime dt))
                            throw new FieldValidationException(x.Context, "StartDate", $"Invalid Date {x.GetField("StartDate")}");
                        return dt;
                        })
                    .Name("StartDate");
                Map(m => m.EndDate)
                    .ConvertUsing(x => {
                        if (string.IsNullOrWhiteSpace(x.GetField("EndDate")))
                            return null;
                        if (!x.GetField("EndDate").TryParseDate(out DateTime dt))
                            throw new FieldValidationException(x.Context, "EndDate", $"Invalid Date {x.GetField("EndDate")}");
                        return dt;
                    }).Name("EndDate");
            }
        }
    }
}
