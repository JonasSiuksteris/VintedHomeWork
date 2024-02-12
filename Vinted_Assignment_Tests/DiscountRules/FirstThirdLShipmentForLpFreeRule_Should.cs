using AutoFixture.Xunit2;
using Moq;
using Vinted_Assignment.DiscountRules;
using Vinted_Assignment.Storage;
using Xunit;
using Vinted_Assignment.Models;

namespace Vinted_Assignment_Tests.DiscountRules;

public class FirstThirdLShipmentForLpFreeRule_Should
{
    [Theory, AutoData]
    public void GetDiscount_WhenItsThird(
        [Frozen] Mock<IDataStore> dataStore)
    {
        //Arrange
        var rule = new FirstThirdLShipmentForLpFreeRule(dataStore.Object);
        var newTransaction = new Transaction(DateTime.Now, PackageSize.L, Provider.LP);

        var transactions = new List<Transaction>
        {
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP),
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP),
        };

        dataStore.Setup(x => x.GetTransactions()).Returns(transactions);
        
        //Execute
        var discount = rule.GetDiscount(newTransaction);
        
        //Assert
        dataStore.Verify(x => x.GetTransactions(), Times.Once);
        
        Assert.Equal(6.90m, discount);
    }
    
    [Theory, AutoData]
    public void NotGetDiscount_WhenItsFourth(
        [Frozen] Mock<IDataStore> dataStore)
    {
        //Arrange
        var rule = new FirstThirdLShipmentForLpFreeRule(dataStore.Object);
        var newTransaction = new Transaction(DateTime.Now, PackageSize.L, Provider.LP);

        var transactions = new List<Transaction>
        {
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP),
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP),
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP),
        };

        dataStore.Setup(x => x.GetTransactions()).Returns(transactions);
        
        //Execute
        var discount = rule.GetDiscount(newTransaction);
        
        //Assert
        dataStore.Verify(x => x.GetTransactions(), Times.Once);
        
        Assert.Equal(0, discount);
    }
    
    [Theory, AutoData]
    public void NotGetDiscount_WhenItsDifferentMonth(
        [Frozen] Mock<IDataStore> dataStore)
    {
        //Arrange
        var rule = new FirstThirdLShipmentForLpFreeRule(dataStore.Object);
        var newTransaction = new Transaction(DateTime.Now, PackageSize.L, Provider.LP);

        var transactions = new List<Transaction>
        {
            new Transaction(newTransaction.Date.AddMonths(1), PackageSize.L, Provider.LP),
            new Transaction(newTransaction.Date.AddMonths(1), PackageSize.L, Provider.LP),
        };

        dataStore.Setup(x => x.GetTransactions()).Returns(transactions);
        
        //Execute
        var discount = rule.GetDiscount(newTransaction);
        
        //Assert
        dataStore.Verify(x => x.GetTransactions(), Times.Once);
        
        Assert.Equal(0, discount);
    }
    
    [Theory, AutoData]
    public void NotGetDiscount_WhenItsDifferentProvider(
        [Frozen] Mock<IDataStore> dataStore)
    {
        //Arrange
        var rule = new FirstThirdLShipmentForLpFreeRule(dataStore.Object);
        var newTransaction = new Transaction(DateTime.Now, PackageSize.L, Provider.MR);

        var transactions = new List<Transaction>
        {
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP),
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP),
        };

        dataStore.Setup(x => x.GetTransactions()).Returns(transactions);
        
        //Execute
        var discount = rule.GetDiscount(newTransaction);
        
        //Assert
        dataStore.Verify(x => x.GetTransactions(), Times.Never);
        
        Assert.Equal(0, discount);
    }
    
    [Theory, AutoData]
    public void NotGetDiscount_WhenItsDifferentSize(
        [Frozen] Mock<IDataStore> dataStore)
    {
        //Arrange
        var rule = new FirstThirdLShipmentForLpFreeRule(dataStore.Object);
        var newTransaction = new Transaction(DateTime.Now, PackageSize.M, Provider.LP);

        var transactions = new List<Transaction>
        {
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP),
            new Transaction(newTransaction.Date, PackageSize.L, Provider.LP),
        };

        dataStore.Setup(x => x.GetTransactions()).Returns(transactions);
        
        //Execute
        var discount = rule.GetDiscount(newTransaction);
        
        //Assert
        dataStore.Verify(x => x.GetTransactions(), Times.Never);
        
        Assert.Equal(0, discount);
    }
}