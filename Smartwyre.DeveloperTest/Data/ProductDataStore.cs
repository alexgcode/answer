using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore
{
    public Product GetProduct(string productIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 
        if(productIdentifier == "PD-01")
        {
            return new Product
            {
                Id = 1,
                Identifier = "PD-01",
                Price = 50.0m,
                Uom = "kg",
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate
            };
        }
        else
        {
            return null;
        }
    }
}
