namespace CrypTrackerWPF.Models.DTOs;

public interface IMappable<TEntity>
{
    TEntity Map();
}