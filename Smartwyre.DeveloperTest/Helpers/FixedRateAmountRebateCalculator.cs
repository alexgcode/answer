using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Helpers
{
    public class FixedRateAmountRebateCalculator : RebateCalculator
    {
        Product _product;
        decimal _volume;

        public FixedRateAmountRebateCalculator(Rebate rebate, Product product, decimal volume) : base(rebate)
        {
            _product = product;
            _volume = volume;
        }

        public override decimal CalculateRebase()
        {
            return _product.Price * _rebate.Percentage * _volume;
        }
    }
}
