namespace Vinted_Assignment.Models;

public record Transaction
{
    public DateTime Date { get; }
    public PackageSize PackageSize { get; }
    public Provider Provider { get; }
    public decimal FinalShipmentPrice { get; set; }
    public decimal ShipmentDiscount { get; set; }

    public Transaction(DateTime date, PackageSize packageSize, Provider provider)
    {
        Date = date;
        PackageSize = packageSize;
        Provider = provider;
    }
}