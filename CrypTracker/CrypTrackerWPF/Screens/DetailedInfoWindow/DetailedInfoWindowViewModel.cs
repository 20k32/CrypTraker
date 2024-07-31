using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using CrypTrackerWPF.Models;
using CrypTrackerWPF.Models.ApiAccessor;
using CrypTrackerWPF.Models.EventMessages;
using CrypTrackerWPF.Models.LocalizationExtensions;

namespace CrypTrackerWPF.Screens.DetailedInfoWindow;

public sealed class DetailedInfoWindowViewModel : AffectUiScreen, IHandle<GetCoinInfoMessage>
{
    
    private readonly IApiAccessor _apiAccessor;
    private readonly IEventAggregator _eventAggregator;

    public ICommand OpenUrlCommand { get; }
    public BindableCollection<CoinMarketModel> Items { get; set; }
    
    public override string DisplayName
    {
        get => TranslationSource.Instance[Replicas.DetailedInfoWindowTitle];
    }

    private DetailedInfoCurrencyModel _currentCoin;

    public DetailedInfoCurrencyModel CurrentCoin
    {
        get => _currentCoin;
        set
        {
            _currentCoin = value;
            NotifyOfPropertyChange();
        }
    }

    public DetailedInfoWindowViewModel(IEventAggregator eventAggregator, IApiAccessor apiAccessor)
    {
        _eventAggregator = eventAggregator;
        _eventAggregator.SubscribeOnUIThread(this);
        _apiAccessor = apiAccessor;
        OpenUrlCommand = new RelayCommand(OpenUrl);
    }

    public async Task HandleAsync(GetCoinInfoMessage message, CancellationToken cancellationToken)
    {
        ApiAccessorResponse<List<CoinMarketModel>> marketsResponse = null!;
        await ExecuteInUiContextAsync(
            async() => marketsResponse = await _apiAccessor.GetAssetMarketsAsync(message.Id));

        if (marketsResponse.Result is not null)
        {
            if (Items is null)
            {
                Items = new();
                NotifyOfPropertyChange(nameof(Items));
            }
            else
            {
                Items.Clear();
            }
            
            Items.AddRange(marketsResponse.Result);
        }
        
        ApiAccessorResponse<DetailedInfoCurrencyModel> assetResponse = null!;
        await ExecuteInUiContextAsync(
            async() => assetResponse = await _apiAccessor.GetAssetDetailedInfoByIdAsync(message.Id));

        if (assetResponse.Result is not null)
        {
            CurrentCoin = assetResponse.Result;
        }
    }

    public void OpenUrl(object arg)
    {
        if (arg is Uri uri)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = uri.AbsoluteUri,
                UseShellExecute = true
            });
        }
    }
}