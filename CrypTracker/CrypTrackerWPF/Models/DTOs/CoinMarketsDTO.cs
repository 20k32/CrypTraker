using System.Collections.Generic;

namespace CrypTrackerWPF.Models.DTOs;

public sealed class CoinMarketsDTO
{
    public List<CoinMarketDTO> Data { get; set; }
}