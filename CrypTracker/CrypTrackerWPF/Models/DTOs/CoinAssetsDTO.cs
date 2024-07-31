using System.Collections.Generic;
using Caliburn.Micro;
using CrypTrackerWPF.Models.ListBoxItemModels;

namespace CrypTrackerWPF.Models.DTOs;

public sealed class CoinAssetsDTO : IMappable<List<CoinItemModel>>
{
    public List<CoinAssetDTO> Data { get; set; }
    
    public void Map(out List<CoinItemModel> entity)
    {
        entity = new List<CoinItemModel>(Data.Count);
        foreach (var item in Data)
        {
            item.Map(out CoinItemModel coinItemModel);
            entity.Add(coinItemModel);
        }
    }
}