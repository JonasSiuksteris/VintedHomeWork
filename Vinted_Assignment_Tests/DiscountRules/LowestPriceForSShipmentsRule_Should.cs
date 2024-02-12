using AutoFixture.Xunit2;
using Moq;
using Vinted_Assignment.DiscountRules;
using Vinted_Assignment.Models;
using Vinted_Assignment.Storage;
using Xunit;

namespace Vinted_Assignment_Tests.DiscountRules;

public class LowestPriceForSShipmentsRule_Should
{
    [Theory, AutoData]
    public void GetDiscount_WhenProviderMR()
    {
        //Arrange
        var rule = new LowestPriceForSShipmentsRule();

        var transaction = new Transaction(DateTime.Now, PackageSize.S, Provider.MR);
        
        //Execute
        var discount = rule.GetDiscount(transaction);
        
        //Assert
        Assert.Equal(0.5m, discount);
    }
    
    [Theory, AutoData]
    public void NotGetDiscount_WhenProviderLP()
    {
        //Arrange
        var rule = new LowestPriceForSShipmentsRule();

        var transaction = new Transaction(DateTime.Now, PackageSize.S, Provider.LP);
        
        //Execute
        var discount = rule.GetDiscount(transaction);
        
        //Assert
        Assert.Equal(0, discount);
    }
}