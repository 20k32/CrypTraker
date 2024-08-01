namespace CrypTrackerWPF.Models.DTOs;

public sealed class MarketUriDTO : INullCheck
{
    public sealed class MarketUri : INullCheck
    {
        public string ExchangeUrl { get; set; }
        public bool IsNull()
        {
            return string.IsNullOrWhiteSpace(ExchangeUrl);
        }
    }

    public MarketUri Data { get; set; }
    public bool IsNull()
    {
        if (Data is null)
        {
            return true;
        }
        
        return Data.IsNull();
    }
}
