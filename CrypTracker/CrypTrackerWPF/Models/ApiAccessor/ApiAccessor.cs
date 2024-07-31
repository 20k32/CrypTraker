using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography.Pkcs;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;
using CrypTrackerWPF.Models.DTOs;
using CrypTrackerWPF.Models.ListBoxItemModels;
using CrypTrackerWPF.Models.LocalizationExtensions;

namespace CrypTrackerWPF.Models.ApiAccessor;

public class ApiAccessor : IApiAccessor
{
    private HttpClient _httpClient;
    private ushort _intervalLength;
    private ushort _intervalOffset;
    public const ushort MAX_ENTRIES_COUNT = 2000;
    
    public ApiAccessor()
    {
        _httpClient = HttpClientFactory.CreateClient();
    }

    public void SetIntervalLength(ushort value)
    {
        if (value > MAX_ENTRIES_COUNT)
        {
            throw new ArgumentOutOfRangeException(nameof(_intervalLength), 
                $"Max value is greater than {MAX_ENTRIES_COUNT}");
        }

        _intervalLength = value;
    }

    public void SetIntervalOffset(ushort value)
    {
        if (value >= MAX_ENTRIES_COUNT)
        {
            throw new ArgumentOutOfRangeException(nameof(_intervalOffset), 
                $"Max value must be less than less than {MAX_ENTRIES_COUNT}");
        }

        _intervalOffset = value;
    }

    public ushort GetIntervalLength() => _intervalLength;

    public ushort GetIntervalOffset() => _intervalOffset;

    public ushort GetMaxEntries() => MAX_ENTRIES_COUNT;
    
    public async Task<ApiAccessorResponse<List<CoinItemModel>>> GetAssetsInRangeAsync()
    {
        ApiAccessorResponse<List<CoinItemModel>> accessorResponse = new();
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{ApiConstants.ASSETS_ROUTE}?{ApiConstants.ASSETS_LIMIT_PARAM}=" +
                $"{_intervalLength}&{ApiConstants.ASSETS_OFFSET_PARAM}={_intervalOffset}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var assets = await response.Content.ReadFromJsonAsync<CoinAssetsDTO>();
                assets.Map(out accessorResponse.Result);
            }
            else
            {
                accessorResponse.Message =
                    $"{TranslationSource.Instance[Replicas.ServerSideError]} {response.StatusCode}";
            }
        }
        catch (Exception _)
        {
            accessorResponse.Message = TranslationSource.Instance[Replicas.ClientSideError];
        }
        
        return accessorResponse;
    }

    
    public async Task<ApiAccessorResponse<CoinItemModel>> GetAssetByIdAsync(string id)
    {
        ApiAccessorResponse<CoinItemModel> accessorResponse = new();
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{ApiConstants.ASSETS_ROUTE}/{id}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var assets = await response.Content.ReadFromJsonAsync<SingleCoinAssetDTO>();
                assets.Data.Map(out accessorResponse.Result);
            }
            else
            {
                accessorResponse.Message =
                    $"{TranslationSource.Instance[Replicas.ServerSideError]} {response.StatusCode}";
            }
        }
        catch (Exception _)
        {
            accessorResponse.Message = TranslationSource.Instance[Replicas.ClientSideError];
        }
        
        return accessorResponse;
    }

    private async Task<ApiAccessorResponse<Uri>> GetMarketUriAsync(string marketId)
    {
        using (var httpClient = HttpClientFactory.CreateClient())
        {
            ApiAccessorResponse<Uri> accessorResponse = new();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"{ApiConstants.EXCHANGES_ROUTE}/{marketId}");

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var assets = await response.Content.ReadFromJsonAsync<MarketUriDTO>();
                    accessorResponse.Result = new Uri(assets.Data.ExchangeUrl);
                }
                else
                {
                    accessorResponse.Message =
                        $"{TranslationSource.Instance[Replicas.ServerSideError]} {response.StatusCode}";
                }
            }
            catch (Exception _)
            {
                accessorResponse.Message = TranslationSource.Instance[Replicas.ClientSideError];
            }
        
            return accessorResponse;
        }
    }
    
    public async Task<ApiAccessorResponse<List<CoinMarketModel>>> GetAssetMarketsAsync(string assetId)
    {
        using (var httpClient = HttpClientFactory.CreateClient())
        {
            ApiAccessorResponse<List<CoinMarketModel>> accessorResponse = new();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"{ApiConstants.MARKETS_ROUTE}?{ApiConstants.COIN_ID_PARAM}={assetId}" +
                    $"&{ApiConstants.ASSETS_LIMIT_PARAM}=10");

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var markets = await response.Content.ReadFromJsonAsync<CoinMarketsDTO>();
                    if (markets.Data is not null)
                    {
                        accessorResponse.Result = new List<CoinMarketModel>(markets.Data.Count);
                        foreach (var marketDto in markets.Data)
                        {
                            var uri = await GetMarketUriAsync(marketDto.ExchangeId);
                            marketDto.Map(out var coinMarketModel, uri.Result);
                            accessorResponse.Result.Add(coinMarketModel);
                        }
                    }
                    else
                    {
                        accessorResponse.Message = TranslationSource.Instance[Replicas.ClientSideError];
                    }
                }
                else
                {
                    accessorResponse.Message =
                        $"{TranslationSource.Instance[Replicas.ServerSideError]} {response.StatusCode}";
                }
            }
            catch (Exception _)
            {
                accessorResponse.Message = TranslationSource.Instance[Replicas.ClientSideError];
            }
            return accessorResponse;
        }
    }
    
    public async Task<ApiAccessorResponse<DetailedInfoCurrencyModel>> 
        GetAssetDetailedInfoByIdAsync(string id)
    {
        ApiAccessorResponse<DetailedInfoCurrencyModel> accessorResponse = new();
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{ApiConstants.ASSETS_ROUTE}/{id}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var assets = await response.Content.ReadFromJsonAsync<SingleCoinAssetDTO>();
                assets.Data.Map(out accessorResponse.Result);
            }
            else
            {
                accessorResponse.Message =
                    $"{TranslationSource.Instance[Replicas.ServerSideError]} {response.StatusCode}";
            }
        }
        catch (Exception _)
        {
            accessorResponse.Message = TranslationSource.Instance[Replicas.ClientSideError];
        }
        
        return accessorResponse;
    }
    
    
    public void Dispose()
    {
        _httpClient.Dispose();
    }
}