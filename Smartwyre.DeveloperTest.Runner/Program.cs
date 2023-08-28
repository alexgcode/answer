using Microsoft.Extensions.Logging;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole();
        });
        var logger = loggerFactory.CreateLogger<RebateService>();

        RebateDataStore rebateDataStore = new RebateDataStore();
        ProductDataStore productDataStore = new ProductDataStore();
        
        RebateService rebateService = new RebateService(rebateDataStore, productDataStore, logger);

        Console.Write("Rebate Identifier: ");
        var inputRebateIdentifier = Console.ReadLine();
        Console.Write("Product Identifier: ");
        var inputProductIdentifier = Console.ReadLine();
        Console.Write("Volume: ");
        var inputVolume = Decimal.Parse(Console.ReadLine());

        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = inputRebateIdentifier,
            ProductIdentifier = inputProductIdentifier,
            Volume = inputVolume
        };

        var result = rebateService.Calculate(request);

        Console.WriteLine(result.Success);
    }
}
