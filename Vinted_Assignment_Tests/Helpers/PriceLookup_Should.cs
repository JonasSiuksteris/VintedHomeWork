using AutoFixture.Xunit2;
using Vinted_Assignment.Helpers;
using Vinted_Assignment.Models;
using Xunit;

namespace Vinted_Assignment_Tests.Helpers;

public class PriceLookup_Should
{
    [Theory]
    [InlineData(Provider.LP, PackageSize.S, 1.5)]
    [InlineData(Provider.LP, PackageSize.M, 4.9)]
    [InlineData(Provider.LP, PackageSize.L, 6.9)]
    [InlineData(Provider.MR, PackageSize.S, 2)]
    [InlineData(Provider.MR, PackageSize.M, 3)]
    [InlineData(Provider.MR, PackageSize.L, 4)]
    public void GetDiscount_WhenItsThird(
        Provider provider,
        PackageSize packageSize,
        decimal correctPrice)
    {
        //Execute
        var price = PriceLookup.GetPrice(provider, packageSize);
        
        //Assert
        Assert.Equal(correctPrice, price);
    }

    [Theory,AutoData]
    public void GetLowestPriceForSSize()
    {
        //Execute
        var price = PriceLookup.GetLowestPriceForSSize();
        
        
        //Assert
        Assert.Equal(1.5m, price);
    }
}