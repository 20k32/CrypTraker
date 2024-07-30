using System;

namespace CrypTrackerWPF.Models;

public sealed class CoinMarketModel
{
    public string Name { get; set; }
    public string Price { get; set; }
    public string PriceCurrency { get; set; }
    public Uri ExchangeUri { get; set; }

    public CoinMarketModel(string name, string price, string priceCurrency, Uri exchangeUri = null!) => 
        (Name, Price, PriceCurrency, ExchangeUri) = (name, price, priceCurrency, exchangeUri = null!);
    

    public CoinMarketModel()
    { }
}