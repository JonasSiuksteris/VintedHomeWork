using Vinted_Assignment.Helpers;
using Vinted_Assignment.Models;
using Vinted_Assignment.Storage;

namespace Vinted_Assignment.DiscountRules;

/// <summary>
/// All S shipments should always match the lowest S package price among the providers.
/// </summary>
public class LowestPriceForSShipmentsRule : IDiscountRule
{
    public decimal GetDiscount(Transaction transaction)
    {
        if (transaction.PackageSize == PackageSize.S)
        {
            return PriceLookup.GetPrice(transaction.Provider, transaction.PackageSize) - PriceLookup.GetLowestPriceForSSize();
        }

        return transaction.ShipmentDiscount;
    }
}