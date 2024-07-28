using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        _intervalLength = 5;
        _intervalOffset = 0;
        _httpClient = new()
        {
            DefaultRequestHeaders =
            {
                { "Accept", "*/*" },
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
        if (value > _intervalLength
            || value >= MAX_ENTRIES_COUNT)
        {
            throw new ArgumentOutOfRangeException(nameof(_intervalOffset), 
                $"Max value must be less than _intervalLenght and less than {MAX_ENTRIES_COUNT}");
        }

        _intervalOffset = value;
    }
    public async Task<ApiAccessorResponse<IEnumerable<CoinItemModel>>> GetAssetsInRange()
    {
        ApiAccessorResponse<IEnumerable<CoinItemModel>> accessorResponse = new();
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{ApiRoutes.ASSETS_ROUTE}?{ApiRoutes.ASSETS_LIMIT_PARAM}=" +
                $"{_intervalLength}&{ApiRoutes.ASSETS_OFFSET_PARAM}={_intervalOffset}");

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

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}