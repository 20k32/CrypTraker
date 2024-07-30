namespace CrypTrackerWPF.Models.ListBoxItemModels;

/// <summary>
/// this class does not implements inotifypropertychange, be aware of memory leaks
/// </summary>
public sealed class CoinItemModel
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string ShortName { get; init; }
    public string Price { get; init; }

    public CoinItemModel(string id, string name, string shortName, string price)
        => (Id, Name, ShortName, Price) = (id, name, shortName, price);
}

