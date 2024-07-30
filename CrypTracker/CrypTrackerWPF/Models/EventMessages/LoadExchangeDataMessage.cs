namespace CrypTrackerWPF.Models.EventMessages;

public class LoadExchangeDataMessage
{
    public string BuyCurrencyId { get; init; }
    public string BuyCurrencyName { get; init; }
    public string SellCurrencyId { get; init; }
    public string SellCurrencyName { get; init; }

    public LoadExchangeDataMessage(string buyId, string sellId, string buyName, string sellName) =>
        (BuyCurrencyId, SellCurrencyId, BuyCurrencyName, SellCurrencyName) =
        (buyId, sellId, buyName, sellName);
}