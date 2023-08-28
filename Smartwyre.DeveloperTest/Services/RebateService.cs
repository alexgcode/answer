
using Microsoft.Extensions.Logging;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Helpers;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Data.SqlTypes;
using System.Reflection.Metadata.Ecma335;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    RebateDataStore _rebateDataStore;
    ProductDataStore _productDataStore;
    ILogger<RebateService> _logger;

    public RebateService(RebateDataStore rebateDataStore, ProductDataStore productDataStore, ILogger<RebateService> logger)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _logger = logger;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = _productDataStore.GetProduct(request.ProductIdentifier);

        if (rebate == null || product == null)
        {
            return new CalculateRebateResult { Success = false };
        }

        RebateCalculator calculator;

        if (rebate.Incentive == IncentiveType.FixedCashAmount 
            && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount)
            && rebate.Amount != 0)
        {
            calculator = new FixedCashRebateCalculator(rebate);
        }
        else if (rebate.Incentive == IncentiveType.FixedRateRebate
            && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate)
            && rebate.Percentage != 0 && product.Price != 0 && request.Volume != 0)
        {
            calculator = new FixedRateAmountRebateCalculator(rebate, product, request.Volume);
        }
        else if (rebate.Incentive == IncentiveType.AmountPerUom
            && product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom)
            && rebate.Amount != 0 && request.Volume != 0)
        {
            calculator = new AmountPerUomRebateCalculator(rebate, request.Volume);
        }
        else
        {
            return new CalculateRebateResult { Success = false };
        }

        var rebateAmount = calculator.CalculateRebase();

        try
        {
            _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
            _logger.LogInformation(DateTime.Now + " Register rebate " + rebate.Identifier + " calculation;");
            return new CalculateRebateResult { Success = true };
        }
        catch(Exception ex)
        {
            _logger.LogError(DateTime.Now + " Error registering rebate " + rebate.Identifier + " calculation: " + ex.Message);
            return new CalculateRebateResult { Success = false };
        }
        
    }
}