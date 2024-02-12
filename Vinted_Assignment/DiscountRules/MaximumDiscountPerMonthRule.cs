using Vinted_Assignment.Models;
using Vinted_Assignment.Storage;

namespace Vinted_Assignment.DiscountRules;

/// <summary>
/// Accumulated discounts cannot exceed 10 € in a calendar month. If there are not enough funds to fully cover a discount this calendar month, it should be covered partially.
/// </summary>
public class MaximumDiscountPerMonthRule : IDiscountRule
{
    private const decimal MaximumMonthlyDiscount = 10m;
    private readonly IDataStore _dataStore;

    public MaximumDiscountPerMonthRule(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public decimal GetDiscount(Transaction transaction)
    {
        var usedDiscountThisMonth = GetUsedDiscount(transaction.Date);
        var remainingDiscountCapacity = MaximumMonthlyDiscount - usedDiscountThisMonth;
        
        // If no discount capacity left, no discount is applied.
        if (remainingDiscountCapacity <= 0)
        {
            return 0;
        }
        
        // If there's enough capacity for the full discount, apply it fully.
        else if (remainingDiscountCapacity >= transaction.ShipmentDiscount)
        {
            return transaction.ShipmentDiscount;
        }
        
        // If there's not enough capacity, apply as much as possible.
        else
        {
            return remainingDiscountCapacity;
        }
    }

    private decimal GetUsedDiscount(DateTime dateTime)
    {
        return _dataStore.GetTransactions().Where(x => x.Date.Month == dateTime.Month).Sum(x => x.ShipmentDiscount);
    }
}