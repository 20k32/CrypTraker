using System.Collections.Generic;
using Caliburn.Micro;
using CrypTrackerWPF.Models.ListBoxItemModels;

namespace CrypTrackerWPF.Models.DTOs;

public sealed class CoinAssetsDTO : IMappable<IEnumerable<CoinItemModel>>
{
    public List<CoinAssetDTO> Data { get; set; }
    
    public IEnumerable<CoinItemModel> Map()
    {
        foreach (var item in Data)
        {
            yield return item.Map();
        }
    }
}