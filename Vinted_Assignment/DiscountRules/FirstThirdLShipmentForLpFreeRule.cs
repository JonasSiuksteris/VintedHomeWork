using Vinted_Assignment.Helpers;
using Vinted_Assignment.Models;
using Vinted_Assignment.Storage;

namespace Vinted_Assignment.DiscountRules;

/// <summary>
/// The third L shipment via LP should be free, but only once a calendar month.
/// </summary>
public class FirstThirdLShipmentForLpFreeRule : IDiscountRule
{
    private readonly IDataStore _dataStore;
    private const int FirstFreeOrderCount = 3;
    
    public FirstThirdLShipmentForLpFreeRule(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public decimal GetDiscount(Transaction transaction)
    {
        if (transaction is { Provider: Provider.LP, PackageSize: PackageSize.L } && IsThirdLShipment(transaction.Date))
        {
            return PriceLookup.GetPrice(transaction.Provider, transaction.PackageSize);
        }

        return transaction.ShipmentDiscount;
    }

    private bool IsThirdLShipment(DateTime dateTime)
    {
        return _dataStore
            .GetTransactions()
            .Count(x =>
                x.Date.Month == dateTime.Month && 
                x is { PackageSize: PackageSize.L, Provider: Provider.LP }) == (FirstFreeOrderCount - 1);
    }
}