using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrypTrackerWPF.Models.ListBoxItemModels;

namespace CrypTrackerWPF.Models.ApiAccessor;

public interface IApiAccessor : IDisposable
{
    public Task<ApiAccessorResponse<List<CoinItemModel>>> GetAssetsInRangeAsync();
    public Task<ApiAccessorResponse<List<CoinItemModel>>> GetAssetsByIdOrAliasAsync(string id);
    Task<ApiAccessorResponse<List<CoinMarketModel>>> GetAssetMarketsAsync(string assetId);
    Task<ApiAccessorResponse<DetailedInfoCurrencyModel>> GetAssetDetailedInfoByIdAsync(string assetId);
    public Task<ApiAccessorResponse<CoinItemModel>> GetAssetByIdAsync(string id);
    public void SetIntervalLength(ushort value);
    public void SetIntervalOffset(ushort value);
    public ushort GetIntervalLength();
    public ushort GetIntervalOffset();
    public ushort GetMaxEntries();
}