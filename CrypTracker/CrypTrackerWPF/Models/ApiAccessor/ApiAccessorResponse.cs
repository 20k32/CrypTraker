using System.Net.Http;

namespace CrypTrackerWPF.Models.ApiAccessor;

public class ApiAccessorResponse<TResult> : INullCheck
{
    public string Message;
    public TResult Result;
    
    public bool IsNull()
    {
        if (Result is INullCheck nullCheckResult)
        {
            return nullCheckResult.IsNull();
        }

        return Result is null;
    }
}