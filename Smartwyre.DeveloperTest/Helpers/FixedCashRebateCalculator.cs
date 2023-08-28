using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Helpers
{
    public class FixedCashRebateCalculator : RebateCalculator
    {
        public FixedCashRebateCalculator(Rebate rebate) : base(rebate) 
        {             
        }

        public override decimal CalculateRebase()
        {
            return _rebate.Amount;
        }
    }
}
