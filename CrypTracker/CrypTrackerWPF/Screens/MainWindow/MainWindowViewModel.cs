using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
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
    
    public async Task Submit()
    {
        ApiAccessorResponse<IEnumerable<CoinItemModel>> responce = null!;
        
        await ExecuteInUiContextAsync(async () =>
        {
            responce = await _apiAccessor.GetAssetsInRange();
        });
        
        ApiAccessorExtensions.ValidateResponce(responce, (result) 
             => Items.AddRange(result));
    }

    public void ValidateResponce()
    {
        
    }
}