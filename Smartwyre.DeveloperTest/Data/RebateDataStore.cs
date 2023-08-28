using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore
{
    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 
        if(rebateIdentifier == "RB-01")
        {
            return new Rebate
            {
                Identifier = "RB-01",
                Incentive = IncentiveType.FixedRateRebate,
                Amount = 50m,
                Percentage = 0.23m
            };
        }
        else
        {
            return null;
        }
        
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        // Update account in database, code removed for brevity
    }
}
