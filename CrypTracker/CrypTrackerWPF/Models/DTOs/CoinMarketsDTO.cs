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
        
        public void Map(out CoinMarketModel entity) 
            => entity = new CoinMarketModel(ExchangeId, PriceQuote, QuoteSymbol);
            
        public void Map(out CoinMarketModel entity, Uri exchnageUri)
        {
            Map(out entity);
            entity.ExchangeUri = exchnageUri;
        }
    }
    
    public List<CoinMarketDTO> Data { get; set; }
}