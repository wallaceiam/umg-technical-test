using Microsoft.Extensions.DependencyInjection;
using RecklassRekkids.GlobalRightsManagement.Extensions;
using RecklassRekkids.GlobalRightsManagement.Models;
using RecklassRekkids.GlobalRightsManagement.Repositories;
using RecklassRekkids.GlobalRightsManagement.Services;
using System;
using System.Linq;

namespace RecklassRekkids.GlobalRightsManagement
{
    public class Application
    {
        private readonly IServiceProvider _serviceProvider;

        protected Application(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public static Application Build(IServiceProvider serviceProvider = null)
        {
            var sp = serviceProvider ?? new ServiceCollection()
                    .AddTransient<IRepository<MusicContract>, MusicContractFileRepository>()
                    .AddTransient<IRepository<DistributionPartnerContract>, DistributionPartnerContractFileRepository>()
                    .AddTransient<IFancyUI, FancyConsoleUI>()
                    .Configure<GRMOptions>(x =>
                    {
                        x.DistributionPartnerContractsFileName = "./Data/DistributionPartnerContracts.csv";
                        x.MusicContractsFileName = "./Data/MusicContracts.csv";
                        x.CsvCharacterSeparator = "|";
                    })
                    .AddTransient<IProductService, ProductService>()
                    .BuildServiceProvider();

            return new Application(sp);
        }

        public Application Run(string[] args)
        {
            var searchTerm = args.Any() ? args[0] : "YouTube 27th Dec 2012";
            var service = _serviceProvider.GetService<IProductService>();
            var fancyUI = _serviceProvider.GetService<IFancyUI>();

            var filter = ProductFilterBuilder
                .ParseCommand(searchTerm)
                .Build();

            var contracts = service.GetMusicContracts(filter);

            fancyUI.Display(contracts);
            return this;
        }
    }
}
