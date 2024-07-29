using CrypTrackerWPF.Models.ListBoxItemModels;

namespace CrypTrackerWPF.Models.DTOs;

public sealed class CoinAssetDTO : IMappable<CoinItemModel>, IMappable<DetailedInfoCurrencyModel>
{
    public string Id { get; set; }
    public int Rank { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string Supply { get; set; }
    public string PriceUsd { get; set; }
    public string ChangePercent24Hr { get; set; }
    

    public CoinAssetDTO()
    { }
    
    public CoinItemModel Map()
    {
        return new(Id, Name, Symbol, PriceUsd);
    }

    DetailedInfoCurrencyModel IMappable<DetailedInfoCurrencyModel>.Map()
    {
        return new(Id, Name, Symbol, PriceUsd, Supply, ChangePercent24Hr);
    }

    public DetailedInfoCurrencyModel MapToDetailedInfoCurrency() => 
        (this as IMappable<DetailedInfoCurrencyModel>).Map();
}