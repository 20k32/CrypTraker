using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using CrypTrackerWPF.Models;
using CrypTrackerWPF.Models.ApiAccessor;
using CrypTrackerWPF.Models.EventMessages;
using CrypTrackerWPF.Models.ListBoxItemModels;
using CrypTrackerWPF.Models.LocalizationExtensions;

namespace CrypTrackerWPF.Screens.MainWindow;

public sealed class MainWindowViewModel : AffectUiScreen
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IApiAccessor _apiAccessor;
    private ushort _paginationOffsetCoeff;
    
    public BindableCollection<CoinItemModel> Items { get; set; }
    
    public override string DisplayName => TranslationSource.Instance[Replicas.MainWindowTitle];

    public MainWindowViewModel(IApiAccessor apiAccessor, IEventAggregator eventAggregator)
    {
        _paginationOffsetCoeff = 1;
        _apiAccessor = apiAccessor;
        _eventAggregator = eventAggregator;
        Items = new();
    }

    protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
    {
        _apiAccessor.SetIntervalLength(20);
        _apiAccessor.SetIntervalOffset(0);
        await LoadAssetsAsync();
    }

    private async Task LoadAssetsAsync()
    {
        ApiAccessorResponse<IEnumerable<CoinItemModel>> responce = null!;

        await ExecuteInUiContextAsync(async () =>
        {
            responce = await _apiAccessor.GetAssetsInRangeAsync();
        });

        ApiAccessorExtensions.ValidateResponse(responce, (result)
            => Items.AddRange(result));
    }

    #region NextPageCommand

    public bool CanNextPage => 
        _paginationOffsetCoeff * _apiAccessor.GetIntervalLength() != _apiAccessor.GetMaxEntries()
        && string.IsNullOrEmpty(SearchOptions);
    public async Task NextPage()
    {
        var newOffsetValue = (ushort)(_apiAccessor.GetIntervalLength() * _paginationOffsetCoeff);
        _apiAccessor.SetIntervalOffset(newOffsetValue);
        Items.Clear();
        _paginationOffsetCoeff++;
        await LoadAssetsAsync();
        
        if (CanPreviousPage)
        {
            NotifyOfPropertyChange(nameof(CanPreviousPage));
        }
        if (!CanNextPage)
        {
            NotifyOfPropertyChange(nameof(CanNextPage));
        }
        if (CanReset)
        {
            NotifyOfPropertyChange(nameof(CanReset));
        }
    }

    #endregion

    #region PreviousPageCommand

    public bool CanPreviousPage => _paginationOffsetCoeff > 1 
                                   && string.IsNullOrEmpty(SearchOptions);
    public async Task PreviousPage()
    {
        var newOffset =
            (ushort)(_apiAccessor.GetIntervalOffset() - _apiAccessor.GetIntervalLength());
        _apiAccessor.SetIntervalOffset(newOffset);
        Items.Clear();
        await LoadAssetsAsync();
        _paginationOffsetCoeff--;

        if (!CanPreviousPage)
        {
            NotifyOfPropertyChange(nameof(CanPreviousPage));
        }
        if (CanNextPage)
        {
            NotifyOfPropertyChange(nameof(CanNextPage));
        }
        if (!CanReset)
        {
            NotifyOfPropertyChange(nameof(CanReset));
        }
    }

    #endregion
    
    #region ResetCommand

    public bool CanReset => _paginationOffsetCoeff != 1 
                            || !string.IsNullOrEmpty(SearchOptions);

    public async Task Reset()
    {
        _apiAccessor.SetIntervalLength(20);
        _apiAccessor.SetIntervalOffset(0);
        _paginationOffsetCoeff = 1;
        Items.Clear();
        await LoadAssetsAsync();
        
        _searchOptions = string.Empty;
        NotifyOfPropertyChange(nameof(SearchOptions));
        
        NotifyOfPropertyChange(nameof(CanPreviousPage));
        NotifyOfPropertyChange(nameof(CanNextPage));
        NotifyOfPropertyChange(nameof(CanReset));
    }

    #endregion
    
    #region SearchOptions

    private string _searchOptions;

    public string SearchOptions
    {
        get => _searchOptions;
        set
        {
            _searchOptions = value;
            NotifyOfPropertyChange();
            HandleSearchAsync().ShouldNotAwaited();
        }
    }

    private async Task HandleSearchAsync()
    {
        if (SearchOptions != string.Empty)
        {
            var coercedOptions = 
                Regex.Replace(SearchOptions.ToLower(), "[ _]", "-");
            
            await PerformSearchAsync(coercedOptions);
            
            NotifyOfPropertyChange(nameof(CanPreviousPage));
            NotifyOfPropertyChange(nameof(CanNextPage));
            NotifyOfPropertyChange(nameof(CanReset));
        }
        else
        {
            await Reset();
        }
    }
    
    public async Task PerformSearchAsync(string searchOptions)
    {
        ApiAccessorResponse<CoinItemModel> responce = null!;
        
        await ExecuteInUiContextAsync(async () =>
        {
            responce = await _apiAccessor.GetAssetByIdAsync(searchOptions);
        });
        
        if (responce.Result is not null)
        {
            Items.Clear();
            Items.Add(responce.Result);
        }
    }
    
    #endregion

    #region Selected CoinEntity
    
    private CoinItemModel _selectedCoin;

    public CoinItemModel SelectedCoin
    {
        get => _selectedCoin;
        set
        {
            _selectedCoin = value;
            NotifyOfPropertyChange();
            
            if (SelectedCoin is not null)
            {
                _eventAggregator.PublishOnUIThreadAsync(new GetCoinInfoMessage(SelectedCoin.Id))
                    .ShouldNotAwaited();
            }
        }
    }

    #endregion
}