using System.Threading.Tasks;

namespace CrypTrackerWPF.Models;

public static class TaskExtensions
{
    public static Task ShouldNotAwaited(this Task task) => task;
}