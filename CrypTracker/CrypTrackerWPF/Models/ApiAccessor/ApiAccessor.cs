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
        _httpClient = new()
        {
            DefaultRequestHeaders =
            {
                { "Accept", "text/json" },
                { "Accept-Encoding", "deflate" }
            }
        };
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
    
    public async Task<ApiAccessorResponse<IEnumerable<CoinItemModel>>> GetAssetsInRangeAsync()
    {
        ApiAccessorResponse<IEnumerable<CoinItemModel>> accessorResponse = new();
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{ApiConstants.ASSETS_ROUTE}?{ApiConstants.ASSETS_LIMIT_PARAM}=" +
                $"{_intervalLength}&{ApiConstants.ASSETS_OFFSET_PARAM}={_intervalOffset}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var assets = await response.Content.ReadFromJsonAsync<CoinAssetsDTO>();
                accessorResponse.Result = assets.Map();
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
                accessorResponse.Result = assets.Data.Map();
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
    
    public async Task<ApiAccessorResponse<IEnumerable<CoinMarketModel>>> GetAssetMarketsAsync(string assetId)
    {
        using (var httpClient = HttpClientFactory.CreateClient())
        {
            ApiAccessorResponse<IEnumerable<CoinMarketModel>> accessorResponse = new();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"{ApiConstants.MARKETS_ROUTE}?{ApiConstants.COIN_ID_PARAM}={assetId}" +
                    $"&{ApiConstants.EXCHANGE_CURRENCY_PARAM}={ApiConstants.EXCHANGE_CURRENCY_PARAM_VALUE}" +
                    $"&{ApiConstants.ASSETS_LIMIT_PARAM}=10");

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var markets = await response.Content.ReadFromJsonAsync<CoinMarketsDTO>();
                    if (markets.Data is not null)
                    {
                        var result = new List<CoinMarketModel>();
                        foreach (var marketDto in markets.Data)
                        {
                            var uri = await GetMarketUriAsync(marketDto.ExchangeId);
                            var coinMarketModel =
                                new CoinMarketModel(marketDto.ExchangeId, marketDto.PriceUsd, uri.Result);
                            result.Add(coinMarketModel);
                        }
                        accessorResponse.Result = result;
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
                accessorResponse.Result = assets.Data.MapToDetailedInfoCurrency();
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