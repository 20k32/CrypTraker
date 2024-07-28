namespace CrypTrackerWPF.Models.ListBoxItemModels;

/// <summary>
/// this class does not implements inotifypropertychange, be aware of memory leaks
/// </summary>
public sealed class CoinItemModel
{
    public string Name { get; init; }
    public string ShortName { get; init; }
    public decimal Price { get; init; }

    public CoinItemModel(string name, string shortName, decimal price)
        => (Name, ShortName, Price) = (name, shortName, price);
}