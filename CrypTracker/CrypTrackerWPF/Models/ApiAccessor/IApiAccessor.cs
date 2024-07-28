using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrypTrackerWPF.Models.ListBoxItemModels;

namespace CrypTrackerWPF.Models.ApiAccessor;

public interface IApiAccessor : IDisposable
{
    public Task<ApiAccessorResponse<IEnumerable<CoinItemModel>>> GetAssetsInRange();
    public void SetIntervalLength(ushort value);
    public void SetIntervalOffset(ushort value);
}