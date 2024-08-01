namespace CrypTrackerWPF.Models.EventMessages;

public sealed class GetCoinInfoMessage
{
    public GetCoinInfoMessage(string id)
    {
        Id = id;
    }
    
    public string Id { get; init; }
}