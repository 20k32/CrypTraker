using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using CrypTrackerWPF.Models;
using CrypTrackerWPF.Models.ApiAccessor;
using CrypTrackerWPF.Models.ListBoxItemModels;
using CrypTrackerWPF.Models.LocalizationExtensions;

namespace CrypTrackerWPF.Screens.MainWindow;

public sealed class MainWindowViewModel : AffectUiScreen
{
    public BindableCollection<CoinItemModel> Items { get; set; }
    
    private readonly IApiAccessor _apiAccessor;

    public override string DisplayName => TranslationSource.Instance[Replicas.MainWindowTitle];
    
    public MainWindowViewModel(IApiAccessor apiAccessor)
    {
        _apiAccessor = apiAccessor;
        Items = new();
    }

    protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
    {
        await LoadAssetsAsync();
    }

    private async Task LoadAssetsAsync()
    {
        ApiAccessorResponse<IEnumerable<CoinItemModel>> responce = null!;
        
        await ExecuteInUiContextAsync(async () =>
        {
            responce = await _apiAccessor.GetAssetsInRange();
        });
        
        ApiAccessorExtensions.ValidateResponse(responce, (result) 
            => Items.AddRange(result));
    }

    #region SelectedCoinIndex
    
    private ushort _selectedIndexTop;
    public ushort SelectedIndexTop
    {
        get => _selectedIndexTop;
        set
        {
            var newValue = checked ((ushort)((value + 1) * 5));
            var oldValue = checked ((ushort)((SelectedIndexTop + 1) * 5));
            
            if (value > SelectedIndexTop)
            {
                _apiAccessor.SetIntervalLength(checked ((ushort)(newValue - oldValue)));
                _apiAccessor.SetIntervalOffset(oldValue);
                
                LoadAssetsAsync().ShouldNotAwaited();
            }
            else
            {
                var items = Items
                    .Skip(newValue)
                    .Take(oldValue)
                    .ToArray();
                
                Items.RemoveRange(items);
            }
            
            _selectedIndexTop = value;
            NotifyOfPropertyChange();
        }
    }
    #endregion
}