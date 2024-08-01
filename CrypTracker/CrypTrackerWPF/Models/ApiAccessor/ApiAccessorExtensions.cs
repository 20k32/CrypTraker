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
            if (!response.IsNull())
            {
                ifValidCallback(response.Result);
            }
            else
            {
                MessageBox.Show(TranslationSource.Instance[Replicas.ClientSideError]);
            }
        }
        else
        {
            MessageBox.Show(response.Message);
        }
    }
}