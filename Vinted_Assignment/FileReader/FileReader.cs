using Vinted_Assignment.Models;

namespace Vinted_Assignment.FileReader;

public class FileReader : IDisposable
{
    private readonly StreamReader _streamReader;

    public FileReader(string path)
    {
        var file = File.OpenRead(path);
        _streamReader = new StreamReader(file);
    }
    
    public bool IsEndOfFile()
    {
        return _streamReader.EndOfStream;
    }

    //This implementation assumes that data will be provided without any empty lines and only defined providers are given
    public ParsingResult GetTransaction()
    {
        var rawLine = _streamReader.ReadLine();
        var parsedLine = rawLine?.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

        if (parsedLine is not { Length: 3 })
        {   
            return ParsingResult.Failure($"Invalid format: {rawLine}", rawLine);
        }
        
        var date = DateTime.ParseExact(parsedLine[0], "yyyy-MM-dd", null);
        var isPackageSizeParsed = Enum.TryParse(parsedLine[1], out PackageSize packageSize);
        var isProviderParsed = Enum.TryParse(parsedLine[2], out Provider provider);

        if (!isProviderParsed || !isPackageSizeParsed)
        {
            return ParsingResult.Failure($"Invalid data in line: {rawLine}", rawLine);
        }
        
        return ParsingResult.Success(new Transaction(date, packageSize, provider)); 
    }

    public void Dispose()
    {
        _streamReader.Dispose();
    }
}