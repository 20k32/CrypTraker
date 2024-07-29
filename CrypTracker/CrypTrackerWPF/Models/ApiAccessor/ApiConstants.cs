namespace CrypTrackerWPF.Models.ApiAccessor;

public static class ApiConstants
{
    public const string ASSETS_LIMIT_PARAM = "limit";
    public const string ASSETS_OFFSET_PARAM = "offset";
    public const string EXCHANGE_CURRENCY_PARAM = "quoteId";
    public const string COIN_ID_PARAM = "baseId";
    public const string EXCHANGE_CURRENCY_PARAM_VALUE = "united-states-dollar";
    public const string ASSETS_ROUTE = "https://api.coincap.io/v2/assets";
    public const string RATES_ROUTE = "https://api.coincap.io/v2/rates/";
    public const string MARKETS_ROUTE = "https://api.coincap.io/v2/markets";
    public const string EXCHANGES_ROUTE = "https://api.coincap.io/v2/exchanges";
}