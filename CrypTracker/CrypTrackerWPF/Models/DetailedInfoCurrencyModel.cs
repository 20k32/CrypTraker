using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using CrypTrackerWPF.Models.DTOs;

namespace CrypTrackerWPF.Models;

public sealed class DetailedInfoCurrencyModel : INotifyPropertyChanged
{
    public string Id { get; set; }

    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    private string _shortName;

    public string ShortName
    {
        get => _shortName;
        set
        {
            _shortName = value;
            OnPropertyChanged();
        }
    }

    private string _price;

    public string Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged();
        }
    }

    private string _volume;

    public string Volume
    {
        get => _volume;
        set
        {
            _volume = value;
            OnPropertyChanged();
        }
    }

    private string _priceChange;

    public string PriceChange
    {
        get => _priceChange;
        set
        {
            _priceChange = value;
            OnPropertyChanged();
        }
    }

public DetailedInfoCurrencyModel()
    { }

    public DetailedInfoCurrencyModel(string id, string name, string shortName, string price,
        string volume, string priceChange) =>
        (Id, _name, _shortName, _price, _volume, _priceChange) = (id, name, shortName, price, volume, priceChange);

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}