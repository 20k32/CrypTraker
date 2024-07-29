using System;

namespace CrypTrackerWPF.Models;

public sealed class CoinMarketModel
{
    public string Name { get; set; }
    public string Price { get; set; }
    public Uri ExchangeUri { get; set; }

    public CoinMarketModel(string name, string price, Uri exchangeUri) => 
        (Name, Price, ExchangeUri) = (name, price, exchangeUri);

    public CoinMarketModel()
    { }
}