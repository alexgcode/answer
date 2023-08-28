using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Helpers;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    
    [Fact]
    public void ServiceTestSuccessTrue()
    {
        RebateDataStore rebateDataStore = new RebateDataStore();
        ProductDataStore productDataStore = new ProductDataStore();


        // create the service
        var service = new RebateService(rebateDataStore, productDataStore);


        //create the input
        var input = new CalculateRebateRequest
        {
            RebateIdentifier = "RB-01",
            ProductIdentifier = "PD-01",
            Volume = 20.13m
        };

        // act
        var result = service.Calculate(input);

        // assert
        Assert.NotNull(result);
        Assert.True(result.Success);

    }

    [Fact]
    public void ServiceTestSuccessFalse()
    {
        RebateDataStore rebateDataStore = new RebateDataStore();
        ProductDataStore productDataStore = new ProductDataStore();


        // create the service
        var service = new RebateService(rebateDataStore, productDataStore);


        //create the input
        var input = new CalculateRebateRequest
        {
            RebateIdentifier = "RB-01",
            ProductIdentifier = "PD-03",
            Volume = 20.13m
        };

        // act
        var result = service.Calculate(input);

        // assert
        Assert.NotNull(result);
        Assert.False(result.Success);

    }

    [Fact]
    public void FixedRateAmountCalculatorTest()
    {

        Rebate rebate = new Rebate
        {
            Identifier = "RB-01",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 20,
            Percentage = 0.2m
        };

        Product product = new Product
        {
            Id = 1,
            Identifier = "PD-01",
            Price = 50.0m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var volume = 10.0m;

        //act
        var calculator = new FixedRateAmountRebateCalculator(rebate, product, volume);

        // assert
        Assert.Equal(100m, calculator.CalculateRebase());
    }

    [Fact]
    public void FixedCashRebateCalculator()
    {

        Rebate rebate = new Rebate
        {
            Identifier = "RB-01",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 50,
            Percentage = 0.2m
        };

        //act
        var calculator = new FixedCashRebateCalculator(rebate);

        // assert
        Assert.Equal(50m, calculator.CalculateRebase());
    }

    [Fact]
    public void AmountPerUomRebateCalculatorTest()
    {

        Rebate rebate = new Rebate
        {
            Identifier = "RB-01",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 50,
            Percentage = 0.2m
        };

        var volume = 55.0m;

        //act
        var calculator = new AmountPerUomRebateCalculator(rebate, volume);

        // assert
        Assert.Equal(2750m, calculator.CalculateRebase());
    }
}
