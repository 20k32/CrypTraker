using System.Net.Http;

namespace CrypTrackerWPF.Models.ApiAccessor;

public class ApiAccessorResponse<TResult>
{
    public string Message;
    public TResult Result;
}