using System.Globalization;

namespace CrypTrackerWPF.Models.ListBoxItemModels;

/// <summary>
/// this class does not implements inotifypropertychange, be aware of memory leaks
/// </summary>
public class LocalizationItemModel
{
    public CultureInfo Culture { get; set; }
    public string Name { get; set; }

    public LocalizationItemModel(CultureInfo culture, string name)
    {
        Culture = culture;
        Name = name;
    }
}