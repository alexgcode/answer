using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Helpers
{
    public class AmountPerUomRebateCalculator : RebateCalculator
    {
        decimal _volume;
        public AmountPerUomRebateCalculator(Rebate rebate, decimal volume) : base(rebate)
        {
            _volume = volume;
        }

        public override decimal CalculateRebase()
        {
            return _rebate.Amount * _volume;
        }
    }
}
