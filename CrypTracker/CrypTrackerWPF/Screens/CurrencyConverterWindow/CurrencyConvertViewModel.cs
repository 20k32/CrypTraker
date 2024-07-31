using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using CrypTrackerWPF.Models.ApiAccessor;
using CrypTrackerWPF.Models.EventMessages;
using CrypTrackerWPF.Models.LocalizationExtensions;

namespace CrypTrackerWPF.Screens.CurrencyConverterWindow;

public sealed class CurrencyConvertViewModel : Screen, IHandle<LoadConvertDataMessage>
{
    private decimal _buyPriceM;
    private decimal _sellPriceM;
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

    #region SellQuantity

    private bool _isSellQuantityCorrect;
    
    private string _sellQuantity;

    public string SellQuantity
    {
        get => _sellQuantity;
        set
        {
            _sellQuantity = value;
            NotifyOfPropertyChange();
            ValidateBuyQuantity();
            
            if (_isSellQuantityCorrect)
            {
                Convert();
            }
        }
    }
    #endregion
    
    public bool IsErrorVisible => !_isSellQuantityCorrect;
    public bool IsResultVisible => !IsErrorVisible;

    public void NotifyOfErrorChange()
    {
        NotifyOfPropertyChange(nameof(IsErrorVisible));
        NotifyOfPropertyChange(nameof(IsResultVisible));
    }
    
    public override string DisplayName => TranslationSource.Instance[Replicas.CurrencyConvertWindowTitle];

    public CurrencyConvertViewModel(IEventAggregator eventAggregator, IApiAccessor apiAccessor)
    {
        _eventAggregator = eventAggregator;
        _eventAggregator.SubscribeOnUIThread(this);
        _apiAccessor = apiAccessor;
    }

    public async Task HandleAsync(LoadConvertDataMessage message, CancellationToken cancellationToken)
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


    private void ValidateBuyQuantity()
    {
        if (decimal.TryParse(SellQuantity, out _sellQuantityM))
        {
            _isSellQuantityCorrect = true;
        }
        else
        {
            _isSellQuantityCorrect = false;
        }
        NotifyOfErrorChange();
    }
    
    private void Convert()
    {
        try
        {
            checked
            {
                var totalBuyPrice =  _sellPriceM * _sellQuantityM;
                var converter = totalBuyPrice / _buyPriceM;
                ConvertResul = converter.ToString();
            }
        }
        catch
        {
            _isSellQuantityCorrect = false;
            NotifyOfErrorChange();
        }
    }

    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
        if (close)
        {
            _eventAggregator.Unsubscribe(this);
        }
        else
        {
            _sellQuantity = string.Empty;
            NotifyOfPropertyChange(nameof(SellQuantity));
            _isSellQuantityCorrect = false;
            NotifyOfErrorChange();
        }
        
        return base.OnDeactivateAsync(close, cancellationToken);
    }
}
