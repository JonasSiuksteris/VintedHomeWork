# VintedHomeWork

Components of solution:
* FileReader - used to read information from file and process it line by line.
* DataStore - used to store already processed information so it could be used for analytic queries when applying various discount rules.
* RuleProcessor - used to process every rule that is registered to it for a single transaction at a time.
* DiscountRules - discount rules defined in requirements of the task. They have to implement IDiscountRule for them to be processable by RulesProcessor.
* PriceLookup - helper that has all of the static information for what are prices dependant on provider and size. Could also be implemented on storage layer.

To make code more decoupled it is possible to introduce TransactionProcessor and OutputService and extract the logic out of them.

With this version only tested the crucial business logic components of the application

To run app navigate to VintedHomeWork/Vinted_Assignment/ and run "dotnet run"
To run tests navigate to VintedHomeWork/Vinted_Assignment_Tests/ and run "dotnet test"
