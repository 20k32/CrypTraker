using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Caliburn.Micro;

namespace CrypTrackerWPF.Models;

public sealed class ExchangeUnitModel
{
    public sealed class ExchangePayload : INotifyPropertyChanged
    {
        public string AssetId { get; set; }
        private string _assetName;

        public string AssetName
        {
            get => _assetName;
            set
            {
                _assetName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsFilled));
            }
        }

        public ExchangePayload()
        { }

        public bool Equals(string id)
        {
            return AssetId == id;
        }

        public bool IsFilled => 
            !(string.IsNullOrEmpty(AssetId) && string.IsNullOrEmpty(AssetName));

        public void Clear()
        {
            AssetId = string.Empty;
            AssetName = string.Empty;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public ExchangePayload BuyCurrency { get; set; }
    public ExchangePayload SellCurrency { get; set; }
    

    public ExchangeUnitModel()
    {
        BuyCurrency = new();
        SellCurrency = new();
    }
    
    public void AddValueAndNotify(string id, string name)
    {
        if (string.IsNullOrEmpty(SellCurrency.AssetId)
                 && !BuyCurrency.Equals(id))
        {
            SellCurrency.AssetId = id;
            SellCurrency.AssetName = name;
        }
        else if (string.IsNullOrEmpty(BuyCurrency.AssetId))
        {
            BuyCurrency.AssetId = id;
            BuyCurrency.AssetName = name;
        }
    }
    
    public void RemoveValueAndNotify(string id)
    {
        if (SellCurrency.Equals(id))
        {
            SellCurrency.Clear();
        }
        if (BuyCurrency.Equals(id))
        {
            BuyCurrency.Clear();
        }
        if (SellCurrency.IsFilled)
        {
            SellCurrency.Clear();
        }
        else if (BuyCurrency.IsFilled)
        {
            BuyCurrency.Clear();
        }
    }
}