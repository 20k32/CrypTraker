using System.Collections.Generic;
using CrypTrackerWPF.Models.DTOs;

namespace CrypTrackerWPF.Models;

public sealed class DetailedInfoCurrencyModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Price { get; set; }
    public string Volume { get; set; }
    public string PriceChange { get; set; }

    public DetailedInfoCurrencyModel()
    { }

    public DetailedInfoCurrencyModel(string id, string name, string shortName, string price,
        string volume, string priceChange) =>
        (Id, Name, ShortName, Price, Volume, PriceChange) = (id, name, shortName, price, volume, priceChange);
}