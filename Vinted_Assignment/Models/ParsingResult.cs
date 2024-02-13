namespace Vinted_Assignment.Models;

public class ParsingResult
{
    public bool IsSuccess { get; }
    public Transaction? Transaction { get; }
    public string? RawLine { get; }

    private ParsingResult(Transaction? transaction, string? error, string? rawLine)
    {
        Transaction = transaction;
        RawLine = rawLine;
        IsSuccess = transaction != null;
    }
    
    public static ParsingResult Success(Transaction transaction) => new ParsingResult(transaction, null, null);

    public static ParsingResult Failure(string? error, string? rawLine) => new ParsingResult(null, error, rawLine);

}