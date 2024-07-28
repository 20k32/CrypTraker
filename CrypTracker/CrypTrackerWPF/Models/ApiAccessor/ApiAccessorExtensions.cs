using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using CrypTrackerWPF.Models.LocalizationExtensions;

namespace CrypTrackerWPF.Models.ApiAccessor;

public static class ApiAccessorExtensions
{
    public static SimpleContainer AddApiAccessor(this SimpleContainer container) =>
        container.Singleton<IApiAccessor, ApiAccessor>();
    
    public static void ValidateResponse<TResult>(ApiAccessorResponse<TResult> response,
        System.Action<TResult> ifValidCallback)
    {
        if (response is null)
        {
            MessageBox.Show(TranslationSource.Instance[Replicas.EmptyValueError]);
        }
        else if (response.Message is null)
        {
            ifValidCallback(response.Result);
        }
        else
        {
            MessageBox.Show(response.Message);
        }
    }
    
    public static async Task ValidateResponseAsync<TResult>(ApiAccessorResponse<TResult> response,
        System.Func<TResult, Task> ifValidCallbackAsync)
    {
        if (response is null)
        {
            MessageBox.Show(TranslationSource.Instance[Replicas.EmptyValueError]);
        }
        else if (response.Message is null)
        {
            await ifValidCallbackAsync(response.Result);
        }
        else
        {
            MessageBox.Show(response.Message);
        }
    }
}