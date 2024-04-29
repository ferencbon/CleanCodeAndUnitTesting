using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week05_homework.DiscountManagement
{
    public class DiscountCalculator
    {
        private readonly Dictionary<string, int> _discounts = new Dictionary<string, int>
        {
            { "standard", 5 },
            { "silver", 10 },
            { "gold", 20 }
        };

        public int CalculateDiscountPercentage(string level)
        {
            if (level is null) throw new ArgumentNullException(nameof(level));

            level = level.ToLower();

            if (_discounts.TryGetValue(level, out int discountPercentage))
                return discountPercentage;

            return 0;
        }
    }
}
