using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Helpers
{
    public abstract class RebateCalculator
    {
        public Rebate _rebate;
        public RebateCalculator(Rebate rebate)
        {
            _rebate = rebate;
        }

        public abstract decimal CalculateRebase();
    }
}
