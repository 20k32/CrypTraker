namespace CrypTrackerWPF.Models.DTOs;

public sealed class MarketUriDTO
{
    public sealed class MarketUri
    {
        public string ExchangeUrl { get; set; }
    }

    public MarketUri Data { get; set; }
}
