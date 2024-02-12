using Vinted_Assignment.Models;

namespace Vinted_Assignment.Helpers;

public static class PriceLookup
{
    private static readonly Dictionary<(Provider, PackageSize), decimal> Prices = new()
    {
        { new ValueTuple<Provider, PackageSize>(Provider.LP, PackageSize.S), 1.50m },
        { new ValueTuple<Provider, PackageSize>(Provider.LP, PackageSize.M), 4.90m },
        { new ValueTuple<Provider, PackageSize>(Provider.LP, PackageSize.L), 6.90m },
        { new ValueTuple<Provider, PackageSize>(Provider.MR, PackageSize.S), 2.00m },
        { new ValueTuple<Provider, PackageSize>(Provider.MR, PackageSize.M), 3.00m },
        { new ValueTuple<Provider, PackageSize>(Provider.MR, PackageSize.L), 4.00m },
    };

    public static decimal GetPrice(Provider provider, PackageSize packageSize)
    {
        var exist = Prices.TryGetValue((provider, packageSize), out var value);
        if (!exist)
        {
            throw new Exception(
                $"Package size {packageSize} for provider {provider} does not exist in static PriceLookup dictionary");
        }
        
        return value;
    }
    public static decimal GetLowestPriceForSSize()
    {
        return Prices.Where(x => x.Key.Item2 == PackageSize.S).Min(x => x.Value);
    }
}