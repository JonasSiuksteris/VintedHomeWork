using Vinted_Assignment.DiscountRules;
using Vinted_Assignment.Models;

namespace Vinted_Assignment.RulesProcessor;

public class RulesProcessor : IRuleProcessor
{
    private readonly List<IDiscountRule> _rules = [];

    public void AddRule(IDiscountRule discountRule)
    {
        _rules.Add(discountRule);
    }

    public decimal ExecuteRules(Transaction transaction)
    {
        foreach (var rule in _rules)
        {
            transaction.ShipmentDiscount = rule.GetDiscount(transaction);
        }

        return transaction.ShipmentDiscount;
    }
}