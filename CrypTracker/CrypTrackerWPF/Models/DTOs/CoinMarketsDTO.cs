using System;
using System.Collections.Generic;

namespace CrypTrackerWPF.Models.DTOs;

public sealed class CoinMarketsDTO : INullCheck
{
    public sealed class CoinMarketDTO : IMappable<CoinMarketModel>,
        INullCheck
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

        public bool IsNull()
        {
            return string.IsNullOrWhiteSpace(ExchangeId)
                   || string.IsNullOrWhiteSpace(PriceQuote)
                   || string.IsNullOrWhiteSpace(QuoteSymbol);
        }
    }
    
    public List<CoinMarketDTO> Data { get; set; }
    
    public bool IsNull()
    {
        if (Data is null)
        {
            return true;
        }
        
        foreach (var item in Data)
        {
            if (item.IsNull())
            {
                return true;
            }
        }

        return false;
    }
}