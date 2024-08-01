namespace CrypTrackerWPF.Models.EventMessages;

public sealed class LoadConvertDataMessage
{
    public string BuyCurrencyId { get; init; }
    public string BuyCurrencyName { get; init; }
    public string SellCurrencyId { get; init; }
    public string SellCurrencyName { get; init; }

    public LoadConvertDataMessage(string buyId, string sellId, string buyName, string sellName) =>
        (BuyCurrencyId, SellCurrencyId, BuyCurrencyName, SellCurrencyName) =
        (buyId, sellId, buyName, sellName);
}