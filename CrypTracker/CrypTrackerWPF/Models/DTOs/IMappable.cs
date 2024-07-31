namespace CrypTrackerWPF.Models.DTOs;

public interface IMappable<TEntity>
{
    void Map(out TEntity entity);
}