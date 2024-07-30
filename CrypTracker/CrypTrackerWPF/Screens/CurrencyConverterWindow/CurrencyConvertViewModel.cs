using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using CrypTrackerWPF.Models.ApiAccessor;
using CrypTrackerWPF.Models.EventMessages;
using CrypTrackerWPF.Models.LocalizationExtensions;

namespace CrypTrackerWPF.Screens.CurrencyConverterWindow;

public sealed class CurrencyConvertViewModel : Screen, IHandle<LoadExchangeDataMessage>
{
    private decimal _buyPriceM;
    private decimal _sellPriceM;
    private decimal _buyQuantityM;
    private decimal _sellQuantityM;
    private readonly IEventAggregator _eventAggregator;
    private readonly IApiAccessor _apiAccessor;

    #region BuyCoinName

    private string _buyCoinName;
    public string BuyCoinName
    {
        get => _buyCoinName;
        set
        {
            _buyCoinName = value;
            NotifyOfPropertyChange();
        }
    }
    #endregion

    #region SellCoinName

    private string _sellCoinName;
    public string SellCoinName
    {
        get => _sellCoinName;
        set
        {
            _sellCoinName = value;
            NotifyOfPropertyChange();
        }
    }

    #endregion

    #region ConvertResult

    private string _convertResult;
    public string ConvertResul
    {
        get => _convertResult;
        set
        {
            _convertResult = value;
            NotifyOfPropertyChange();
        }
    }

    #endregion

    #region BuyQuantity

    private string _buyQuantity;

    public string BuyQuantity
    {
        get => _buyQuantity;
        set
        {
            _buyQuantity = value;
            NotifyOfPropertyChange();
        }
    }
    #endregion

    #region SellQuantity

    private string _sellQuantity;

    public string SellQuantity
    {
        get => _sellQuantity;
        set
        {
            _sellQuantity = value;
            NotifyOfPropertyChange();
        }
    }

    #endregion
    
    
    public override string DisplayName
    {
        get => TranslationSource.Instance[Replicas.CurrencyConvertWindowTitle];
    }

    public CurrencyConvertViewModel(IEventAggregator eventAggregator, IApiAccessor apiAccessor)
    {
        _eventAggregator = eventAggregator;
        _eventAggregator.SubscribeOnUIThread(this);
        _apiAccessor = apiAccessor;
    }

    public async Task HandleAsync(LoadExchangeDataMessage message, CancellationToken cancellationToken)
    {
        BuyCoinName = message.BuyCurrencyName;
        SellCoinName = message.SellCurrencyName;
        var buyCoinResult = await _apiAccessor.GetAssetByIdAsync(message.BuyCurrencyId);
        var sellCoinResult = await _apiAccessor.GetAssetByIdAsync(message.SellCurrencyId);
  
        ApiAccessorExtensions.ValidateResponse(buyCoinResult,
            (result) => _buyPriceM = decimal.Parse(buyCoinResult.Result.Price));

        ApiAccessorExtensions.ValidateResponse(sellCoinResult,
            (result) => _sellPriceM = decimal.Parse(sellCoinResult.Result.Price));
        
        await _eventAggregator.PublishOnUIThreadAsync(new ExchangeDataLoadedMessage());
    }
}
