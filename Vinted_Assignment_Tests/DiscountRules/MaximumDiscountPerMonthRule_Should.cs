using AutoFixture.Xunit2;
using Moq;
using Vinted_Assignment.DiscountRules;
using Vinted_Assignment.Models;
using Vinted_Assignment.Storage;
using Xunit;

namespace Vinted_Assignment_Tests.DiscountRules;

public class MaximumDiscountPerMonthRule_Should
{
    [Theory, AutoData]
    public void GetDiscount_WhenLimitPartialyReached(
        [Frozen] Mock<IDataStore> dataStore)
    {
        //Arrange
        var rule = new MaximumDiscountPerMonthRule(dataStore.Object);
        var newTransaction = new Transaction(DateTime.Now, PackageSize.L, Provider.LP) {ShipmentDiscount = 3};

        var transactions = new List<Transaction>
        {
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP) {ShipmentDiscount = 5},
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP) {ShipmentDiscount = 4},
        };

        dataStore.Setup(x => x.GetTransactions()).Returns(transactions);
        
        //Execute
        var discount = rule.GetDiscount(newTransaction);
        
        //Assert
        dataStore.Verify(x => x.GetTransactions(), Times.Once);
        
        Assert.Equal(1, discount);
    }
    
    [Theory, AutoData]
    public void GetDiscount_WhenLimitNotReached(
        [Frozen] Mock<IDataStore> dataStore)
    {
        //Arrange
        var rule = new MaximumDiscountPerMonthRule(dataStore.Object);
        var newTransaction = new Transaction(DateTime.Now, PackageSize.L, Provider.LP) {ShipmentDiscount = 3};

        var transactions = new List<Transaction>
        {
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP) {ShipmentDiscount = 5},
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP) {ShipmentDiscount = 1},
        };

        dataStore.Setup(x => x.GetTransactions()).Returns(transactions);
        
        //Execute
        var discount = rule.GetDiscount(newTransaction);
        
        //Assert
        dataStore.Verify(x => x.GetTransactions(), Times.Once);
        
        Assert.Equal(3, discount);
    }
    
    [Theory, AutoData]
    public void GetDiscount_WhenNextMonthComes(
        [Frozen] Mock<IDataStore> dataStore)
    {
        //Arrange
        var rule = new MaximumDiscountPerMonthRule(dataStore.Object);
        var newTransaction = new Transaction(DateTime.Now, PackageSize.L, Provider.LP) {ShipmentDiscount = 3};

        var transactions = new List<Transaction>
        {
            new Transaction(newTransaction.Date.AddMonths(-1), PackageSize.L, Provider.LP) {ShipmentDiscount = 5},
            new Transaction(newTransaction.Date.AddMonths(-1), PackageSize.L, Provider.LP) {ShipmentDiscount = 4},
        };

        dataStore.Setup(x => x.GetTransactions()).Returns(transactions);
        
        //Execute
        var discount = rule.GetDiscount(newTransaction);
        
        //Assert
        dataStore.Verify(x => x.GetTransactions(), Times.Once);
        
        Assert.Equal(3, discount);
    }
    
    [Theory, AutoData]
    public void NotGetDiscount_WhenLimitReached(
        [Frozen] Mock<IDataStore> dataStore)
    {
        //Arrange
        var rule = new MaximumDiscountPerMonthRule(dataStore.Object);
        var newTransaction = new Transaction(DateTime.Now, PackageSize.L, Provider.LP) {ShipmentDiscount = 3};

        var transactions = new List<Transaction>
        {
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP) {ShipmentDiscount = 5},
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP) {ShipmentDiscount = 5},
        };

        dataStore.Setup(x => x.GetTransactions()).Returns(transactions);
        
        //Execute
        var discount = rule.GetDiscount(newTransaction);
        
        //Assert
        dataStore.Verify(x => x.GetTransactions(), Times.Once);
        
        Assert.Equal(0, discount);
    }
}