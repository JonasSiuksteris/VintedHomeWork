using Vinted_Assignment.DiscountRules;
using Vinted_Assignment.Models;

namespace Vinted_Assignment.RulesProcessor;

public interface IRuleProcessor
{
    public void AddRule(IDiscountRule discountRule);
    public decimal ExecuteRules(Transaction transaction);

}