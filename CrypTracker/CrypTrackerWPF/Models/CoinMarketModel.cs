using System;

namespace CrypTrackerWPF.Models;

/// <summary>
/// this class does not implements inotifypropertychange, be aware of memory leaks
/// </summary>
public sealed class CoinMarketModel : INullCheck
{
    public string Name { get; set; }
    public string Price { get; set; }
    public string PriceCurrency { get; set; }
    public Uri ExchangeUri { get; set; }

    public CoinMarketModel(string name, string price, string priceCurrency, Uri exchangeUri = null!) => 
        (Name, Price, PriceCurrency, ExchangeUri) = (name, price, priceCurrency, exchangeUri);
    
    public CoinMarketModel()
    { }

    public bool IsNull()
    {
        return string.IsNullOrWhiteSpace(Name)
               || string.IsNullOrWhiteSpace(Price)
               || string.IsNullOrWhiteSpace(PriceCurrency);
    }
}