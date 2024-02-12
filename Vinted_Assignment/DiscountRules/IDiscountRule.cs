using Vinted_Assignment.Models;

namespace Vinted_Assignment.DiscountRules;

public interface IDiscountRule
{
    decimal GetDiscount(Transaction transaction);
}