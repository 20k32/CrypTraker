using CrypTrackerWPF.Models.ListBoxItemModels;

namespace CrypTrackerWPF.Models.DTOs;

public sealed class CoinAssetDTO : IMappable<CoinItemModel>
{
    public string Id { get; set; }
    public int Rank { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string Supply { get; set; }
    public string PriceUsd { get; set; }

    public CoinAssetDTO()
    { }
    
    public CoinItemModel Map()
    {
        if (decimal.TryParse(PriceUsd, out var price))
        {
            return new(Name, Symbol, price);
        }

        return new(Name, Symbol, default);
    }
}