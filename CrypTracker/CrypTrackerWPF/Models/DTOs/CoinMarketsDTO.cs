using System;
using System.Collections.Generic;

namespace CrypTrackerWPF.Models.DTOs;

public sealed class CoinMarketsDTO 
{
    public sealed class CoinMarketDTO : IMappable<CoinMarketModel>
    {
        public string ExchangeId { get; set; }
        public string PriceQuote { get; set; }
        public string QuoteSymbol { get; set; }
        
        CoinMarketModel IMappable<CoinMarketModel>.Map() =>
            new CoinMarketModel(ExchangeId, PriceQuote, QuoteSymbol);
        public CoinMarketModel MapToCoinMarketModel(Uri exchnageUri)
        {
            var result = (this as IMappable<CoinMarketModel>).Map();
            result.ExchangeUri = exchnageUri;
            return result;
        }
    }
    
    public List<CoinMarketDTO> Data { get; set; }
    
    
}