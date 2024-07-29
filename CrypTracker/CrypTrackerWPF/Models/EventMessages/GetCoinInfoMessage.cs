namespace CrypTrackerWPF.Models.EventMessages;

public class GetCoinInfoMessage
{
    public GetCoinInfoMessage(string id)
    {
        Id = id;
    }
    
    public string Id { get; init; }
}