using System;

namespace CrypTrackerWPF.Models.DTOs;

public sealed class SingleCoinAssetDTO : INullCheck
{
    public CoinAssetDTO Data { get; set; }
    public bool IsNull()
    {
        if (Data is null)
        {
            return true;
        }
        
        return Data.IsNull();
    }
}