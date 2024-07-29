using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrypTrackerWPF.Models.ListBoxItemModels;

namespace CrypTrackerWPF.Models.ApiAccessor;

public interface IApiAccessor : IDisposable
{
    public Task<ApiAccessorResponse<IEnumerable<CoinItemModel>>> GetAssetsInRangeAsync();
    public Task<ApiAccessorResponse<CoinItemModel>> GetAssetByIdAsync(string id);
    Task<ApiAccessorResponse<IEnumerable<CoinMarketModel>>> GetAssetMarketsAsync(string assetId);
    Task<ApiAccessorResponse<DetailedInfoCurrencyModel>> GetAssetDetailedInfoByIdAsync(string assetId);
    public void SetIntervalLength(ushort value);
    public void SetIntervalOffset(ushort value);
    public ushort GetIntervalLength();
    public ushort GetIntervalOffset();
    public ushort GetMaxEntries();
}