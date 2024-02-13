using Vinted_Assignment.Models;

namespace Vinted_Assignment.Helpers;

public static class ConsoleWriter
{
    public static void WriteSuccess(Transaction transaction)
    {
        Console.WriteLine($"{transaction.Date:yyyy-MM-dd} {transaction.PackageSize} {transaction.Provider} {transaction.FinalShipmentPrice} {(transaction.ShipmentDiscount != 0 ? transaction.ShipmentDiscount : "-")}");
    }

    public static void WriteFailure(string line)
    {
        Console.WriteLine($"{line} Ignored");
    }
}